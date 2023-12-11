using Microsoft.AspNetCore.Mvc;
using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Taxually.TechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatRegistrationController : ControllerBase
    {

        private readonly IVatRegistrationServiceFactory _vatRegistrationServiceFactory;

        public VatRegistrationController(IVatRegistrationServiceFactory vatRegistrationServiceFactory)
        {
            _vatRegistrationServiceFactory = vatRegistrationServiceFactory;
        }

        /// <summary>
        /// Registers a company for a VAT number in a given country
        /// </summary>
        [HttpPost]
        public async Task <ActionResult> Post([FromBody] VatRegistrationRequest request)
        {
            try
            {
                await _vatRegistrationServiceFactory.CreateProcessorInstance(request).SaveDataToDestinationAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
