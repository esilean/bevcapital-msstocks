using BevCapital.Stocks.Application.UseCases.AppUserStocks;
using BevCapital.Stocks.Application.UseCases.AppUserStocks.Response;
using BevCapital.Stocks.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BevCapital.Stocks.API.Controllers
{

    public class AppUserStocksController : BaseController
    {
        public AppUserStocksController(IMediator mediator) : base(mediator) { }


        /// <summary>
        /// gets stocks from the logged user
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>The details of a user</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Error</response>
        [HttpGet("user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppUserStockOut))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IReadOnlyCollection<Notification>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IReadOnlyCollection<Notification>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AppUserStockOut>> Get(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new ListStock.ListAppUserStockQuery { }, cancellationToken);
        }

        /// <summary>
        /// adds a stock to the logged user
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Error</response>
        [HttpPost("user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IReadOnlyCollection<Notification>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Unit>> Add(Add.AddAppUserStockCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        /// <summary>
        /// removes a stock from the logged user
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="204">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Error</response>
        [HttpDelete("{symbol}/user")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Unit))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IReadOnlyCollection<Notification>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IReadOnlyCollection<Notification>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Unit>> Delete(string symbol, CancellationToken cancellationToken)
        {
            await _mediator.Send(new Delete.DeleteAppUserStockCommand { Symbol = symbol }, cancellationToken);
            return NoContent();
        }

    }
}
