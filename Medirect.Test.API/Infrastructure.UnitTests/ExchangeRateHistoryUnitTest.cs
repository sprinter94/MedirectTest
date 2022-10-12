using Medirect.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace Infrastructure.UnitTests
{
    public class ExchangeRateHistoryUnitTest
    {
        [Fact]
        public async void Add_ExchangeRatesHistory_True()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "exchanges")
            .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DataContext(options))
            {
                context.ExchangeRatesHistories.Add(new Medirect.Domain.ExchangeRatesHistory
                {
                    BaseAmount = 5,
                    BaseCurrency = "eur",
                    DateInserted = System.DateTime.Now,
                    ExchangeRate = 0.95m,
                    Id = 1,
                    ToAmount = 4.85m,
                    ToCurrency = "usd",
                    UserId = 1,
                });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new DataContext(options))
            {
                ExchangeRateHistoryRepository exchangeRateHistoryRepository = new ExchangeRateHistoryRepository(context);
                exchangeRateHistoryRepository.Add(new Medirect.Domain.ExchangeRatesHistory
                {
                    BaseAmount = 5,
                    BaseCurrency = "eur",
                    DateInserted = System.DateTime.Now,
                    ExchangeRate = 0.82m,
                    Id = 2,
                    ToAmount = 4.1m,
                    ToCurrency = "gbp",
                    UserId = 1,
                });

                await exchangeRateHistoryRepository.SaveChangesAsync();

                var all = await exchangeRateHistoryRepository.GetAllAsync();

                Assert.Equal(2, all.Count());
            }
        }

        [Fact]
        public async void GetByCondition_ExchangeRateHistory_True()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "exchanges")
            .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new DataContext(options))
            {
                context.ExchangeRatesHistories.Add(new Medirect.Domain.ExchangeRatesHistory
                {
                    BaseAmount = 5,
                    BaseCurrency = "eur",
                    DateInserted = System.DateTime.Now,
                    ExchangeRate = 0.95m,
                    Id = 1,
                    ToAmount = 4.85m,
                    ToCurrency = "usd",
                    UserId = 1,
                });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new DataContext(options))
            {
                ExchangeRateHistoryRepository exchangeRateHistoryRepository = new ExchangeRateHistoryRepository(context);

                var exchangeList = await exchangeRateHistoryRepository.FindByConditionAsync(x => x.Id == 1);
                var exchange = exchangeList.First();

                Assert.NotNull(exchange);
                Assert.NotNull(exchangeList);
                Assert.Single(exchangeList);
                Assert.Equal(1, exchange.Id);
                Assert.Equal((decimal)4.85, exchange.ToAmount);
                Assert.Equal((decimal)5, exchange.BaseAmount);
                Assert.Equal("eur", exchange.BaseCurrency);
                Assert.Equal((decimal)0.95, exchange.ExchangeRate);
                Assert.Equal("usd", exchange.ToCurrency);
            }
        }
    }
}