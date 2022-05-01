using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Model
{
    public class PaymentDetails
    {
        public string id { get; set; }
        public DateTime requested_on { get; set; }
        public Source source { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string payment_type { get; set; }
        public string reference { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public bool approved { get; set; }
        public Risk risk { get; set; }
        public Customer customer { get; set; }
        public BillingDescriptor billing_descriptor { get; set; }
        public string payment_ip { get; set; }
        public string scheme_id { get; set; }
        public Links _links { get; set; }
        public string maskedCardNumber { get; set; }
    }

    public class Source
    {
        public string id { get; set; }
        public string token { get; set; }
        public string type { get; set; }
        public int expiry_month { get; set; }
        public int expiry_year { get; set; }
        public string name { get; set; }
        public string scheme { get; set; }
        public string last4 { get; set; }
        public string fingerprint { get; set; }
        public string bin { get; set; }
        public string card_type { get; set; }
        public string card_category { get; set; }
        public string issuer { get; set; }
        public string issuer_country { get; set; }
        public string product_id { get; set; }
        public string product_type { get; set; }
        public string avs_check { get; set; }
        public string cvv_check { get; set; }
    }

    public class Risk
    {
        public bool flagged { get; set; }
    }

    public class Customer
    {
        public string id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
    }

    public class BillingDescriptor
    {
        public string name { get; set; }
        public string city { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Actions
    {
        public string href { get; set; }
    }

    public class Refund
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
        public Actions actions { get; set; }
        public Refund refund { get; set; }
    }
}
