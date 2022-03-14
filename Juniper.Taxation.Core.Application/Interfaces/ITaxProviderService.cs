using Juniper.Taxation.Core.Domain;
using Juniper.Taxation.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Juniper.Taxation.Core.Application.Models;

namespace Juniper.Taxation.Core.Application.Interfaces
{
    public interface ITaxProviderService
    {
        Task<TaxRate> GetTaxRateByLocationAsync(TaxByLocationQuery query);

        Task<SalesTax> CalculateSalesTaxByOrderAsync(OrderSalesTaxCommand command);
    }
}
