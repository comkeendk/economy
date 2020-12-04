using Comkeen.Economy.Core.Abstractions;
using Comkeen.Economy.Core.Abstractions.Rules;
using Comkeen.Economy.Dk.Rates;
using Comkeen.Economy.Dk.Rules;
using Comkeen.Economy.Dk.Tests.Utilities;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Comkeen.Economy.Dk.Tests
{
    public class DanishTaxCalculatorTests
    {
        private const string marriedCouplesStubPath = "../../../Resources/TaxCalculationBasis/CoupleMarried/";
        private const string singlePersonStubPath = "../../../Resources/TaxCalculationBasis/OnePerson/";

        [Theory]
        [MemberData(nameof(CalculationData.SinglePerson), MemberType = typeof(CalculationData))]
        public void Calculate_OnePerson_GivenCalculationBasis_ReturnsExpected(int year, string persona, decimal expectedResult)
        {
            var calculationBasis = LoadCalculationBasis(year, persona, singlePersonStubPath);
            
            var result = CreateTaxCalculator().Calculate(calculationBasis);

            Assert.NotEqual(0m, result.GetTotal(Person.Person1));
            Assert.Equal(expectedResult, result.GetTotal(Person.Person1), 2);
        }

        [Theory]
        [MemberData(nameof(CalculationData.MarriedCouples), MemberType = typeof(CalculationData))]
        public void Calculate_MarriedCouples_GivenCalculationBasis_ReturnsExpected(int year, string persona, decimal expectedResult)
        {
            var calculationBasis = LoadCalculationBasis(year, persona, marriedCouplesStubPath);
            
            var result = CreateTaxCalculator().Calculate(calculationBasis);

            Assert.NotEqual(0m, result.GetTotal(Person.Person1));
            Assert.Equal(expectedResult, result.GetTotal(Person.Both), 2);
        }

        private static DanishTaxCalculator CreateTaxCalculator()
        {
            return new DanishTaxCalculator(CreateTaxRules());
        }

        private static IList<ITaxRule> CreateTaxRules()
        {
            var taxProvider = new DkTaxRatesProvider();
            var municipalityService = new DkMunicipalityService();

            return new List<ITaxRule>()
            {
                new MunicipalityAndChurchTaxRule(taxProvider, municipalityService),
                new BottomTaxRule(taxProvider),
                new MarginalTaxRule(taxProvider, municipalityService),
                new DeductionNegativeNetCapitalIncomeRule(taxProvider),
                new StockIncomeTaxRule(taxProvider),
                new GreenCheckRule(taxProvider)
            };
        }

        private static DanishTaxCalculationBasis LoadCalculationBasis(int year, string persona, string stubRoot)
        {
            var completePath = Path.Combine(stubRoot, $"{persona}.json");

            var input = SerializationHelper.DeserializeFromFile<DanishCalculationInput>(completePath);
            input.Year = year;

            return new DanishTaxCalculationBasis(new DkTaxRatesProvider(), input);
        }
    }

    internal class CalculationData
    {
        private static readonly Dictionary<string, List<(int Year, decimal ExpectedResult)>> _marriedCouples = new Dictionary<string, List<(int Year, decimal ExpectedResult)>>()
        {
            { 
                "basis-salary", 
                new List<(int Year, decimal ExpectedResult)>()
                {
                    (2016, 180084.80m),
                    (2017, 178945.20m)
                }
            },
            { 
                "basis-salary-one-with-high-income", 
                new List<(int Year, decimal ExpectedResult)>()
                {
                    (2016, 242669.08m),
                    (2017, 239674.48m)
                }
            },
            { 
                "basis-salary-one-with-negative-netcapital-income", 
                new List<(int Year, decimal ExpectedResult)>()
                {
                    (2016, 164434.80m),
                    (2017, 163295.20m)
                }
            },
            { 
                "basis-salary-both-with-negative-netcapital-income", 
                new List<(int Year, decimal ExpectedResult)>()
                {
                    (2016, 151914.80m),
                    (2017, 150775.20m)
                }
            },
            { 
                "basis-salary-positive-and-negative-netcapital-income", 
                new List<(int Year, decimal ExpectedResult)>()
                {
                    (2016, 187160.80m),
                    (2017, 186021.20m)
                }
            },
            { 
                "basis-salary-interestincome-both-marginal-income", 
                new List<(int Year, decimal ExpectedResult)>()
                {
                    (2016, 398576.64m),
                    (2017, 393598.88m)
                }
            },
            { 
                "basis-salary-interestincome-both-marginal-income-one-over-capital-limit", 
                new List<(int Year, decimal ExpectedResult)>()
                {
                    (2016, 377326.64m),
                    (2017, 372348.88m)
                }
            }
        };

        private static readonly Dictionary<string, List<(int Year, decimal ExpectedResult)>> _singlePerson = new Dictionary<string, List<(int Year, decimal ExpectedResult)>>()
        {
            {
                "basis-salary",
                new List<(int Year, decimal ExpectedResult)>()
                {
                    (2016, 185071.40m)
                }
            },
            {
                "basis-salary-medium-income",
                new List<(int Year, decimal ExpectedResult)>()
                {
                    (2016, 74582.90m)
                }
            },
            {
                "basis-salary-medium-income-deduction-green-check",
                new List<(int Year, decimal ExpectedResult)>()
                {
                    (2016, 115286.60m)
                }
            },
            {
                "basis-salary-interestincome",
                new List<(int Year, decimal ExpectedResult)>()
                {
                    (2016, 192147.40m)
                }
            },
            {
                "basis-salary-interestincome-marginal",
                new List<(int Year, decimal ExpectedResult)>()
                {
                    (2016, 207588.12m)
                }
            },
            {
                "basis-salary-interestincome-interestexpenses",
                new List<(int Year, decimal ExpectedResult)>()
                {
                    (2016, 175681.4m)
                }
            }
        };

        public static IEnumerable<object[]> SinglePerson
        {
            get
            {
                return EnumerateCollection(_singlePerson);
            }
        }

        public static IEnumerable<object[]> MarriedCouples
        {
            get
            {
                return EnumerateCollection(_marriedCouples);
            }
        }

        private static IEnumerable<object[]> EnumerateCollection(Dictionary<string, List<(int Year, decimal ExpectedResult)>> collection)
        {
            foreach (var personaYearsPair in collection)
            {
                foreach (var (Year, ExpectedResult) in personaYearsPair.Value)
                {
                    yield return new object[] { Year, personaYearsPair.Key, ExpectedResult };
                }
            }
        }
    }
}