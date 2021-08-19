using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Palfinger.ProductManual.Api.Models.Manual;
using Palfinger.ProductManual.Api.Models.Search;

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
        
        [HttpPost("search")]
        [ProducesResponseType(typeof(PaginatedDataResponse<ManualResponse>), (int)HttpStatusCode.OK)]
        [Produces("application/json")]
        public async Task<ActionResult<PaginatedDataResponse<ManualResponse>>> Search([FromBody] UserFilterRequest request)
        {
            var response = await _mediator.Send(request.ToDomain());

            var result = _userSearchResponseBuilder.BuildSearchUserResponseCollection(response);

            return Ok();
        }
    }
}   