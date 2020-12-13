using CheckoutAPI.DataStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CheckoutAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentRetrievalController : ControllerBase
    {
        private IPaymentStorage paymentStorage;

        public PaymentRetrievalController(IPaymentStorage paymentStorage)
        {
            this.paymentStorage = paymentStorage;
        }

        /// <summary>Retrieve payent details by bank response Id.</summary>
        /// <param name="id">Id of the transaction as defined by the acquiring bank.</param>
        /// <returns>
        ///     On valid Id: 200 Succes response with body of type <see cref="PaymentStorageObject"/>.
        ///     On invalid Id: 404 Not Found response.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaymentStorageObject), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RetrievePaymentById(int id)
        {
            var result = await Task.Run(() => { return paymentStorage.Retrieve(id); });

            if (result == null) {
                return NotFound(string.Format("Id {0} not found", id));
            } else {
                return Ok(result);
            }
        }
    }
}