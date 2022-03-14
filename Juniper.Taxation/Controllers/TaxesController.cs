using System.Security.Cryptography;
using System.Threading.Tasks;
using Juniper.Taxation.Core.Application.Interfaces;
using Juniper.Taxation.Core.Application.Models;
using Juniper.Taxation.Core.Domain;
using Juniper.Taxation.Core.Domain.Entities;
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
        public async Task<IActionResult> GetTaxRateByLocationAsync([FromQuery] TaxByLocationQuery query)
        {
            return Ok(await _taxCalculationService.GetTaxRateByLocation(query));
        }

        [HttpPost]
        [Route("ordersalestax")]
        public async Task<IActionResult> CalculateSalesTaxByOrderAsync([FromBody] OrderSalesTaxCommand command)
        {
            return Ok(await _taxCalculationService.CalculateSalesTaxByOrder(command));
        }

    }
}
