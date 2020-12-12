namespace CheckoutAPI.DataStorage
{
    public interface IPaymentStorage
    {
        public void Store(int id, PaymentStorageObject paymentDetails);
        public PaymentStorageObject Retrieve(int id);
    }
}
