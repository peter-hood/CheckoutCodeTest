using CheckoutCodeTest.AcquiringBank;
using CheckoutCodeTest.DataStorage;
using CheckoutCodeTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CheckoutCodeTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        IAcquiringBank acquiringBank;
        IPaymentStorage paymentStorage;

        public PaymentController(IAcquiringBank acquiringBank, IPaymentStorage paymentStorage)
        {
            this.acquiringBank = acquiringBank;
            this.paymentStorage = paymentStorage;
        }

        /// <summary>Attempts to make a payment with bank authorization and payment storage</summary>
        /// <param name="paymentRequest">The details of the payment being attempted.</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PaymentResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public async Task<IActionResult> MakePayment(PaymentRequestDto paymentRequest)
        {
            IActionResult result;

            var bankResponse = await Task.Run(() => { return acquiringBank.PaymentIsApproved(paymentRequest); });

            if (bankResponse.status) {
                result = Ok(new PaymentResponseDto());
            } else {
                result = StatusCode(StatusCodes.Status406NotAcceptable);
            }

            paymentStorage.Store(bankResponse.id, paymentRequest, bankResponse.status);

            return result;
        }
    }
}