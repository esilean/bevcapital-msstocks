using BevCapital.Stocks.Application.UseCases.Stocks;
using BevCapital.Stocks.Application.UseCases.Stocks.Response;
using MediatR;
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
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<StockOut>>> List(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new ListStock.ListStockQuery(), cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.CreateStockCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{symbol}")]
        public async Task<ActionResult<Unit>> Update(string symbol, Update.UpdateStockCommand command, CancellationToken cancellationToken)
        {
            command.Symbol = symbol;
            return await _mediator.Send(command, cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{symbol}")]
        public async Task<ActionResult<Unit>> Delete(string symbol, CancellationToken cancellationToken)
        {
            await _mediator.Send(new Delete.DeleteStockCommand { Symbol = symbol }, cancellationToken);
            return NoContent();
        }

    }
}
