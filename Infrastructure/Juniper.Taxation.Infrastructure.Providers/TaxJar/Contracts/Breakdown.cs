using System;
using System.Collections.Generic;
using System.Text;

namespace Juniper.Taxation.Infrastructure.Providers.TaxJar.Contracts
{
    public class Breakdown
    {
        public int taxable_amount { get; set; }
        public double tax_collectable { get; set; }
        public double combined_tax_rate { get; set; }
        public int state_taxable_amount { get; set; }
        public double state_tax_rate { get; set; }
        public double state_tax_collectable { get; set; }
        public int county_taxable_amount { get; set; }
        public double county_tax_rate { get; set; }
        public double county_tax_collectable { get; set; }
        public int city_taxable_amount { get; set; }
        public int city_tax_rate { get; set; }
        public int city_tax_collectable { get; set; }
        public int special_district_taxable_amount { get; set; }
        public double special_tax_rate { get; set; }
        public double special_district_tax_collectable { get; set; }
        public List<TaxLineItem> line_items { get; set; }
    }

   
}
