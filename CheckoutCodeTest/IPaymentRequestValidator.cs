using CheckoutAPI.Models;

namespace CheckoutAPI
{
    public interface IPaymentRequestValidator
    {
        public bool IsValid(PaymentRequestDto paymentRequest);
    }
}