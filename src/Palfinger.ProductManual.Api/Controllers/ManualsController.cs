using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Palfinger.ProductManual.Api.Models.Manual;

namespace Palfinger.ProductManual.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManualsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ManualsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Produces("application/json")]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}   