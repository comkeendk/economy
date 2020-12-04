using System;
using Comkeen.Economy.Core.Abstractions;
using Comkeen.Economy.Dk.Rates;
using Xunit;

namespace Comkeen.Economy.Dk.Tests
{
    public class DanishTaxCalculatorBasisTests
    {
        private const int calculationYear = 2016;
        private DanishTaxCalculationBasis TaxCalculationBasis => new DanishTaxCalculationBasis(new DkTaxRatesProvider())
        {
            Year = calculationYear,
            NumberOfPersons = 1
        };

        public class Constructor : DanishTaxCalculatorBasisTests
        {
            [Fact]
            public void WhenRatesProvider_IsNull_ThrowsException()
            {
                Assert.Throws<ArgumentNullException>(() => new DanishTaxCalculationBasis(null));
            }
        }

        public class PersonalIncomeBasis : DanishTaxCalculatorBasisTests
        {
            [Fact]
            public void SubtractsAmb()
            {
                var basis = TaxCalculationBasis;
                basis.Salary[Person.Person1] = 100m;

                Assert.Equal(92m, basis.PersonalIncomeBasis[Person.Person1]);
            }

            [Fact]
            public void SubtractsPrivateKapitalpensionDeposits()
            {
                var basis = TaxCalculationBasis;
                basis.Salary[Person.Person1] = 100m;
                basis.DepositPrivateKap[Person.Person1] = 10m;

                Assert.Equal(82m, basis.PersonalIncomeBasis[Person.Person1]);
            }

            [Fact]
            public void SubtractsPrivateRatepensionDeposits()
            {
                var basis = TaxCalculationBasis;
                basis.Salary[Person.Person1] = 100m;
                basis.DepositPrivateRate[Person.Person1] = 10m;

                Assert.Equal(82m, basis.PersonalIncomeBasis[Person.Person1]);
            }

            [Fact]
            public void SubtractsPrivateLifelongAnnuityDeposits()
            {
                var basis = TaxCalculationBasis;
                basis.Salary[Person.Person1] = 100m;
                basis.DepositPrivateLifelongAnnuity[Person.Person1] = 10m;

                Assert.Equal(82m, basis.PersonalIncomeBasis[Person.Person1]);
            }
        }
    }
}