using CheckoutAPI.Models;
using System;

namespace CheckoutAPI.DataStorage
{
    public class PaymentStorageObject
    {
        public PaymentRequestDto paymentRequest { get; private set; }
        public bool approved { get; private set; }

        public PaymentStorageObject(PaymentRequestDto paymentRequest, bool approved)
        {
            this.paymentRequest = paymentRequest;
            this.approved = approved;
        }

        public override bool Equals(Object obj)
        {
            if (!(obj is PaymentStorageObject other)) return false;
            return (paymentRequest == other.paymentRequest) && (approved == other.approved);
        }

        public override int GetHashCode()
        {
            return paymentRequest.GetHashCode() + approved.GetHashCode();
        }
    }
}
