using System;
using System.Collections.Generic;
using System.Text;

namespace Juniper.Taxation.Core.Domain.Entities
{
    public class SalesTax
    {
        public double OrderTotalAmount { get; set; }
        public double Shipping { get; set; }
        public int TaxableAmount { get; set; }
        public double AmountToCollect { get; set; }
        public double Rate { get; set; }

        //public bool has_nexus { get; set; }
        public bool FreightTaxable { get; set; }
        public string TaxSource { get; set; }
        public Jurisdictions Jurisdictions { get; set; }
        public Breakdown Breakdown { get; set; }
    }
}
