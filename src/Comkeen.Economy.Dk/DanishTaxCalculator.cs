using Comkeen.Economy.Core.Abstractions;
using Comkeen.Economy.Core.Abstractions.Rules;
using Comkeen.Economy.Dk.Rates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Comkeen.Economy.Dk
{
    public class DanishTaxCalculator : ITaxCalculator
    {
        private readonly ITaxRule[] _taxRules;

        public DanishTaxCalculator(IEnumerable<ITaxRule> taxRules)
        {
            _taxRules = taxRules?.ToArray() ?? throw new ArgumentNullException(nameof(taxRules));

            if(_taxRules.Length == 0) { throw new ArgumentException("No tax rules to apply", nameof(taxRules)); }
        }

        public ITaxCalculationResult Calculate(ITaxCalculationBasis basis)
        {
            var result = new DanishTaxCalculationResult();

            if (!(basis is DanishTaxCalculationBasis))
            {
                throw new ArgumentException("basis must be of type DanishTaxCalculationBasis", nameof(basis));
            }

            for (int i = 0; i < basis.NumberOfPersons; i++)
            {
                var person = (Person)i;
                foreach (var rule in _taxRules)
                {
                    rule.Apply(person, basis, result);
                }
            }

            return result;
        }
    }
}
