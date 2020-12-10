using System;

namespace CheckoutCodeTest.Models
{
    public class PaymentRequestDto
    {
        public long cardNumber { get; private set; }
        public DateTime expiryDate { get; private set; }
        public string currency { get; private set; }
        public decimal amount { get; private set; }
        public int cvv { get; private set; }

        public PaymentRequestDto(long cardNumber, DateTime expiryDate, string currency, decimal amount, int cvv)
        {
            this.cardNumber = cardNumber;
            this.expiryDate = expiryDate;
            this.currency = currency;
            this.amount = amount;
            this.cvv = cvv;
        }
    }
}
