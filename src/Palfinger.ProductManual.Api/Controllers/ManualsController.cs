using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Palfinger.ProductManual.Api.Models.Manual;
using Palfinger.ProductManual.Domain;
using Palfinger.ProductManual.Domain.Repositories;

namespace Palfinger.ProductManual.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManualsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMediator _mediator;
        public ManualsController(IMediator mediator, IRepositoryWrapper repositoryWrapper)
        {
            _mediator = mediator;
            _repositoryWrapper = repositoryWrapper;
        }
            
        [HttpGet("test")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Produces("application/json")]
        public async Task<IActionResult> GetTest()
        {
            var products = _repositoryWrapper.AttributeRepository.FindByCondition(x => x.Name == "Name");
            
            var products2 = _repositoryWrapper.AttributeRepository.FindALl();

            return Ok();
        }   
        
        [HttpGet]
        [ProducesResponseType(typeof(ManualResponse),(int)HttpStatusCode.OK)]
        [Produces("application/json")]
        public async Task<IActionResult> GetManual([FromQuery] AttributesFromProductFilterRequest attributesFromProductFilterRequest)
        {
            var manual = _repositoryWrapper.AttributeRepository.GetAttributesPaging(attributesFromProductFilterRequest);

            var metadata = new
            {
                manual.TotalCount,
                manual.PageSize,
                manual.CurrentPage,
                manual.HasNext,
                manual.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            
            return Ok(manual);
        }   
    }
}   