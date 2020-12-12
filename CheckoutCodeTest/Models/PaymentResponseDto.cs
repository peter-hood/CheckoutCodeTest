using CheckoutAPI.AcquiringBank;
using System;

namespace CheckoutAPI.Models
{
    public class PaymentResponseDto
    {
        public PaymentRequestDto paymentRequest { get; private set; }
        public BankResponseDto bankResponse { get; private set; }

        public PaymentResponseDto(PaymentRequestDto paymentRequest, BankResponseDto bankResponse)
        {
            this.paymentRequest = paymentRequest;
            this.bankResponse = bankResponse;
        }

        public override bool Equals(Object obj)
        {
            if (!(obj is PaymentResponseDto other)) return false;
            return (paymentRequest == other.paymentRequest)
                && (bankResponse == other.bankResponse);
        }

        public override int GetHashCode()
        {
            return paymentRequest.GetHashCode()
                + bankResponse.GetHashCode();
        }
    }
}
