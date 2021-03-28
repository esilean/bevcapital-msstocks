using BevCapital.Stocks.Application.UseCases.AppUserStocks;
using BevCapital.Stocks.Application.UseCases.AppUserStocks.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace BevCapital.Stocks.API.Controllers
{

    public class AppUserStocksController : BaseController
    {
        public AppUserStocksController(IMediator mediator) : base(mediator) { }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("user")]
        public async Task<ActionResult<AppUserStockOut>> Get(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new ListStock.ListAppUserStockQuery { }, cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("user")]
        public async Task<ActionResult<Unit>> Add(Add.AddAppUserStockCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{symbol}/user")]
        public async Task<ActionResult<Unit>> Delete(string symbol, CancellationToken cancellationToken)
        {
            await _mediator.Send(new Delete.DeleteAppUserStockCommand { Symbol = symbol }, cancellationToken);
            return NoContent();
        }

    }
}
