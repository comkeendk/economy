using System.Collections.Generic;

namespace Comkeen.Economy.Core.Abstractions.Rules
{
    public interface ITaxRule
    {
        void Apply(Person person, ITaxCalculationBasis basis, ITaxCalculationResult result);
        IEnumerable<string> MustBeAppliedAfter { get; }
    }
}