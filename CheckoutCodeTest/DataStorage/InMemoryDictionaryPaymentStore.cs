using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckoutCodeTest.Models;

namespace CheckoutCodeTest.DataStorage
{
    public class InMemoryDictionaryPaymentStore : IPaymentStorage
    {
        private Dictionary<int, Tuple<PaymentRequestDto, bool>> inMemoryStore;

        public void Retrieve()
        {
            throw new NotImplementedException();
        }

        public void Store(int id, PaymentRequestDto paymentRequest, bool approved)
        {
            inMemoryStore.Add(id, new Tuple<PaymentRequestDto, bool>(paymentRequest, approved));
        }
    }
}
