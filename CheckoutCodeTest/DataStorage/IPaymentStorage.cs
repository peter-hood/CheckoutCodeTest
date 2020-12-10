
using CheckoutCodeTest.Models;

namespace CheckoutCodeTest.DataStorage
{
    public interface IPaymentStorage
    {
        public void Store(int id, PaymentRequestDto paymentRequest, bool approved);
        public void Retrieve();
    }
}
