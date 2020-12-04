using Comkeen.Economy.Core.Abstractions;
using Comkeen.Economy.Core.Types;
using System;
using Xunit;

namespace Comkeen.Economy.Core.Tests.Types
{
    public class CashflowTests
    {
        public class Constructor : CashflowTests
        {
            [Fact]
            public void GivenValueAndDate_AddCorrectly()
            {
                const decimal value = 1m;
                
                var cashflow = new Cashflow(value, DateTime.Today);

                Assert.Equal(value, cashflow[DateTime.Today]);
            }

            [Fact]
            public void GivenValueAndFrequency_AddsCorrectly()
            {
                const decimal value = 1m;
                const Frequency frequency = Frequency.Quarterly;
                var firstOfJanuary = new DateTime(DateTime.Today.Year, 1, 1);
                
                var cashflow = new Cashflow(value, frequency);

                Assert.Equal(value, cashflow[firstOfJanuary]);
                Assert.Equal(value, cashflow[firstOfJanuary.AddMonths(3)]);
                Assert.Equal(value, cashflow[firstOfJanuary.AddMonths(6)]);
                Assert.Equal(value, cashflow[firstOfJanuary.AddMonths(9)]);
            }

            [Fact]
            public void GivenValueAndFrequencyAndStartDateAndEndDate_AddsCorrectly()
            {
                const decimal value = 1m;
                const Frequency frequency = Frequency.Quarterly;
                var firstOfJanuary = new DateTime(DateTime.Today.Year, 1, 1);
                var endOfYear = firstOfJanuary.AddYears(1).AddDays(-1);
                
                var cashflow = new Cashflow(value, frequency, firstOfJanuary, endOfYear);

                Assert.Equal(value, cashflow[firstOfJanuary]);
                Assert.Equal(value, cashflow[firstOfJanuary.AddMonths(3)]);
                Assert.Equal(value, cashflow[firstOfJanuary.AddMonths(6)]);
                Assert.Equal(value, cashflow[firstOfJanuary.AddMonths(9)]);
            }
        }

        public class Add : CashflowTests
        {
            [Fact]
            public void GivenValueAndDate_AddCorrectly()
            {
                const decimal value = 1m;
                var cashflow = new Cashflow(0m, DateTime.Today.AddDays(1));

                cashflow.Add(value, DateTime.Today);

                Assert.Equal(value, cashflow[DateTime.Today]);
            }

            [Fact]
            public void GivenValueAndDate_WhenAddingToSameDate_AddCorrectly()
            {
                const decimal value = 1m;
                var cashflow = new Cashflow(value, DateTime.Today);

                cashflow.Add(value, DateTime.Today);

                Assert.Equal(value * 2, cashflow[DateTime.Today]);
            }

            [Fact]
            public void GivenValueAndFrequencyAndDateSpan_AddsCorrectly()
            {
                const decimal value = 1m;
                const Frequency frequency = Frequency.Quarterly;
                var firstOfJanuary = new DateTime(DateTime.Today.Year, 1, 1);
                var endOfYear = firstOfJanuary.AddYears(1).AddDays(-1);
                var cashflow = new Cashflow(0m, firstOfJanuary);

                cashflow.Add(value, frequency, firstOfJanuary, endOfYear);

                Assert.Equal(value, cashflow[firstOfJanuary]);
                Assert.Equal(value, cashflow[firstOfJanuary.AddMonths(3)]);
                Assert.Equal(value, cashflow[firstOfJanuary.AddMonths(6)]);
                Assert.Equal(value, cashflow[firstOfJanuary.AddMonths(9)]);
            }
        }

        public class Sum : CashflowTests
        {
            [Fact]
            public void SumsAllValues()
            {
                const decimal value = 1m;
                const Frequency frequency = Frequency.Quarterly;
                var cashflow = new Cashflow(value, frequency);

                var sum = cashflow.Sum();

                Assert.Equal(value * (int)frequency, sum);
            }

            [Fact]
            public void GivenDates_SumsValuesWithRange()
            {
                const decimal value = 1m;
                const Frequency frequency = Frequency.Quarterly;
                var start = new DateTime(DateTime.Today.Year, 1, 1);
                var end = start.AddYears(1).AddDays(-1);
                var cashflow = new Cashflow(value, frequency, start, end);

                var sum = cashflow.Sum(start.AddDays(1), end);

                Assert.Equal((value * (int)frequency) - value, sum);
            }

            [Fact]
            public void GivenDates_WhenStartDateIsAfterEndDate_ThrowsException()
            {
                const decimal value = 1m;
                const Frequency frequency = Frequency.Quarterly;
                var cashflow = new Cashflow(value, frequency);

                Assert.Throws<ArgumentOutOfRangeException>(() => cashflow.Sum(DateTime.Today, DateTime.Today.AddDays(-5)));
            }
        }
    }
}