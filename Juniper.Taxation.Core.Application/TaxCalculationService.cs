using Juniper.Taxation.Core.Application.Interfaces;
using Juniper.Taxation.Core.Domain.Entities;
using System.Threading.Tasks;
using AutoMapper;
using Juniper.Taxation.Core.Application.Exceptions;
using Juniper.Taxation.Core.Application.Models;
using Juniper.Taxation.Core.Application.Models.Validators;
using Microsoft.Extensions.Logging;

namespace Juniper.Taxation.Core.Application
{
    public class TaxCalculationService : ITaxCalculationService
    {
        private readonly ITaxProviderService _taxProvider;
        private readonly ILogger<TaxCalculationService> _logger;
        private readonly IMapper _mapper;

        public TaxCalculationService(ILogger<TaxCalculationService> logger,ITaxProviderService provider,IMapper mapper)
        {
            _logger =logger;
            _taxProvider = provider;
            _mapper = mapper;
        }

        public async Task<SalesTaxByOrderResponse> CalculateSalesTaxByOrder(OrderSalesTaxCommand command)
        {
            var validator = new OrderSalesTaxCommandValidator();
            var validationResult =await validator.ValidateAsync(command);

            if (validationResult.Errors.Count>0)
            {
                throw new ValidationException(validationResult);
            }

            var result = await _taxProvider.CalculateSalesTaxByOrderAsync(command);

            return _mapper.Map<SalesTaxByOrderResponse>(new SalesTaxByOrderResponse
            {
                SalesTax = _mapper.Map<SalesTax>(result)
            });
            
        }

        public async Task<GetTaxRateByLocationResponse> GetTaxRateByLocation(TaxByLocationQuery query)
        {
            var validator = new TaxByLocationQueryValidator();
            var validationResult = await validator.ValidateAsync(query);

            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationException(validationResult);
            }

            var result = await _taxProvider.GetTaxRateByLocationAsync(query);

            return _mapper.Map<GetTaxRateByLocationResponse>(new GetTaxRateByLocationResponse
            {
                Rate = _mapper.Map<TaxRate>(result)
            });
        }
    }
}
