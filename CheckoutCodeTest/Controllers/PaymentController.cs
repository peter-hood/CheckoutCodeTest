using CheckoutAPI.DataStorage;
using CheckoutAPI.AcquiringBank;
using CheckoutAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace CheckoutAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        IAcquiringBank acquiringBank;
        IPaymentStorage paymentStorage;
        IPaymentRequestValidator paymentRequestValidator;

        public PaymentController(IAcquiringBank acquiringBank, IPaymentStorage paymentStorage, IPaymentRequestValidator paymentRequestValidator)
        {
            this.acquiringBank = acquiringBank;
            this.paymentStorage = paymentStorage;
            this.paymentRequestValidator = paymentRequestValidator;
        }

        /// <summary>Attempts to make a payment with bank authorization and payment storage</summary>
        /// <param name="paymentRequest">The details of the payment being attempted.</param>
        /// <returns>
        ///     On succesful payment: a 200 OK response with body <see cref="PaymentResponseDto"/>.
        ///     On failed payment: returns 401 Unauthorized response.
        ///     On invalid request: returns 400 Bad Request response.
        ///     On failure with acquiring bank: returns 500 Internal Server Error response.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(typeof(PaymentResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MakePayment(PaymentRequestDto paymentRequest)
        {
            IActionResult result;
            try {
                if (paymentRequest == null || !paymentRequestValidator.IsValid(paymentRequest)) { 
                    return BadRequest(paymentRequest); 
                }

                var bankResponse = await Task.Run(() => { return acquiringBank.PaymentIsApproved(paymentRequest); });
                await Task.Run(() => { paymentStorage.Store(bankResponse.id, new PaymentStorageObject(paymentRequest, bankResponse.status)); });

                if (bankResponse.status) {
                    result = Ok(new PaymentResponseDto(paymentRequest, bankResponse));
                } else {
                    result = StatusCode(StatusCodes.Status401Unauthorized);
                }

                return result;
            } catch(Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}