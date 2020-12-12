using System;
using System.Collections.Generic;
using CheckoutAPI.Models;

namespace CheckoutAPI.DataStorage
{
    public class InMemoryDictionaryPaymentStore : IPaymentStorage
    {
        private Dictionary<int, PaymentStorageObject> inMemoryStore = new Dictionary<int, PaymentStorageObject>();

        public PaymentStorageObject Retrieve(int id)
        {
            return inMemoryStore[id];
        }

        public void Store(int id, PaymentStorageObject paymentObject)
        {
            inMemoryStore.Add(id, paymentObject);
        }
    }
}
