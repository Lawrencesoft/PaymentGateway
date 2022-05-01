using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using PaymentGateway.Business;
using PaymentGateway.Controllers;
using PaymentGateway.Model;
using System.Collections;
using System.Collections.Generic;

namespace PaymentGateway.Test
{
    [TestClass]
    public class PaymentGatewayControllerTest
    {
        /// <summary>
        /// The Payment Gateway Business
        /// </summary>
        private readonly IPaymentGatewayBusiness _paymentGatewayBusiness;
        private readonly PaymentGatewayController _paymentController;

        public PaymentGatewayControllerTest()
        {
            _paymentGatewayBusiness = Substitute.For<IPaymentGatewayBusiness>();
            _paymentController = new PaymentGatewayController(_paymentGatewayBusiness);
        }

        [TestMethod]
        public void GetPaymentDetailsAsync_ValidPayment_ShouldMatchResult()
        {
            //Arrange
            string paymentId = "pay_phtt3fzybelktkunplqypjfvmy";
            var request = new PaymentGatewayResponse { PaymentDetails = new PaymentDetails { id = "pay_phtt3fzybelktkunplqypjfvmy", payment_type = "card", amount = 340, currency = "USD", source = new Source { name = "Lawrence Muthian", expiry_year = 2025 } } };
            _paymentGatewayBusiness.GetPaymentDetailsAsync(paymentId).Returns(request);

            //Act
            var response = _paymentController.GetPaymentDetailsAsync(paymentId).Result;

            //Assert
            Assert.IsNotNull(response);
            ObjectResult okResult = response as ObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            var paymentResponse = okResult.Value as PaymentGatewayResponse;
            Assert.AreEqual("pay_phtt3fzybelktkunplqypjfvmy", paymentResponse.PaymentDetails.id);
            Assert.AreEqual("card", paymentResponse.PaymentDetails.payment_type);
            Assert.AreEqual(340, paymentResponse.PaymentDetails.amount);
            Assert.AreEqual("USD", paymentResponse.PaymentDetails.currency);
            Assert.AreEqual("Lawrence Muthian", paymentResponse.PaymentDetails.source.name);
            Assert.AreEqual(2025, paymentResponse.PaymentDetails.source.expiry_year);
        }

        [TestMethod]
        public void GetPaymentDetailsAsync_EmptyPaymentId_ShouldReturnValidationMessage()
        {
            //Arrange
            string paymentId = "";
            
            //Act
            var response = _paymentController.GetPaymentDetailsAsync(paymentId).Result;

            //Assert
            Assert.IsNotNull(response);
            ObjectResult okResult = response as ObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode);
            var validationMsg = okResult.Value as string;
            Assert.AreEqual("Invalid request", validationMsg);
        }

        [TestMethod]
        public void GetPaymentDetailsAsync_InvalidPayment_ShouldReturnFailureMessage()
        {
            //Arrange
            string paymentId = "InvalidPaymentId";
            var request = new PaymentGatewayResponse { Exception = new Exception { HttpStatusCode = System.Net.HttpStatusCode.NotFound,message="Not Found" } };
            _paymentGatewayBusiness.GetPaymentDetailsAsync(paymentId).Returns(request);

            //Act
            var response = _paymentController.GetPaymentDetailsAsync(paymentId).Result;

            //Assert
            Assert.IsNotNull(response);
            ObjectResult okResult = response as ObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(404, okResult.StatusCode);
            var validationMsg = okResult.Value as string;
            Assert.AreEqual("Not Found", validationMsg);
        }

        [TestMethod]
        public void SubmitPaymentDetails_InvlidRequest_ShouldMatchValidationResult()
        {
            //Arrange
            SubmitPayment submitPayment = new SubmitPayment();

            //Act
            var response = _paymentController.SubmitPaymentDetailsAsync(submitPayment).Result;

            //Assert
            Assert.IsNotNull(response);
            ObjectResult okResult = response as ObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode);
            var result = okResult.Value as IList;
            Assert.IsTrue(result.Contains("Please enter valid card number"));
            Assert.IsTrue(result.Contains("Please enter valid cvv number"));
            Assert.IsTrue(result.Contains("Please enter valid expiry month"));
            Assert.IsTrue(result.Contains("Please enter valid expiry year"));
            Assert.IsTrue(result.Contains("Please enter valid currency"));
            Assert.IsTrue(result.Contains("Please enter the valid amount"));
        }

        [TestMethod]
        public void SubmitPaymentDetails_ValidRequest_ShouldMatchResult()
        {
            //Arrange
            SubmitPayment submitPayment = new SubmitPayment {amount = 250,currency="USD",number= "4543474002249996", cvv="956" , expiry_month = 5, expiry_year = 2025 };
            var request = new PaymentGatewayResponse { PaymentDetails = new PaymentDetails { id = "pay_phtt3fzybelktkunplqypjfvmy", payment_type = "card", amount = 250, currency = "USD", source = new Source { name = "Lawrence Muthian", expiry_year = 2025 } } };
            _paymentGatewayBusiness.SubmitPaymentDetailsAsync(submitPayment).Returns(request);

            //Act
            var response = _paymentController.SubmitPaymentDetailsAsync(submitPayment).Result;

            //Assert
            Assert.IsNotNull(response);
            ObjectResult okResult = response as ObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            var paymentResponse = okResult.Value as PaymentGatewayResponse;
            Assert.AreEqual("pay_phtt3fzybelktkunplqypjfvmy", paymentResponse.PaymentDetails.id);
            Assert.AreEqual("card", paymentResponse.PaymentDetails.payment_type);
            Assert.AreEqual(250, paymentResponse.PaymentDetails.amount);
            Assert.AreEqual("USD", paymentResponse.PaymentDetails.currency);
            Assert.AreEqual("Lawrence Muthian", paymentResponse.PaymentDetails.source.name);
            Assert.AreEqual(2025, paymentResponse.PaymentDetails.source.expiry_year);
        }
    }
}
