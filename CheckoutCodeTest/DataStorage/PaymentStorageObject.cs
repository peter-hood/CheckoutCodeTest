using CheckoutAPI.Models;
using System;

namespace CheckoutAPI.DataStorage
{
    public class PaymentStorageObject
    {
        public string maskedCardNumber { get; private set; }
        public int hashedCardNumber { get; private set; } // allows for comparison without knowledge of the card number itself
        public DateTime expiryDate { get; private set; }
        public decimal amount { get; private set; }
        public string currency { get; private set; }
        public DateTime transactionTime { get; private set; }
        public bool approved { get; private set; }

        public PaymentStorageObject(PaymentRequestDto paymentRequest, bool approved)
        {
            maskedCardNumber = "**** **** **** " + paymentRequest.cardNumber.Substring(paymentRequest.cardNumber.Length - 4);
            hashedCardNumber = paymentRequest.cardNumber.GetHashCode();
            expiryDate = paymentRequest.expiryDate;
            amount = paymentRequest.amount;
            currency = paymentRequest.currency;
            this.approved = approved;
            transactionTime = DateTime.Now;
        }

        public PaymentStorageObject(string maskedCardNumber, int hashedCardNumber, DateTime expiryDate, decimal amount, string currency, DateTime transactionTime, bool approved)
        {
            this.maskedCardNumber = maskedCardNumber;
            this.hashedCardNumber = hashedCardNumber;
            this.expiryDate = expiryDate;
            this.amount = amount;
            this.currency = currency;
            this.transactionTime = transactionTime;
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
                && (transactionTime == other.transactionTime)
                && (approved == other.approved);
        }

        public override int GetHashCode()
        {
            return maskedCardNumber.GetHashCode()
                + hashedCardNumber.GetHashCode()
                + expiryDate.GetHashCode()
                + amount.GetHashCode()
                + currency.GetHashCode()
                + transactionTime.GetHashCode()
                + approved.GetHashCode();
        }
    }
}
