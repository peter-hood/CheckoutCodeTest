using CheckoutAPI.Models;
using System;

namespace CheckoutAPI.DataStorage
{
    public class PaymentStorageObject
    {
        public bool approved { get; private set; }
        public string maskedCardNumber { get; private set; }
        public int hashedCardNumber { get; private set; } // allows for comparison without knowledge of the card number itself
        public DateTime expiryDate { get; private set; }
        public decimal amount { get; private set; }
        public string currency { get; private set; }

        public PaymentStorageObject(PaymentRequestDto paymentRequest, bool approved)
        {
            maskedCardNumber = paymentRequest.cardNumber.Substring(paymentRequest.cardNumber.Length - 4);
            hashedCardNumber = paymentRequest.cardNumber.GetHashCode();
            expiryDate = paymentRequest.expiryDate;
            amount = paymentRequest.amount;
            currency = paymentRequest.currency;
            this.approved = approved;
        }

        public override bool Equals(Object obj)
        {
            if (!(obj is PaymentStorageObject other)) return false;
            return (maskedCardNumber == other.maskedCardNumber)
                && (hashedCardNumber == other.hashedCardNumber)
                && (expiryDate == other.expiryDate)
                && (amount == other.amount)
                && (currency == other.currency)
                && (approved == other.approved);
        }

        public override int GetHashCode()
        {
            return maskedCardNumber.GetHashCode()
                + hashedCardNumber.GetHashCode()
                + expiryDate.GetHashCode()
                + amount.GetHashCode()
                + currency.GetHashCode()
                + approved.GetHashCode();
        }
    }
}
