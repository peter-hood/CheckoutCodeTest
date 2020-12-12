using CheckoutAPI.DataStorage;
using CheckoutAPI.AcquiringBank;
using CheckoutAPI.Controllers;
using CheckoutAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using Microsoft.AspNetCore.Http;
using CheckoutAPI;

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
        private string testCardNum = "1234567890123456";
        private DateTime testExpiry;
        private string testCurrency = "GBP";
        private decimal testAmount = new decimal(123.45);
        private string testCvv = "123";

        private Mock<IPaymentRequestValidator> paymentRequestValidator;

        private PaymentResponseDto testPaymentResponse;

        [SetUp]
        public void Setup()
        {
            bank = new Mock<IAcquiringBank>();
            testBankResponse = new BankResponseDto(testId, testStatus);
            bank.Setup(x => x.PaymentIsApproved(It.IsAny<PaymentRequestDto>())).Returns(testBankResponse);

            paymentStore = new Mock<IPaymentStorage>();
            paymentStore.Setup(x => x.Store(It.IsAny<int>(), It.IsAny<PaymentStorageObject>()));

            paymentRequestValidator = new Mock<IPaymentRequestValidator>();
            paymentRequestValidator.Setup(x => x.IsValid(It.IsAny<PaymentRequestDto>())).Returns(true);

            sut = new PaymentController(bank.Object, paymentStore.Object, paymentRequestValidator.Object);

            testExpiry = DateTime.Now.AddDays(1);
            testPaymentRequestDto = new PaymentRequestDto(testCardNum, testExpiry, testCurrency, testAmount, testCvv);
            testPaymentResponse = new PaymentResponseDto(testPaymentRequestDto, testBankResponse);
        }

        [Test]
        public void GivenValidParams_MakePayment_ReturnsOkResponse()
        {
            var result = sut.MakePayment(testPaymentRequestDto);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void GivenValidParams_MakePayment_ReturnsPaymentResponseDtoAsResponseBody()
        {
            var result = sut.MakePayment(testPaymentRequestDto);

            Assert.IsInstanceOf<PaymentResponseDto>((result.Result as OkObjectResult).Value);
            var responseBody = (result.Result as OkObjectResult).Value as PaymentResponseDto;

            Assert.AreEqual(testPaymentResponse, responseBody);
        }

        [Test]
        public void GivenInvalidParams_MakePayment_Returns400BadRequest()
        {
            var paymentRequestValidator = new Mock<IPaymentRequestValidator>();
            paymentRequestValidator.Setup(x => x.IsValid(It.IsAny<PaymentRequestDto>())).Returns(false);
            var sut = new PaymentController(bank.Object, paymentStore.Object, paymentRequestValidator.Object);

            var result = sut.MakePayment(testPaymentRequestDto);

            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);            
        }

        [Test]
        public void GivenFalseAuthorizationFromBank_MakePayment_Returns401UnauthorizedResponse()
        {
            var bank = new Mock<IAcquiringBank>();
            var testBankResponse = new BankResponseDto(testId, false);
            bank.Setup(x => x.PaymentIsApproved(It.IsAny<PaymentRequestDto>())).Returns(testBankResponse);
            var sut = new PaymentController(bank.Object, paymentStore.Object, paymentRequestValidator.Object);

            var result = sut.MakePayment(testPaymentRequestDto);

            Assert.IsInstanceOf<StatusCodeResult>(result.Result);
            Assert.AreEqual(StatusCodes.Status401Unauthorized, (result.Result as StatusCodeResult).StatusCode);
        }
    }
}