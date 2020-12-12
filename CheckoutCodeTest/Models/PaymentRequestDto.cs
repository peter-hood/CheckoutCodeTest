using System;

namespace CheckoutAPI.Models
{
    public class PaymentRequestDto
    {
        public string cardNumber { get; private set; }
        public DateTime expiryDate { get; private set; }
        public string currency { get; private set; }
        public decimal amount { get; private set; }
        public string cvv { get; private set; }

        public PaymentRequestDto(string cardNumber, DateTime expiryDate, string currency, decimal amount, string cvv)
        {
            this.cardNumber = cardNumber;
            this.expiryDate = expiryDate;
            this.currency = currency;
            this.amount = amount;
            this.cvv = cvv;
        }

        public override bool Equals(Object obj)
        {
            if (!(obj is PaymentRequestDto other)) return false;
            return (cardNumber == other.cardNumber) 
                && (expiryDate == other.expiryDate)
                && (currency == other.currency)
                && (amount == other.amount)
                && (cvv == other.cvv);
        }

        public override int GetHashCode()
        {
            return cardNumber.GetHashCode()
                + expiryDate.GetHashCode()
                + currency.GetHashCode()
                + amount.GetHashCode()
                + cvv.GetHashCode();
        }
    }
}
