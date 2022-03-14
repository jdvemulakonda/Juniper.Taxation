using Juniper.Taxation.Core.Domain.Entities;

namespace Juniper.Taxation.Core.Application.Models
{
    public class GetTaxRateByLocationResponse
    {
        /// <summary>
        ///  Rate object with rates for a given location broken down by state, county, city, and district. 
        ///  For international requests, returns standard and reduced rates.
        /// </summary>
        public TaxRate Rate { get; set; }
    }
}
