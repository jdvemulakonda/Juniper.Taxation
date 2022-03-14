using System;
using System.Collections.Generic;
using System.Text;

namespace Juniper.Taxation.Infrastructure.Providers.TaxJar.Contracts
{
    public class Jurisdrictions
    {
        public string country { get; set; }
        public string state { get; set; }
        public string county { get; set; }
        public string city { get; set; }
    }
}
