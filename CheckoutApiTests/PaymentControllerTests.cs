using CheckoutCodeTest.AcquiringBank;
using CheckoutCodeTest.Controllers;
using CheckoutCodeTest.DataStorage;
using CheckoutCodeTest.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;

namespace CheckoutApiTests
{
    public class PaymentControllerTests
    {
        private PaymentController sut;
        private Mock<IAcquiringBank> bank;
        private Mock<IPaymentStorage> paymentStore;
        private BankResponseDto testBankResponse;
        private int testId = 1;
        private bool testStatus = true;
        private PaymentRequestDto testPaymentRequestDto;

        [SetUp]
        public void Setup()
        {
            bank = new Mock<IAcquiringBank>();
            testBankResponse = new BankResponseDto { id = testId, status = testStatus };
            bank.Setup(x => x.PaymentIsApproved(It.IsAny<PaymentRequestDto>())).Returns(testBankResponse);

            paymentStore = new Mock<IPaymentStorage>();
            paymentStore.Setup(x => x.Store(It.IsAny<int>(), It.IsAny<PaymentRequestDto>(), It.IsAny<bool>()));

            sut = new PaymentController(bank.Object, paymentStore.Object);

            testPaymentRequestDto = new PaymentRequestDto(1234567890123456, new DateTime(), "GBP", new decimal(123.45), 123);
        }

        [Test]
        public void GivenValidParams_MakePayment_ReturnsOkResponse()
        {
            var result = sut.MakePayment(testPaymentRequestDto);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }
    }
}