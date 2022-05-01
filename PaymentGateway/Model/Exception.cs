using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
namespace PaymentGateway.Model
{
    public class Exception
    {

        /// <summary>
        /// Error Message
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// Error Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Http Status Code
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; set; }

    }
}
