using System;
using Comkeen.Economy.Core.Abstractions.Types;

namespace Comkeen.Economy.Core.Abstractions
{
    public interface ITaxCalculationBasis
    {
        int Year { get; set; }
        IPersonCompositeDecimal Salary { get; set; }
        int NumberOfPersons { get; set; }
        DateTime[] PersonBirthdays { get; set; }
        bool Married { get; set; }
    }
}
