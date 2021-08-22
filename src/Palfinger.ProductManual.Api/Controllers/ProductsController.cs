using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Palfinger.ProductManual.Api.Models;
using Palfinger.ProductManual.Domain.Commands.CreateProduct;
using Palfinger.ProductManual.Domain.Commands.CreateProduct.Models;
using Palfinger.ProductManual.Domain.Repositories;
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
        
        [HttpPost]
        [ProducesResponseType(typeof(ManualByProductIdPagingResponse),(int)HttpStatusCode.OK)]
        [Produces("application/json")]
        public async Task<ActionResult> Post([FromBody] ProductRequest request)
        {   
            await _mediator.Send(new CreateProductCommandRequest(request.Name, request.Description, request.ImageUrl, 
                request.Attributes.Select(a =>  new CreateAttributeRequest(a.Name, a.Description, a.ImageUrl, 
                    a.Configurations.Select(c => new CreateConfigurationRequest(c.Name, c.Description, c.ImageUrl)).ToList())).ToList()));

            return Ok();
        }
    }
}   