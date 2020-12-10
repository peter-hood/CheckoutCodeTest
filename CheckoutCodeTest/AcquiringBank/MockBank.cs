using CheckoutCodeTest.Models;

namespace CheckoutCodeTest.AcquiringBank
{
    public class MockBank : IAcquiringBank
    {
        private int uniqueId = 0;

        public BankResponseDto PaymentIsApproved(PaymentRequestDto paymentRequestDto)
        {
            return new BankResponseDto { id = uniqueId++, status = true };
        }
    }
}
