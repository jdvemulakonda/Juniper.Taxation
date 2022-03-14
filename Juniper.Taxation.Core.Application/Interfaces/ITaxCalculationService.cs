using Juniper.Taxation.Core.Domain;
using Juniper.Taxation.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Juniper.Taxation.Core.Application.Models;
using Juniper.Taxation.Core.Application.Wrappers;

namespace Juniper.Taxation.Core.Application.Interfaces
{
    public interface ITaxCalculationService
    {
        Task<SalesTaxByOrderResponse> CalculateSalesTaxByOrder(OrderSalesTaxCommand command);

        Task<GetTaxRateByLocationResponse> GetTaxRateByLocation(TaxByLocationQuery query);
    }
}
