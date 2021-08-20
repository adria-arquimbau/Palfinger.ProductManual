using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Palfinger.ProductManual.Api.Models;
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
            var products = _repositoryWrapper.ProductRepository.FindByCondition(x => x.Name == "Name");
            
            var products2 = _repositoryWrapper.ProductRepository.FindALl();

            return Ok();
        }   
        
        [HttpGet]
        [ProducesResponseType(typeof(ManualResponse),(int)HttpStatusCode.OK)]
        [Produces("application/json")]
        public async Task<IActionResult> GetManual([FromQuery] ManualFilterRequest manualFilterRequest)
        {
            var manual = _repositoryWrapper.ProductRepository.GetProducts(manualFilterRequest);

            return Ok();
        }   
    }
}   