using PaymentGateway.ApiClient;
using PaymentGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Business
{
    /// <summary>
    /// Class to implement the business logic of Payment gateway
    /// </summary>
    public class PaymentGatewayBusiness : IPaymentGatewayBusiness
    {

        /// <summary>
        /// The Client Factory
        /// </summary>
        private readonly IPaymentGatewayClientFactory _clientFactory;

        /// <summary>
        /// Constructer of Payment Gateway Business class
        /// </summary>
        /// <param name="clientFactory"></param>
        public PaymentGatewayBusiness(IPaymentGatewayClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        /// <summary>
        /// To get the Payment details from Checkout payment service
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        public async Task<PaymentGatewayResponse> GetPaymentDetailsAsync(string paymentId)
        {
            var client = _clientFactory.GetClient();
            var response = await client.GetPaymentDetailsAsync(paymentId);

            return response;
        }

        /// <summary>
        /// To submit the Payment details to Checkout payment service
        /// </summary>
        /// <param name="submitPayment"></param>
        /// <returns></returns>
        public async Task<PaymentGatewayResponse> SubmitPaymentDetailsAsync(SubmitPayment submitPayment)
        {
            var client = _clientFactory.GetClient();
            var response = await client.SubmitPaymentDetailsAsync(submitPayment);

            return response;
        }
    }
}
