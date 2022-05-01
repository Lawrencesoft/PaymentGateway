using System;

namespace PaymentGateway.ApiClient
{
    /// <summary>
    /// This class used to build the Http Client object
    /// </summary>
    public interface IPaymentGatewayClientFactory:IDisposable
    {
        /// <summary>
        /// To get the http client object
        /// </summary>
        /// <returns></returns>
        IPaymentGatewayApiClient GetClient();
    }
}
