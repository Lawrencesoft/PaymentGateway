using PaymentGateway.Model;
using System.Threading.Tasks;

namespace PaymentGateway.Business
{
    /// <summary>
    /// Interface for Checkout payment gateway business logic
    /// </summary>
    public interface IPaymentGatewayBusiness
    {
        /// <summary>
        /// To get the Payment details from Checkout payment service
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        public Task<PaymentGatewayResponse> GetPaymentDetailsAsync(string paymentId);

        /// <summary>
        /// To submit the Payment details to Checkout payment service
        /// </summary>
        /// <param name="submitPayment"></param>
        /// <returns></returns>
        public Task<PaymentGatewayResponse> SubmitPaymentDetailsAsync(SubmitPayment submitPayment);
    }
}
