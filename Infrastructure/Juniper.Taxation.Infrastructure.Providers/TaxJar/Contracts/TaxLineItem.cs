using System;
using System.Collections.Generic;
using System.Text;

namespace Juniper.Taxation.Infrastructure.Providers.TaxJar.Contracts
{
    public class TaxLineItem
    {
        public string id { get; set; }
        public int taxable_amount { get; set; }
        public double tax_collectable { get; set; }
        public double combined_tax_rate { get; set; }
        public int state_taxable_amount { get; set; }
        public double state_sales_tax_rate { get; set; }
        public double state_amount { get; set; }
        public int county_taxable_amount { get; set; }
        public double county_tax_rate { get; set; }
        public double county_amount { get; set; }
        public int city_taxable_amount { get; set; }
        public int city_tax_rate { get; set; }
        public int city_amount { get; set; }
        public int special_district_taxable_amount { get; set; }
        public double special_tax_rate { get; set; }
        public double special_district_amount { get; set; }
    }
}
