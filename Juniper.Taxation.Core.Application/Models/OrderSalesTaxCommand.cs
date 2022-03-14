using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Juniper.Taxation.Core.Domain.Entities;

namespace Juniper.Taxation.Core.Application.Models
{
    public class OrderSalesTaxCommand
    {
        public List<OrderLineItem> LineItems { get; set; }

        public double Amount { get; set; }

        [Required]
        public double Shipping { get; set; }

        public Address FromAddress { get; set; }

        public Address ToAddress { get; set; }
    }

}
