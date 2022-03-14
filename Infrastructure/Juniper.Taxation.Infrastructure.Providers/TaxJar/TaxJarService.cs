using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Juniper.Taxation.Core.Application.Interfaces;
using Juniper.Taxation.Core.Application.Models;
using Juniper.Taxation.Core.Domain.Entities;
using Juniper.Taxation.Infrastructure.Providers.TaxJar.Contracts;
using Microsoft.AspNetCore.WebUtilities;
using GetTaxRateByLocationResponse = Juniper.Taxation.Infrastructure.Providers.TaxJar.Contracts.GetTaxRateByLocationResponse;
//using GetTaxRateByLocationResponse = Juniper.Taxation.Core.Application.Models.GetTaxRateByLocationResponse;
using SalesTax = Juniper.Taxation.Core.Domain.Entities.SalesTax;


namespace Juniper.Taxation.Infrastructure.Providers.TaxJar
{
    public class TaxJarService : ITaxProviderService
    {
        public readonly IHttpClientAdapter _httpClientAdapter;
        public readonly IMapper _mapper;
        public const string TaxJarHttpClient = "TaxJarHttpClient";


        public TaxJarService(IHttpClientAdapter httpClientAdpater, IMapper mapper)
        {
            _httpClientAdapter = httpClientAdpater;
            _mapper = mapper;
        }


        public async Task<SalesTax> CalculateSalesTaxByOrderAsync(OrderSalesTaxCommand command)
        {
            var relativePath = "v2/taxes";

            var request = _mapper.Map<CalculateSalesTaxOrderRequest>(command);
            
            var response= await _httpClientAdapter.PostAsync<CalculateSalesTaxOrderResponse>(TaxJarHttpClient, relativePath, request);

            return _mapper.Map<SalesTax>(response.SalesTax);
        }

        public async Task<TaxRate> GetTaxRateByLocationAsync(TaxByLocationQuery query)
        {
            var getTaxRatesUri = $"v2/rates/{query.Location.Zip}";

            var queryDict = query.Location.GetType().GetProperties()
                .Where(prop => !string.IsNullOrEmpty((string) prop.GetValue(query.Location, null)) && ((string)prop.GetValue(query.Location, null)!=query.Location.Zip) )
                .ToDictionary(prop => prop.Name, prop => (string) prop.GetValue(query.Location, null));

            var relativePath = QueryHelpers.AddQueryString(getTaxRatesUri, queryDict);

            var taxRateResponse= await _httpClientAdapter.GetAsync<GetTaxRateByLocationResponse>(TaxJarHttpClient, relativePath);

            return _mapper.Map<TaxRate>(taxRateResponse.rate);
        }
    }
}
