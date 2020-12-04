using System;
using Comkeen.Economy.Core.Abstractions;
using Comkeen.Economy.Dk.Rates;

namespace Comkeen.Economy.Dk.Rules
{
    public class MunicipalityAndChurchTaxRule : DkTaxRuleBase
    {
        private readonly IMunicipalityService _municipalityService;

        public MunicipalityAndChurchTaxRule(ITaxRatesProvider<DkTaxRates> taxRatesProvider, IMunicipalityService municipalityService)
            : base(taxRatesProvider)
        {
            _municipalityService = municipalityService ?? throw new ArgumentNullException(nameof(municipalityService));
        }

        protected override void ApplyInner(Person person, DanishTaxCalculationBasis basis, DanishTaxCalculationResult result)
        {
            var municipality = _municipalityService.GetMunicipality(basis.MunicipalityCode, basis.Year);

            result.MunicipalityChurchTax[person] = (basis.TaxableIncomeBasis[person] - _taxRatesProvider.GetRate(basis.Year, DkTaxRates.PersonFradrag)) * (municipality.GetTotal() + _taxRatesProvider.GetRate(basis.Year, DkTaxRates.SundhedsbidragProcent));
        }
    }
}