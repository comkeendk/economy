using Comkeen.Economy.Core.Abstractions;
using Comkeen.Economy.Core.Abstractions.Types;
using Comkeen.Economy.Core.Types;
using Comkeen.Economy.Dk.Rates;
using System;

namespace Comkeen.Economy.Dk
{
    public class DanishTaxCalculationBasis : DanishCalculationInput, ITaxCalculationBasis
    {
        private readonly ITaxRatesProvider<DkTaxRates> _ratesProvider;

        public DanishTaxCalculationBasis(ITaxRatesProvider<DkTaxRates> ratesProvider)
        {
            _ratesProvider = ratesProvider ?? throw new ArgumentNullException(nameof(ratesProvider));
        }

        public DanishTaxCalculationBasis(ITaxRatesProvider<DkTaxRates> ratesProvider, DanishCalculationInput input)
            : this(ratesProvider)
        {
            if(input == null) { throw new ArgumentNullException(nameof(input)); }

            ApplyValues(input);
        }

        /// <summary>
        /// Skattepligtig indkomst.
        /// </summary>
        public IPersonCompositeDecimal TaxableIncomeBasis => CalculateTaxableIncomeBasis();
        /// <summary>
        /// Personlig indkomst.
        /// </summary>
        public IPersonCompositeDecimal PersonalIncomeBasis => CalculatePersonalIncome();
        /// <summary>
        /// Nettokapital indkomst.
        /// </summary>
        public IPersonCompositeDecimal NetCapitalIncome => CalculateNetCapitalIncome();
        /// <summary>
        /// Ligningsmæssige fradrag.
        /// </summary>
        public IPersonCompositeDecimal LinearDeductions => CalculateLinearDeductions();

        private IPersonCompositeDecimal CalculatePersonalIncome()
        {
            var personalIncome = new decimal[2] { Salary[Person.Person1], Salary[Person.Person2] };

            for (int i = 0; i < NumberOfPersons; i++)
            {
                personalIncome[i] -= Math.Max(0m, Salary[i] * _ratesProvider.GetRate(Year, DkTaxRates.AMBProcent));
                personalIncome[i] -= Math.Max(0m, DepositPrivateKap[i]);
                personalIncome[i] -= Math.Max(0m, DepositPrivateRate[i]);
                personalIncome[i] -= Math.Max(0m, DepositPrivateLifelongAnnuity[i]);
            }

            return new PersonCompositeDecimal(personalIncome);
        }

        private IPersonCompositeDecimal CalculateNetCapitalIncome()
        {
            return InterestIncome
                .Add(CapitalIncome)
                .Subtract(InterestExpenses);
        }

        private IPersonCompositeDecimal WorkAndJobDeductionBasis => Salary
                    .Subtract(DepositPrivateKap)
                    .Subtract(DepositPrivateRate)
                    .Subtract(DepositPrivateLifelongAnnuity);

        private IPersonCompositeDecimal CalculateWorkDeduction()
        {
            return new PersonCompositeDecimal(new decimal[2]
            {
                Math.Min(
                    _ratesProvider.GetRate(Year, DkTaxRates.BeskaeftigelsesfradragMax), 
                    Math.Max(0m, WorkAndJobDeductionBasis[Person.Person1] * _ratesProvider.GetRate(Year, DkTaxRates.BeskaeftigelsesfradragProcent))
                ),
                Math.Min(
                    _ratesProvider.GetRate(Year, DkTaxRates.BeskaeftigelsesfradragMax), 
                    Math.Max(0m, WorkAndJobDeductionBasis[Person.Person2] * _ratesProvider.GetRate(Year, DkTaxRates.BeskaeftigelsesfradragProcent))
                )
            });
        }

        private IPersonCompositeDecimal CalculateJobDeduction()
        {
            return new PersonCompositeDecimal(new decimal[2]
            {
                Math.Min(
                    _ratesProvider.GetRate(Year, DkTaxRates.JobfradragMax),
                    Math.Max(
                        0m,
                        Math.Max(0m, (WorkAndJobDeductionBasis[Person.Person1] - _ratesProvider.GetRate(Year, DkTaxRates.JobfradragBundgraense)) * _ratesProvider.GetRate(Year, DkTaxRates.JobfradragProcent))
                    )
                ),
                Math.Min(
                    _ratesProvider.GetRate(Year, DkTaxRates.JobfradragMax),
                    Math.Max(
                        0m,
                        Math.Max(0m, (WorkAndJobDeductionBasis[Person.Person2] - _ratesProvider.GetRate(Year, DkTaxRates.JobfradragBundgraense)) * _ratesProvider.GetRate(Year, DkTaxRates.JobfradragProcent))
                    )
                )
            });
        }

        private IPersonCompositeDecimal CalculateLinearDeductions()
        {
            return CalculateWorkDeduction()
                .Add(CalculateJobDeduction());
        }

        private IPersonCompositeDecimal CalculateTaxableIncomeBasis()
        {
            return PersonalIncomeBasis.Add(NetCapitalIncome).Subtract(LinearDeductions);
        }
    }
}
