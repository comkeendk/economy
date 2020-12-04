using Comkeen.Economy.Core.Abstractions;
using Comkeen.Economy.Core.Types;

namespace Comkeen.Economy.Dk
{
    public class DanishTaxCalculationResult : ITaxCalculationResult
    {
        /// <summary>
        /// The year of the calculation.
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Payed AMB (arbejdsmarkedsbidrag).
        /// Index 0 = Payed AMB for person 1.
        /// Index 1 = Payed AMB for person 2.
        /// Index 2 = Sum of index 0 and 1.
        /// </summary>
        public decimal[] AMB { get; set; } = new decimal[3] { 0m, 0m, 0m };

        /// <summary>
        /// The calculated municipality and church tax, and health contribution.
        /// </summary>
        public PersonCompositeDecimal MunicipalityChurchTax { get; } = new PersonCompositeDecimal();
        /// <summary>
        /// The calculated bottom tax.
        /// </summary>
        public PersonCompositeDecimal BottomTax { get; } = new PersonCompositeDecimal();
        /// <summary>
        /// The calculated top marginal tax.
        /// </summary>
        public PersonCompositeDecimal TopTax { get; } = new PersonCompositeDecimal();
        /// <summary>
        /// The calculated marginal tax for net capital income.
        /// </summary>
        public PersonCompositeDecimal TopTaxCapitalIncome { get; } = new PersonCompositeDecimal();
        /// <summary>
        /// The calculated deduction when having negative net capital income.
        /// </summary>
        public PersonCompositeDecimal DeductionNegativeNetCapitalIncome { get; } = new PersonCompositeDecimal();
        /// <summary>
        /// The calculated tax on stock dividend.
        /// </summary>
        public PersonCompositeDecimal StockTax { get; } = new PersonCompositeDecimal();
        /// <summary>
        /// Calculated deduction "Grøn check".
        /// </summary>
        public PersonCompositeDecimal GreenCheck { get; } = new PersonCompositeDecimal();

        public decimal GetTotal(Person person)
        {
            return MunicipalityChurchTax[person] 
                + BottomTax[person] 
                + (TopTax[person] - DeductionNegativeNetCapitalIncome[person])
                + StockTax[person] 
                - GreenCheck[person];
        }
    }
}
