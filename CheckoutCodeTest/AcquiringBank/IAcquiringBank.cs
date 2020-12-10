using CheckoutCodeTest.Models;
using System;

namespace CheckoutCodeTest.AcquiringBank
{
    public interface IAcquiringBank
    {
        public BankResponseDto PaymentIsApproved(PaymentRequestDto paymentRequestDto);
    }
}
