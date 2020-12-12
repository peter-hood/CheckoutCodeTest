﻿using CheckoutAPI;
using CheckoutAPI.Models;
using NUnit.Framework;
using System;

namespace CheckoutApiTests
{
    class PaymentRequestValidatorTests
    {
        private PaymentRequestValidator sut;

        private string testCardNumber;
        private DateTime testExpiryDate;
        private string testCurrency;
        private decimal testAmount;
        private string testCvv;
        private PaymentRequestDto testPaymentRequestDto;

        [SetUp]
        public void Setup()
        {
            testCardNumber = "1234567890123456";
            testExpiryDate = DateTime.Now.AddDays(1);
            testCurrency = "GBP";
            testAmount = new decimal(123.45);
            testCvv = "123";
            testPaymentRequestDto = new PaymentRequestDto(testCardNumber, testExpiryDate, testCurrency, testAmount, testCvv);

            sut = new PaymentRequestValidator();
        }

        [Test]
        public void GivenValidParams_IsValid_ReturnsTrue()
        {
            var result = sut.IsValid(testPaymentRequestDto);

            Assert.IsTrue(result);
        }

        [Test]
        public void GivenNull_IsValid_ReturnsFalse()
        {
            var result = sut.IsValid(null);

            Assert.IsFalse(result);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("1")]
        [TestCase("12345678901234567")]
        [TestCase("123456789012345A")]
        [TestCase("A234567890123456")]
        [TestCase("ABCDEFGHIJKLMNOP")]
        public void GivenInvalidCardNumber_IsValid_ReturnsFalse(string localTestCardNumber)
        {
            var testRequest = new PaymentRequestDto(localTestCardNumber, testExpiryDate, testCurrency, testAmount, testCvv);

            var result = sut.IsValid(testRequest);

            Assert.IsFalse(result);
        }

        [Test]
        [TestCase(null)]
        // TODO: expand test cases
        public void GivenInvalidCardNumber_IsValid_ReturnsFalse(DateTime localTestEcpiryDate)
        {
            var testRequest = new PaymentRequestDto(testCardNumber, localTestEcpiryDate, testCurrency, testAmount, testCvv);

            var result = sut.IsValid(testRequest);

            Assert.IsFalse(result);
        }

        // TODO: add similar tests for each param of PaymentRequestDto
    }
}
