using System;
using System.Collections.Generic;
using System.Text;

namespace Juniper.Taxation.Core.Domain.Entities
{
    public class OrderLineItem
    {
        /// <summary>
        /// Unique identifier of the given line item. 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Quantity for the item.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Product tax code for the item. If omitted, the item will remain fully taxable.
        /// </summary>
        public string ProductTaxCode { get; set; }

        /// <summary>
        /// Unit price for the item.
        /// </summary>
        public int UnitPrice { get; set; }

        /// <summary>
        /// Total discount (non-unit) for the item.
        /// </summary>
        public int Discount { get; set; }
    }
}
