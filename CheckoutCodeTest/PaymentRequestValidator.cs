using CheckoutAPI.Models;
using System;

namespace CheckoutAPI
{
    public class PaymentRequestValidator : IPaymentRequestValidator
    {
        public bool IsValid(PaymentRequestDto paymentRequest)
        {
            if (paymentRequest == null
                || paymentRequest.cardNumber == null
                || paymentRequest.expiryDate == null
                || paymentRequest.currency == null
                || paymentRequest.cvv == null) 
            { return false; }

            return paymentRequest.cardNumber.Length >= 13
                && paymentRequest.cardNumber.Length <= 19
                && IsNumeric(paymentRequest.cardNumber)
                && paymentRequest.expiryDate > DateTime.Now
                && paymentRequest.currency.Length == 3  // assuming ISO 4217 standard
                && !IsNumeric(paymentRequest.currency)
                && paymentRequest.amount > 0
                && paymentRequest.cvv.Length == 3
                && IsNumeric(paymentRequest.cvv);
        }

        private bool IsNumeric(string input)
        {
            return long.TryParse(input, out _);
        }
    }
}
