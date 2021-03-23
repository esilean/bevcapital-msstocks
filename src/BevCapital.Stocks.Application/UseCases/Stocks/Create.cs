using BevCapital.Stocks.Application.DataProviders;
using BevCapital.Stocks.Application.Errors;
using BevCapital.Stocks.Domain.Constants;
using BevCapital.Stocks.Domain.Entities;
using BevCapital.Stocks.Domain.Notifications;
using BevCapital.Stocks.Domain.Repositories;
using FluentValidation;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Application.UseCases.Stocks
{
    public class Create
    {
        public class Command : IRequest
        {
            public string Symbol { get; set; }
            public string Name { get; set; }
            public string Exchange { get; set; }
            public string Website { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Symbol).NotEmpty();
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Exchange).NotEmpty();
                RuleFor(x => x.Website).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IAppNotificationHandler _appNotificationHandler;
            private readonly IDateProvider _dateProvider;

            public Handler(IUnitOfWork unitOfWork,
                           IAppNotificationHandler appNotificationHandler,
                           IDateProvider dateProvider)
            {
                _unitOfWork = unitOfWork;
                _appNotificationHandler = appNotificationHandler;
                _dateProvider = dateProvider;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await _unitOfWork.Stocks.FindAsync(request.Symbol) != null)
                {
                    _appNotificationHandler.AddNotification(Keys.APPSTOCK, Messages.STOCK_EXISTS);
                    return Unit.Value;
                }

                var stock = Stock.Create(request.Symbol, request.Name, request.Exchange, request.Website);
                if (stock.Invalid)
                {
                    _appNotificationHandler.AddNotifications(stock.ValidationResult);
                    return Unit.Value;
                }

                stock.SetPrice(0, 0, 0, 0, 0, _dateProvider.Now, 0, _dateProvider.Now, 0, 0);

                await _unitOfWork.Stocks.AddAsync(stock);

                var success = await _unitOfWork.SaveAsync();
                if (success) return Unit.Value;

                throw new AppException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
