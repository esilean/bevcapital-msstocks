using BevCapital.Stocks.Application.UseCases.Stocks;
using BevCapital.Stocks.Application.UseCases.Stocks.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BevCapital.Stocks.API.Controllers
{

    public class StocksController : BaseController
    {
        public StocksController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<ActionResult<List<StockOut>>> List()
        {
            return await _mediator.Send(new List.Query());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        [HttpDelete("delete/{symbol}")]
        public async Task<ActionResult<Unit>> Delete(string symbol)
        {
            await _mediator.Send(new Delete.Command { Symbol = symbol });
            return NoContent();
        }

    }
}
