using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Model
{
    public class Token
    {
        public string type { get; set; }
        public string token { get; set; }
        public DateTime expires_on { get; set; }
        public int expiry_month { get; set; }
        public int expiry_year { get; set; }
        public string name { get; set; }
        public string scheme { get; set; }
        public string last4 { get; set; }
        public string bin { get; set; }
        public string card_type { get; set; }
        public string card_category { get; set; }
        public string issuer { get; set; }
        public string issuer_country { get; set; }
        public string product_id { get; set; }
        public string product_type { get; set; }
    }
}
