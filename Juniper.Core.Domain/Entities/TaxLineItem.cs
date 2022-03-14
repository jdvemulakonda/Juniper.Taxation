using System;
using System.Collections.Generic;
using System.Text;

namespace Juniper.Taxation.Core.Domain.Entities
{
    public class TaxLineItem
    {
        public string Id { get; set; }
        public int TaxableAmount { get; set; }
        public double TaxCollectable { get; set; }
        public double CombinedTaxRate { get; set; }
        public int StateTaxableAmount { get; set; }
        public double StateSalesTaxRate { get; set; }
        public double StateAmount { get; set; }
        public int CountyTaxableAmount { get; set; }
        public double CountyTaxRate { get; set; }
        public double CountyAmount { get; set; }
        public int CityTaxableAmount { get; set; }
        public int CityTaxRate { get; set; }
        public int CityAmount { get; set; }
        public int SpecialDistrictTaxableAmount { get; set; }
        public double SpecialTaxRate { get; set; }
        public double SpecialDistrictAmount { get; set; }
    }
}
