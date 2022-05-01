using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Business;
using PaymentGateway.Model;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace PaymentGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentGatewayController : ControllerBase
    {
        /// <summary>
        /// The Payment Gateway Business
        /// </summary>
        private readonly IPaymentGatewayBusiness _paymentGatewayBusiness;

        /// <summary>
        /// Constructer of PaymentController
        /// </summary>
        /// <param name="paymentGatewayBusiness"></param>
        public PaymentGatewayController(IPaymentGatewayBusiness paymentGatewayBusiness)
        {
            _paymentGatewayBusiness = paymentGatewayBusiness;
        }

        /// <summary>
        /// To get the Payment details from Checkout payment service
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPaymentDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentGatewayResponse))]
        public async Task<IActionResult> GetPaymentDetailsAsync(string paymentId)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(paymentId))
                {
                    var response = await _paymentGatewayBusiness.GetPaymentDetailsAsync(paymentId);
                    if (response?.Exception != null)
                    {
                        return StatusCode((int)response.Exception.HttpStatusCode, response.Exception.message);
                    }
                    return StatusCode(StatusCodes.Status200OK, response);
                }
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid request");
            }
            catch (System.Exception ex)
            {
                //TODO: Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To submit the Payment details to Checkout payment service
        /// </summary>
        /// <param name="submitPayment"></param>
        /// <returns></returns>
        [HttpPost("SubmitPaymentDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentGatewayResponse))]
        public async Task<IActionResult> SubmitPaymentDetailsAsync([FromBody] SubmitPayment submitPayment)
        {
            try
            {
                var validation = SubmitRequestValidation(submitPayment);
                if (validation.Count > 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, validation);
                }
                submitPayment.type = "card";
                var response = await _paymentGatewayBusiness.SubmitPaymentDetailsAsync(submitPayment);
                if (response.Exception != null)
                {
                    return StatusCode((int)response.Exception.HttpStatusCode, response.Exception.message);
                }
                return StatusCode(StatusCodes.Status200OK, response);

            }
            catch (System.Exception ex)
            {
                //TODO: Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Validation for Submit Payment Request
        /// </summary>
        /// <param name="submitPayment"></param>
        private List<string> SubmitRequestValidation(SubmitPayment submitPayment)
        {
            var errorMessage = new List<string>();

            var cardCheck = new Regex(@"^\d{16}$");
            //var cardCheck = new Regex(@"^(1298|1267|4512|4567|8901|8933)([\-\s]?[0-9]{4}){3}$");
            var monthCheck = new Regex(@"^\d{1,2}$");
            var yearCheck = new Regex(@"^20[0-9]{2}$");
            var cvvCheck = new Regex(@"^\d{3}$");
            var currencyCode = new Regex(@"^([A-Z]{3})$");

            if (string.IsNullOrWhiteSpace(submitPayment.number) || (!cardCheck.IsMatch(submitPayment.number)))
                errorMessage.Add("Please enter valid card number");
            if (string.IsNullOrWhiteSpace(submitPayment.cvv) || (!cvvCheck.IsMatch(submitPayment.cvv)))
                errorMessage.Add("Please enter valid cvv number");

            //var dateParts = expiryDate.Split('/'); //expiry date in from MM/yyyy            
            if ((submitPayment.expiry_month <= 0) || (!monthCheck.IsMatch(submitPayment.expiry_month.ToString())))
                errorMessage.Add("Please enter valid expiry month");

            if (!yearCheck.IsMatch(submitPayment.expiry_year.ToString()))
                errorMessage.Add("Please enter valid expiry year");
            if (string.IsNullOrWhiteSpace(submitPayment.currency) || (!currencyCode.IsMatch(submitPayment.currency)))
                errorMessage.Add("Please enter valid currency");
            if (submitPayment.amount <= 0)
                errorMessage.Add("Please enter the valid amount");
            return errorMessage;
        }
    }
}
