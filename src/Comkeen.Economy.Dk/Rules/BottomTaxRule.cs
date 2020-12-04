using System;
using Comkeen.Economy.Core.Abstractions;
using Comkeen.Economy.Dk.Rates;

namespace Comkeen.Economy.Dk.Rules
{
    public class BottomTaxRule : DkTaxRuleBase
    {
        public BottomTaxRule(ITaxRatesProvider<DkTaxRates> taxRatesProvider)
            : base(taxRatesProvider)
        { }

        protected override void ApplyInner(Person person, DanishTaxCalculationBasis basis, DanishTaxCalculationResult result)
        {
            result.BottomTax[person] = (basis.PersonalIncomeBasis[person] - _taxRatesProvider.GetRate(basis.Year, DkTaxRates.PersonFradrag) + 
                    Math.Max(0, 
                        basis.Married && basis.NetCapitalIncome[GetPartner(person, basis.NumberOfPersons)] < 0 
                        ? basis.NetCapitalIncome[Person.Both] 
                        : basis.NetCapitalIncome[person])) * _taxRatesProvider.GetRate(basis.Year, DkTaxRates.BundskatProcent);
        }
    }
}