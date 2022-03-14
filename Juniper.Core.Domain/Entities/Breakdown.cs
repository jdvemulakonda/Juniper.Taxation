using System;
using System.Collections.Generic;
using System.Text;

namespace Juniper.Taxation.Core.Domain.Entities
{
    public class Breakdown
    {
        public int TaxableAmount { get; set; }
        public double TaxCollectable { get; set; }
        public double CombinedTaxRate { get; set; }
        public int StateTaxableAmount { get; set; }
        public double StateTaxRate { get; set; }
        public double StateTaxCollectable { get; set; }
        public int CountyTaxableAmount { get; set; }
        public double CountyTaxRate { get; set; }
        public double CountyTaxCollectable { get; set; }
        public int CityTaxableAmount { get; set; }
        public int CityTaxRate { get; set; }
        public int CityTaxCollectable { get; set; }
        public int SpecialDistrictTaxableAmount { get; set; }
        public double SpecialTaxRate { get; set; }
        public double SpecialDistrictTaxCollectable { get; set; }
        //public List<TaxLineItem> LineItems { get; set; }
    }
}
