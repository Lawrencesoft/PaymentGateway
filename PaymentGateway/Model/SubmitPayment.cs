using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Model
{
    public class SubmitPayment
    {
        public string type { get; set; }
        public string number { get; set; }
        public int expiry_month { get; set; }
        public int expiry_year { get; set; }
        public string name { get; set; }
        public string cvv { get; set; }
        public Source source { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string payment_type { get; set; }
        public string reference { get; set; }
        public string description { get; set; }
        public bool capture { get; set; }
        public DateTime capture_on { get; set; }
        public Customer customer { get; set; }
        public string success_url { get; set; }
        public string failure_url { get; set; }
        public string payment_ip { get; set; }
    }
}
