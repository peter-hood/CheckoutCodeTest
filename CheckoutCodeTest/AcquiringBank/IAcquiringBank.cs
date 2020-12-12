using CheckoutAPI.Models;
using System;

namespace CheckoutAPI.AcquiringBank
{
    public interface IAcquiringBank
    {
        public BankResponseDto PaymentIsApproved(PaymentRequestDto paymentRequestDto);
    }
}
