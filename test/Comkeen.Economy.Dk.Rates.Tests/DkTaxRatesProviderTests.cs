using Xunit;

namespace Comkeen.Economy.Dk.Rates.Tests
{
    public class DkTaxRatesProviderTests
    {
        private readonly DkTaxRatesProvider _provider;

        public DkTaxRatesProviderTests()
        {
            _provider = new DkTaxRatesProvider();
        }

        public class GetRate : DkTaxRatesProviderTests
        {
            [Fact]
            public void GivenYear_WhenYearIsBeforeFirstRegisteredYear_ReturnsZero()
            {
                var yearBeforeFirstEntry = 2000;

                var rate = _provider.GetRate(yearBeforeFirstEntry, DkTaxRates.AMBProcent);

                Assert.Equal(0, rate);
            }

            [Fact]
            public void GivenYear_WhenYearIsInCollection_ReturnsRate()
            {
                const decimal expectedRate = 0.08m;
                const int year = 2016;

                var rate = _provider.GetRate(year, DkTaxRates.AMBProcent);

                Assert.Equal(expectedRate, rate);
            }

            [Fact]
            public void GivenYear_WhenYearIsAfterLastRegisteredYear_ReturnsLastKnowEntry()
            {
                var highestYear = int.MaxValue;

                var rate = _provider.GetRate(highestYear, DkTaxRates.AMBProcent);
                
                Assert.NotEqual(0, rate);
            }
        }
    }
}