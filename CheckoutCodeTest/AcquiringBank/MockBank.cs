using CheckoutAPI.Models;

namespace CheckoutAPI.AcquiringBank
{
    public class MockBank : IAcquiringBank
    {
        private int uniqueId = 0;

        public BankResponseDto PaymentIsApproved(PaymentRequestDto paymentRequestDto)
        {
            return new BankResponseDto(uniqueId++,  true);
        }
    }
}
