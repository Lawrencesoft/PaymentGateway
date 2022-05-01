using PaymentGateway.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentGateway.ApiClient
{
    /// <summary>
    /// Client used to invoke the Checkout Payment service to store and retrieve the Payment details.
    /// </summary>
    public interface IPaymentGatewayApiClient:IDisposable
    {
        /// <summary>
        /// To get the Payment details from Checkout Payment service
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        Task<PaymentGatewayResponse> GetPaymentDetailsAsync(string paymentId);

        /// <summary>
        /// To store the Payment details in Checkout Payment gateway
        /// </summary>
        /// <param name="submitPayment"></param>
        /// <returns></returns>
        Task<PaymentGatewayResponse> SubmitPaymentDetailsAsync(SubmitPayment submitPayment);
    }
}
