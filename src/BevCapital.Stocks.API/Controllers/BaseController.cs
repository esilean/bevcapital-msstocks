using BevCapital.Stocks.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BevCapital.Stocks.API.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    [Authorize]
    [Produces(Common.APPLICATION_JSON)]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
