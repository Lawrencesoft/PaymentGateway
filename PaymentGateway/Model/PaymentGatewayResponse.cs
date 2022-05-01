using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Model
{
    public class PaymentGatewayResponse
    {
        public PaymentDetails PaymentDetails { get; set; }

        public Exception Exception { get; set; }
    }
}
