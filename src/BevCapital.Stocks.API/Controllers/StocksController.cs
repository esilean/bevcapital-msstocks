using BevCapital.Stocks.Application.UseCases.Stocks;
using BevCapital.Stocks.Application.UseCases.Stocks.Response;
using BevCapital.Stocks.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BevCapital.Stocks.API.Controllers
{

    public class StocksController : BaseController
    {
        public StocksController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// list all stocks
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>A list of stocks</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StockOut>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IReadOnlyCollection<Notification>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<StockOut>>> List(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new ListStock.ListStockQuery(), cancellationToken);
        }

        /// <summary>
        /// creates a stock
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IReadOnlyCollection<Notification>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Unit>> Create(Create.CreateStockCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        /// <summary>
        /// updates a stock
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Error</response>
        [HttpPut("{symbol}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IReadOnlyCollection<Notification>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IReadOnlyCollection<Notification>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Unit>> Update(string symbol, Update.UpdateStockCommand command, CancellationToken cancellationToken)
        {
            command.Symbol = symbol;
            return await _mediator.Send(command, cancellationToken);
        }

        /// <summary>
        /// removes a stock
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="204">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Error</response>
        [HttpDelete("{symbol}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IReadOnlyCollection<Notification>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IReadOnlyCollection<Notification>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Unit>> Delete(string symbol, CancellationToken cancellationToken)
        {
            await _mediator.Send(new Delete.DeleteStockCommand { Symbol = symbol }, cancellationToken);
            return NoContent();
        }

    }
}
