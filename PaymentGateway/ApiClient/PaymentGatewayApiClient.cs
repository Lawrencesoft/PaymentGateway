using Microsoft.Extensions.Configuration;
using PaymentGateway.Model;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PaymentGateway.ApiClient.PaymentGateway
{
    /// <summary>
    /// This Payment Gateway Api Client is used to invoke the Checkout payment service.
    /// </summary>
    public sealed class PaymentGatewayApiClient : IPaymentGatewayApiClient
    {
        /// <summary>
        /// Checkout payment base address
        /// </summary>
        private readonly string _baseAddress;

        /// <summary>
        /// Secret Api Key
        /// </summary>
        private readonly string _secretKey;

        /// <summary>
        /// Public Api Key
        /// </summary>
        private readonly string _publicKey;

        /// <summary>
        /// Http Client for token
        /// </summary>
        private static readonly HttpClient tokenClient = new HttpClient();

        /// <summary>
        /// Http Client for Checkout service
        /// </summary>
        private static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// Checkout Api client constructor
        /// </summary>
        /// <param name="configuration"></param>
        public PaymentGatewayApiClient(IConfiguration configuration)
        {
            _baseAddress = configuration.GetSection("PaymentGateway").GetSection("PaymentURL").Value;
            _secretKey = configuration.GetSection("PaymentGateway").GetSection("Secretkey").Value;
            _publicKey = configuration.GetSection("PaymentGateway").GetSection("Publickey").Value;
            SetAuthorizationKey();
        }


        /// <summary>
        /// To submit the Payment details to Checkout payment service
        /// </summary>
        /// <returns></returns>
        public async Task<PaymentGatewayResponse> SubmitPaymentDetailsAsync(SubmitPayment submitPayment)
        {
            var paymentGatewayResponse = new PaymentGatewayResponse();
            const string route = "payments/";
            try
            {
                var tokenKey = await GetTokenKey(submitPayment);
                if (!string.IsNullOrWhiteSpace(tokenKey?.token))
                {
                    submitPayment.source = new Source { type = "token", token = tokenKey?.token };
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string requestUri = _baseAddress + route;
                    HttpContent httpContent = new StringContent(JsonSerializer.Serialize(submitPayment), Encoding.UTF8);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync(requestUri, httpContent);
                    var contentString = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var paymentDetails = JsonSerializer.Deserialize<PaymentDetails>(contentString);
                        paymentDetails.maskedCardNumber = string.Format("XXXX-XXXX-XXXX-{0}", paymentDetails.source.last4);
                        paymentGatewayResponse.PaymentDetails = paymentDetails;
                    }
                    else
                    {
                        var exception = new Model.Exception { HttpStatusCode = response.StatusCode, message = response.ReasonPhrase, Type = "Exception" };
                        paymentGatewayResponse.Exception = exception;
                    }
                }
                else
                {
                    var exception = new Model.Exception { HttpStatusCode = System.Net.HttpStatusCode.InternalServerError, message = "Exception on retrieving the token", Type = "Exception" };
                    paymentGatewayResponse.Exception = exception;
                }
            }
            catch (System.Exception ex)
            {
                // TODO: Log the exception
                var exception = new Model.Exception { HttpStatusCode = System.Net.HttpStatusCode.InternalServerError, message = ex.Message, Type = "Exception" };
                paymentGatewayResponse.Exception = exception;
            }
            return paymentGatewayResponse;
        }

        /// <summary>
        /// To get the Payment details from Checkout payment service
        /// </summary>
        /// <returns></returns>
        public async Task<PaymentGatewayResponse> GetPaymentDetailsAsync(string paymentId)
        {
            var paymentGatewayResponse = new PaymentGatewayResponse();
            const string route = "payments/";
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string requestUri = _baseAddress + route + paymentId;
                var response = await client.GetAsync(requestUri);
                var contentString = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var paymentDetails = JsonSerializer.Deserialize<Model.PaymentDetails>(contentString);
                    paymentDetails.maskedCardNumber = string.Format("XXXX-XXXX-XXXX-{0}", paymentDetails.source.last4);
                    paymentGatewayResponse.PaymentDetails = paymentDetails;
                }
                else
                {
                    var exception = new Model.Exception { HttpStatusCode = response.StatusCode, message = response.ReasonPhrase, Type = "Exception" };
                    paymentGatewayResponse.Exception = exception;
                }
            }
            catch (System.Exception ex)
            {
                // TODO: Log the exception
                var exception = new Model.Exception { HttpStatusCode = System.Net.HttpStatusCode.InternalServerError, message = ex.Message, Type = "Exception" };
                paymentGatewayResponse.Exception = exception;
            }
            return paymentGatewayResponse;
        }

        /// <summary>
        /// Dispose the client object
        /// </summary>
        public void Dispose()
        {
            client?.Dispose();
        }

        /// <summary>
        /// To set the httpcliet Authorization key
        /// </summary>
        private void SetAuthorizationKey()
        {
            try
            {
                tokenClient.DefaultRequestHeaders.Add("Authorization", _publicKey);
                client.DefaultRequestHeaders.Add("Authorization", _secretKey);
            }
            catch (System.Exception)
            {
                //TODO: Log the exception
            }
        }

        /// <summary>
        /// To retrieve the token key from Checkout payment service
        /// </summary>
        /// <returns></returns>
        private async Task<Token> GetTokenKey(SubmitPayment submitPayment)
        {
            try
            {
                tokenClient.DefaultRequestHeaders.Accept.Clear();
                tokenClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string requestUri = _baseAddress + "tokens";
                HttpContent httpContent = new StringContent(JsonSerializer.Serialize(submitPayment), Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await tokenClient.PostAsync(requestUri, httpContent);
                var contentString = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<Token>(contentString);
                }
            }
            catch (System.Exception)
            {
                // TODO: Log the exception on retrieve the token
            }
            return null;
        }
    }
}
