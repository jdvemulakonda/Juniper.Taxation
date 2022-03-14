namespace Juniper.Taxation.Infrastructure.Providers.TaxJar.Contracts
{
    public class LineItem
    {
        public string id { get; set; }
        public int quantity { get; set; }
        public string product_tax_code { get; set; }
        public int unit_price { get; set; }
        public int discount { get; set; }
    }
}
