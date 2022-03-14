using System.Threading.Tasks;
using Juniper.Taxation.Core.Application.Interfaces;
using Juniper.Taxation.Core.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Juniper.Taxation.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class TaxesController:ControllerBase
    {
        private readonly ILogger<TaxesController> _logger;
        private readonly ITaxCalculationService _taxCalculationService;

        public TaxesController(ILogger<TaxesController> logger, ITaxCalculationService taxCalculationService)
        {
            _logger = logger;
            _taxCalculationService= taxCalculationService;
        }

        [HttpGet]
        [Route("taxrate")]
        [ProducesResponseType(typeof(GetTaxRateByLocationResponse),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTaxRateByLocationAsync([FromQuery] TaxByLocationQuery query)
        {
            return Ok(await _taxCalculationService.GetTaxRateByLocation(query));
        }

        [HttpPost]
        [Route("ordersalestax")]
        [ProducesResponseType(typeof(SalesTaxByOrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CalculateSalesTaxByOrderAsync([FromBody] OrderSalesTaxCommand command)
        {
            return Ok(await _taxCalculationService.CalculateSalesTaxByOrder(command));
        }

    }
}
