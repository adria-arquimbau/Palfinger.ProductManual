using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Palfinger.ProductManual.Api.Models;
using Palfinger.ProductManual.Queries.Handlers;
using Palfinger.ProductManual.Queries.Models;

namespace Palfinger.ProductManual.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
            
        [HttpGet]
        [ProducesResponseType(typeof(ManualByProductIdPagingResponse),(int)HttpStatusCode.OK)]
        [Produces("application/json")]
        public async Task<ActionResult> GetManualByProductId([FromQuery] GetManualByProductIdRequest request)
        {
            var response = await _mediator.Send(new GetManualByProductIdQueryRequest(request.ProductId, request.PageNumber, request.PageSize));

            return Ok(response.ManualByProductIdPagingResponse);
        }
    }
}   