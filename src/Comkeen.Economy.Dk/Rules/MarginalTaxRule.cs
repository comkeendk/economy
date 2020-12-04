using System;
using Comkeen.Economy.Core.Abstractions;
using Comkeen.Economy.Dk.Rates;

namespace Comkeen.Economy.Dk.Rules
{
    public class MarginalTaxRule : DkTaxRuleBase
    {
        private readonly IMunicipalityService  _municipalityService;

        public MarginalTaxRule(ITaxRatesProvider<DkTaxRates> taxRatesProvider, IMunicipalityService municipalityService)
            : base(taxRatesProvider)
        {
            _municipalityService = municipalityService ?? throw new ArgumentNullException(nameof(municipalityService));
        }

        protected override void ApplyInner(Person person, DanishTaxCalculationBasis basis, DanishTaxCalculationResult result)
        {
            var municipality = _municipalityService.GetMunicipality(basis.MunicipalityCode, basis.Year);
            var samletSkatteProcent = municipality.GetTotal(false) + _taxRatesProvider.GetRate(basis.Year, DkTaxRates.SundhedsbidragProcent) + _taxRatesProvider.GetRate(basis.Year, DkTaxRates.BundskatProcent) + _taxRatesProvider.GetRate(basis.Year, DkTaxRates.TopskatProcent);

            result.TopTax[person] = Math.Max(0, basis.PersonalIncomeBasis[person] - _taxRatesProvider.GetRate(basis.Year, DkTaxRates.TopskatGraense)) * (_taxRatesProvider.GetRate(basis.Year, DkTaxRates.TopskatProcent) - Math.Max(0, samletSkatteProcent - _taxRatesProvider.GetRate(basis.Year, DkTaxRates.SkatteloftPersonligIndkomst)));

            var bundfradragKapitalIndkomstTop = _taxRatesProvider.GetRate(basis.Year, DkTaxRates.BundfradragTopskatKapitalIndkomst);
            var topskatKapitalIndkomst = 0m;
            if(basis.Married)
            {
                if(basis.NetCapitalIncome[Person.Both] > bundfradragKapitalIndkomstTop * 2)
                {
                    var applyTaxToPerson = basis.PersonalIncomeBasis[Person.Person1] >= basis.PersonalIncomeBasis[Person.Person2] ? Person.Person1 : Person.Person2;
                    if(person == applyTaxToPerson)
                    {
                        topskatKapitalIndkomst = CalculateMarginalTaxOnPositiveCapitalIncome(basis.Year, basis.NetCapitalIncome[Person.Both], bundfradragKapitalIndkomstTop * 2, basis.PersonalIncomeBasis[person], samletSkatteProcent);
                    }
                }
            }
            else if (basis.NetCapitalIncome[person] > 0)
            {
                topskatKapitalIndkomst = CalculateMarginalTaxOnPositiveCapitalIncome(basis.Year, basis.NetCapitalIncome[person], bundfradragKapitalIndkomstTop, basis.PersonalIncomeBasis[person], samletSkatteProcent);
            }

            result.TopTaxCapitalIncome[person] = topskatKapitalIndkomst;
            result.TopTax[person] += topskatKapitalIndkomst;
        }

        private decimal CalculateMarginalTaxOnPositiveCapitalIncome(int year, decimal positiveCapitalIncome, decimal capitalIncomeDeductionMarginalTax, decimal personalIncome, decimal taxPercent)
        {
            return Math.Max(0, positiveCapitalIncome 
                - capitalIncomeDeductionMarginalTax
                - Math.Max(0, _taxRatesProvider.GetRate(year, DkTaxRates.TopskatGraense) - personalIncome)) *
                (_taxRatesProvider.GetRate(year, DkTaxRates.TopskatProcent) - Math.Max(0, taxPercent - _taxRatesProvider.GetRate(year, DkTaxRates.SkatteloftPositivKapitalIndkomstProcent)));
        }
    }
}