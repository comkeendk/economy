using System;
using Comkeen.Economy.Core.Abstractions;
using Comkeen.Economy.Dk.Rates;

namespace Comkeen.Economy.Dk.Rules
{
    public class DeductionNegativeNetCapitalIncomeRule : DkTaxRuleBase
    {
        public DeductionNegativeNetCapitalIncomeRule(ITaxRatesProvider<DkTaxRates> taxRatesProvider)
            : base(taxRatesProvider)
        { }

        protected override void ApplyInner(Person person, DanishTaxCalculationBasis basis, DanishTaxCalculationResult result)
        {
            if (basis.NetCapitalIncome[person] < 0 && (!basis.Married || (basis.Married && basis.NetCapitalIncome[Person.Both] < 0)))
            {
                var partnerIndex = GetPartner(person, basis.NumberOfPersons);
                var kapitalIndkomstNedslag = Math.Abs(Math.Max(_taxRatesProvider.GetRate(basis.Year, DkTaxRates.NegativKapitalIndkomstMax) *
                    (basis.Married && (basis.NetCapitalIncome[person] < 0 && basis.NetCapitalIncome[partnerIndex] >= 0) || (basis.NetCapitalIncome[person] >= 0 && basis.NetCapitalIncome[partnerIndex] < 0) ? 2 : 1),
                    Math.Min(0, basis.Married && basis.NetCapitalIncome[person] >= 0 && basis.NetCapitalIncome[Person.Both] < 0 ? basis.NetCapitalIncome[Person.Both] : basis.NetCapitalIncome[person])) * _taxRatesProvider.GetRate(basis.Year, DkTaxRates.NegativKapitalIndkomstProcent));
                result.DeductionNegativeNetCapitalIncome[person] = kapitalIndkomstNedslag;
            }
        }
    }
}