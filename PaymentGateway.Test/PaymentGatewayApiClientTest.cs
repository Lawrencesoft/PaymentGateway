using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGateway.ApiClient;
using PaymentGateway.ApiClient.PaymentGateway;
using PaymentGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Test
{
    [TestClass]
    public class PaymentGatewayApiClientTest
    {
        private readonly IPaymentGatewayApiClient _paymentGatewayApiClient;
        private readonly IConfiguration _configuration;

        public PaymentGatewayApiClientTest()
        {
            _configuration = NSubstitute.Substitute.For<IConfiguration>();
            _configuration.GetSection("PaymentGateway").GetSection("PaymentURL").Value = "https://api.sandbox.checkout.com/";
            _configuration.GetSection("PaymentGateway").GetSection("Publickey").Value = "pk_test_687dcbf2-0df5-4791-8be0-864139900697";
            _configuration.GetSection("PaymentGateway").GetSection("Secretkey").Value = "sk_test_5c453d18-40a7-45dc-b025-98c4b37cecc1";
            _paymentGatewayApiClient = new PaymentGatewayApiClient(_configuration);
        }

        [TestMethod]
        public void SubmitPaymentDetailsAsync_ValidInput_ShouldMatchResult()
        {
            //Arrange
            SubmitPayment submitPayment = new SubmitPayment { amount = 250, currency = "USD", number = "4543474002249996", cvv = "956", expiry_month = 5, expiry_year = 2025, type ="card", name="Lawrence Muthian" };

            //Act
            var response = _paymentGatewayApiClient.SubmitPaymentDetailsAsync(submitPayment).Result;

            //Assert
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsNotNull(response.PaymentDetails);
            Assert.IsNotNull(response.PaymentDetails.id);
            Assert.AreEqual("card", response.PaymentDetails.source.type);
            Assert.AreEqual(250, response.PaymentDetails.amount);
            Assert.AreEqual("USD", response.PaymentDetails.currency);
            Assert.AreEqual("Lawrence Muthian", response.PaymentDetails.source.name);
            Assert.AreEqual(2025, response.PaymentDetails.source.expiry_year);
            Assert.AreEqual(5, response.PaymentDetails.source.expiry_month);
        }

        [TestMethod]
        public void SubmitPaymentDetailsAsync_NullInput_ShouldMatchResult()
        {
            //Arrange

            //Act
            var response = _paymentGatewayApiClient.SubmitPaymentDetailsAsync(null).Result;

            //Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Exception);
            Assert.IsNull(response.PaymentDetails);
            Assert.AreEqual("Exception on retrieving the token",response.Exception.message);
        }

        [TestMethod]
        public void SubmitPaymentDetailsAsync_InvalidInput_ShouldMatchResult()
        {
            //Arrange
            SubmitPayment submitPayment = new SubmitPayment { type = "Invalid Values" };

            //Act
            var response = _paymentGatewayApiClient.SubmitPaymentDetailsAsync(submitPayment).Result;

            //Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Exception);
            Assert.IsNull(response.PaymentDetails);
            Assert.AreEqual("Exception on retrieving the token", response.Exception.message);
        }

        [TestMethod]
        public void SubmitPaymentDetailsAsync_InvalidCurrency_ShouldReturnFailureResponse()
        {
            //Arrange
            SubmitPayment submitPayment = new SubmitPayment { amount = 250, currency = "Invalid", number = "4543474002249996", cvv = "956", expiry_month = 5, expiry_year = 2025, type = "card", name = "Lawrence Muthian" };

            //Act
            var response = _paymentGatewayApiClient.SubmitPaymentDetailsAsync(submitPayment).Result;

            //Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Exception);
            Assert.IsNull(response.PaymentDetails);
            Assert.AreEqual("Unprocessable Entity", response.Exception.message);
        }

        [TestMethod]
        public void GetPaymentDetailsAsync_ValidPayment_ShouldMatchResult()
        {
            //Arrange
            string paymentId = "pay_phtt3fzybelktkunplqypjfvmy";

            //Act
            var response = _paymentGatewayApiClient.GetPaymentDetailsAsync(paymentId).Result;

            //Assert
            Assert.IsNotNull(response);
            Assert.IsNull(response.Exception);
            Assert.IsNotNull(response.PaymentDetails);
            Assert.IsNotNull(response.PaymentDetails.id);
            Assert.AreEqual("pay_phtt3fzybelktkunplqypjfvmy", response.PaymentDetails.id);
            Assert.AreEqual(340, response.PaymentDetails.amount);
            Assert.AreEqual("USD", response.PaymentDetails.currency);
            Assert.AreEqual("Lawrence Muthian", response.PaymentDetails.source.name);
            Assert.AreEqual(2025, response.PaymentDetails.source.expiry_year);
        }

        [TestMethod]
        public void GetPaymentDetailsAsync_EmptyPaymentId_ShouldReturnValidationMessage()
        {
            //Arrange
            string paymentId = "";

            //Act
            var response = _paymentGatewayApiClient.GetPaymentDetailsAsync(paymentId).Result;

            //Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Exception);
            Assert.IsNull(response.PaymentDetails);
            Assert.AreEqual("Method Not Allowed", response.Exception.message);
        }

        [TestMethod]
        public void GetPaymentDetailsAsync_InvalidPayment_ShouldReturnFailureMessage()
        {
            //Arrange
            string paymentId = "InvalidPaymentId";

            //Act
            var response = _paymentGatewayApiClient.GetPaymentDetailsAsync(paymentId).Result;

            //Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Exception);
            Assert.IsNull(response.PaymentDetails);
            Assert.AreEqual("Not Found", response.Exception.message);
        }

    }
}
