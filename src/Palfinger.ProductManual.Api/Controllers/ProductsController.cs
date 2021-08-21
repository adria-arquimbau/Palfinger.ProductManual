using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Palfinger.ProductManual.Api.Models;
using Palfinger.ProductManual.Domain;
using Palfinger.ProductManual.Domain.Repositories;
using Palfinger.ProductManual.Queries.Handlers;
using Palfinger.ProductManual.Queries.Models;

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
        [ProducesResponseType(typeof(ManualByProductIdPagingResponse),(int)HttpStatusCode.OK)]
        [Produces("application/json")]
        public async Task<ActionResult> GetManualByProductId([FromQuery] GetManualByProductIdRequest request)
        {
            var response = await _mediator.Send(new GetManualByProductIdQueryRequest(request.ProductId, request.PageNumber, request.PageSize));

            return Ok(response.ManualByProductIdPagingResponse);
        }
    }
}   