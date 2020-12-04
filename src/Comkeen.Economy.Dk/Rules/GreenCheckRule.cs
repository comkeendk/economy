using System;
using Comkeen.Economy.Core.Abstractions;
using Comkeen.Economy.Dk.Rates;

namespace Comkeen.Economy.Dk.Rules
{
    public class GreenCheckRule : DkTaxRuleBase
    {
        public GreenCheckRule(ITaxRatesProvider<DkTaxRates> taxRatesProvider) : base(taxRatesProvider)
        { }

        protected override void ApplyInner(Person person, DanishTaxCalculationBasis basis, DanishTaxCalculationResult result)
        {
            result.GreenCheck[person] = Math.Round(Math.Max(0, _taxRatesProvider.GetRate(basis.Year, DkTaxRates.GroenCheck) + (basis.PersonalIncomeBasis[person] <= _taxRatesProvider.GetRate(basis.Year, DkTaxRates.GroenCheckLavIndkomstMax) ? _taxRatesProvider.GetRate(basis.Year, DkTaxRates.GroenCheckLavIndkomstTillaeg) : 0m) 
                    - (Math.Max(0, basis.PersonalIncomeBasis[person] - _taxRatesProvider.GetRate(basis.Year, DkTaxRates.UdligningsskatNedreGraense)) * _taxRatesProvider.GetRate(basis.Year, DkTaxRates.GroenCheckSupplerendeAftrapningsprocent))), 0);
        }
    }
}