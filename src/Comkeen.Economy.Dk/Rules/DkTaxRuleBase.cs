using System.Collections.Generic;
using Comkeen.Economy.Core.Abstractions;
using Comkeen.Economy.Core.Abstractions.Rules;
using Comkeen.Economy.Dk.Rates;

namespace Comkeen.Economy.Dk.Rules
{
    public abstract class DkTaxRuleBase : ITaxRule
    {
        protected readonly ITaxRatesProvider<DkTaxRates> _taxRatesProvider;

        public DkTaxRuleBase(ITaxRatesProvider<DkTaxRates> taxRatesProvider)
        {
            _taxRatesProvider = taxRatesProvider;
        }

        public IEnumerable<string> MustBeAppliedAfter => throw new System.NotImplementedException();

        public void Apply(Person person, ITaxCalculationBasis basis, ITaxCalculationResult result)
        {
            if(basis is DanishTaxCalculationBasis danishBasis && result is DanishTaxCalculationResult danishResult)
            {
                ApplyInner(person, danishBasis, danishResult);
            }
        }

        protected abstract void ApplyInner(Person person, DanishTaxCalculationBasis basis, DanishTaxCalculationResult result);

        protected Person GetPartner(Person person, int numberOfPersons)
        {
            if(numberOfPersons <= 1) { return Person.Person1; }
            if(person == Person.Person1) { return Person.Person2; }
            return Person.Person1;
        }
    }
}