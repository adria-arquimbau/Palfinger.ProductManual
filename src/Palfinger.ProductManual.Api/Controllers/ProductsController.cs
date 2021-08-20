using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Palfinger.ProductManual.Domain;
using Palfinger.ProductManual.Domain.Repositories;
using Palfinger.ProductManual.Queries.Handlers;

namespace Palfinger.ProductManual.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator, IRepositoryWrapper repositoryWrapper)
        {
            _mediator = mediator;
            _repositoryWrapper = repositoryWrapper;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(ManualByProductIdFilterRequest),(int)HttpStatusCode.OK)]
        [Produces("application/json")]
        public async Task<IActionResult> GetManualByProductId([FromQuery] ManualByProductIdFilterRequest request)
        {
            var response = await _mediator.Send(new GetManualByProductIdQueryRequest(request.ProductId, request.PageNumber, request.PageSize));
            
            // var manual = _repositoryWrapper.AttributeRepository.GetAttributesPaging(request);
            //
            // var metadata = new
            // {
            //     manual.TotalCount,
            //     manual.PageSize,
            //     manual.CurrentPage,
            //     manual.HasNext, 
            //     manual.HasPrevious
            // };
            //
            // Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            
            return Ok();
        }   
    }
}   