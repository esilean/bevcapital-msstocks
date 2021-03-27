using BevCapital.Stocks.Application.UseCases.AppUserStocks;
using BevCapital.Stocks.Application.UseCases.AppUserStocks.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BevCapital.Stocks.API.Controllers
{

    public class AppUserStocksController : BaseController
    {
        public AppUserStocksController(IMediator mediator) : base(mediator) { }


        [HttpGet("user")]
        public async Task<ActionResult<AppUserStockOut>> Get()
        {
            return await _mediator.Send(new ListStock.Query { });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("user")]
        public async Task<ActionResult<Unit>> Add(Add.Command command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        [HttpDelete("{symbol}/user")]
        public async Task<ActionResult<Unit>> Delete(string symbol)
        {
            await _mediator.Send(new Delete.Command { Symbol = symbol });
            return NoContent();
        }

    }
}
