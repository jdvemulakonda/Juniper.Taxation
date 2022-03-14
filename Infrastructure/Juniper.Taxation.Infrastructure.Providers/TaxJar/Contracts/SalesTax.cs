namespace Juniper.Taxation.Infrastructure.Providers.TaxJar.Contracts
{
    public class SalesTax
    {
        public double Order_total_amount { get; set; }
        public double Shipping { get; set; }
        public int Taxable_amount { get; set; }
        public double Amount_to_collect { get; set; }
        public double Rate { get; set; }

        //public bool has_nexus { get; set; }
        public bool Freight_taxable { get; set; }
        public string Tax_source { get; set; }
        public Jurisdrictions jurisdictions { get; set; }
        public Breakdown breakdown { get; set; }
    }
}