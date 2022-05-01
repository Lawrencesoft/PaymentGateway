using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using PaymentGateway.ApiClient;
using PaymentGateway.Business;
using PaymentGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Test
{
    [TestClass]
    public class PaymentGatewayBusinessTest
    {
        /// <summary>
        /// The Client Factory
        /// </summary>
        private readonly IPaymentGatewayClientFactory _clientFactory;

        private readonly IPaymentGatewayBusiness _paymentGatewayBusiness;

        private readonly IPaymentGatewayApiClient _paymentGatewayApiClient;

        public PaymentGatewayBusinessTest()
        {
            _paymentGatewayApiClient = NSubstitute.Substitute.For<IPaymentGatewayApiClient>();
            _clientFactory = NSubstitute.Substitute.For<IPaymentGatewayClientFactory>();
            _paymentGatewayBusiness = new PaymentGatewayBusiness(_clientFactory);
        }

        [TestMethod]
        public void GetPaymentDetailsAsync_ValidPayment_ShouldMatchResult()
        {
            //Arrange
            string paymentId = "pay_phtt3fzybelktkunplqypjfvmy";
            var paymentGatewayResponse = new PaymentGatewayResponse { PaymentDetails = new PaymentDetails { id = "pay_phtt3fzybelktkunplqypjfvmy", payment_type = "card", amount = 340, currency = "USD", source = new Source { name = "Lawrence Muthian", expiry_year = 2025 } } };

            _paymentGatewayApiClient.GetPaymentDetailsAsync(paymentId).Returns(paymentGatewayResponse);
            _clientFactory.GetClient().Returns(_paymentGatewayApiClient);

            //Act
            var response = _paymentGatewayBusiness.GetPaymentDetailsAsync(paymentId).Result;

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
        public void SubmitPaymentDetails_ValidRequest_ShouldMatchResult()
        {
            //Arrange
            SubmitPayment submitPayment = new SubmitPayment { amount = 250, currency = "USD", number = "4543474002249996", cvv = "956", expiry_month = 5, expiry_year = 2025 };
            var paymentGatewayResponse = new PaymentGatewayResponse { PaymentDetails = new PaymentDetails { id = "pay_phtt3fzybelktkunplqypjfvmy", payment_type = "card", amount = 250, currency = "USD", source = new Source { name = "Lawrence Muthian", expiry_year = 2025 } } };
            
            _paymentGatewayApiClient.SubmitPaymentDetailsAsync(submitPayment).Returns(paymentGatewayResponse);
            _clientFactory.GetClient().Returns(_paymentGatewayApiClient);

            //Act
            var response = _paymentGatewayBusiness.SubmitPaymentDetailsAsync(submitPayment).Result;

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("pay_phtt3fzybelktkunplqypjfvmy", response.PaymentDetails.id);
            Assert.AreEqual("card", response.PaymentDetails.payment_type);
            Assert.AreEqual(250, response.PaymentDetails.amount);
            Assert.AreEqual("USD", response.PaymentDetails.currency);
            Assert.AreEqual("Lawrence Muthian", response.PaymentDetails.source.name);
            Assert.AreEqual(2025, response.PaymentDetails.source.expiry_year);
        }
    }
}
