using Microsoft.Extensions.Configuration;
using PaymentGateway.ApiClient.PaymentGateway;
using System.Configuration;

namespace PaymentGateway.ApiClient
{
    /// <summary>
    /// This class used to build the Http Client object
    /// </summary>
    public class PaymentGatewayClientFactory : IPaymentGatewayClientFactory
    {
        /// <summary>
        /// Checkout payment gateway Api client
        /// </summary>
        private static IPaymentGatewayApiClient _client;

        /// <summary>
        /// This is used to retrieve the value from configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Check out Payment Client Factory Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public PaymentGatewayClientFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// To dispose the http client
        /// </summary>
        public void Dispose()
        {
            _client?.Dispose();
            _client = null;
        }

        /// <summary>
        /// To get the http client object
        /// </summary>
        /// <returns></returns>
        public IPaymentGatewayApiClient GetClient() => _client ?? (_client = CreateNewClient());

        /// <summary>
        /// To create the new http client
        /// </summary>
        /// <returns></returns>
        private IPaymentGatewayApiClient CreateNewClient() => new PaymentGatewayApiClient(_configuration);

    }
}
