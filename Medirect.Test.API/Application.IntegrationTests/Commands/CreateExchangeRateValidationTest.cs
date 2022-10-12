using FluentAssertions;
using FluentValidation;
using Medirect.Application.Commands.CreateCurrencyConversionTradeComamnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Commands
{
    public class CreateExchangeRateValidationTest : BaseTestsFixture
    {
        [Fact]
        public async Task ShouldRequireNonEmptyFields()
        {
            var command = new CreateCurrencyConversionRequest();
            await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Theory]
        [MemberData(nameof(MultipleTests))]
        public async Task CurrencySymbolShouldbe3CharLong(CreateCurrencyConversionRequest command)
        {
            await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task ShouldCreateExchangeRate()
        {
            var userId = await RunAsDefaultUserAsync();

            var result = await SendAsync(new CreateCurrencyConversionRequest
            {
                BaseAmount = 10,
                BaseCurrency = "eur",
                ToCurrency = "usd"
            });

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(201, result.StatusCode);
        }

        public static IEnumerable<object[]> MultipleTests()
        {
            yield return new object[] {new CreateCurrencyConversionRequest
                {
                  BaseAmount = 1
                } };
            yield return new object[] {new CreateCurrencyConversionRequest
                {
                  BaseAmount = 1,
                  BaseCurrency = "AA",
                  ToCurrency = "ZZ"
                } };
            yield return new object[] {new CreateCurrencyConversionRequest
                {
                  BaseAmount = 1,
                  BaseCurrency = "AAAA",
                  ToCurrency = "ZZZZ"
                } };
            yield return new object[] {new CreateCurrencyConversionRequest
                {
                  BaseAmount = 1,
                  ToCurrency = "ZZ",
                   BaseCurrency = "AA",
                } };
            yield return new object[] {new CreateCurrencyConversionRequest
                {
                  BaseAmount = 1,
                  ToCurrency = "ZZZZ",
                   BaseCurrency = "AA",
                } };
        }
    }
}