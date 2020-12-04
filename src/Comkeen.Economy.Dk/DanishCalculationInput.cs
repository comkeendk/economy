using System;
using Comkeen.Economy.Core.Abstractions.Types;
using Comkeen.Economy.Core.Types;

namespace Comkeen.Economy.Dk
{
    public class DanishCalculationInput
    {
        /// <summary>
        /// Skatteåret.
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Antallet af voksne personer i husstanden.
        /// </summary>
        public int NumberOfPersons { get; set; }
        /// <summary>
        /// De voksne personers fødseldage.
        /// </summary>
        public DateTime[] PersonBirthdays { get; set; } = new DateTime[2];
        /// <summary>
        /// Er personerne i husstanden gift med hinanden.
        /// </summary>
        public bool Married { get; set; }

        /// <summary>
        /// Kommune der benyttes til at regne kommune og kirkeskat ud fra.
        /// </summary>
        public int MunicipalityCode { get; set; }

        /// <summary>
        /// Lønindkomster før AMB.
        /// </summary>
        public IPersonCompositeDecimal Salary { get; set; } = new PersonCompositeDecimal();
        /// <summary>
        /// Indbetaling på privattegnede livsvarige livrenter.
        /// </summary>
        public PersonCompositeDecimal DepositPrivateLifelongAnnuity { get; set; } = new PersonCompositeDecimal();
        /// <summary>
        /// Indbetaling på privattegnede rate- og ophørende livrenter.
        /// </summary>
        public PersonCompositeDecimal DepositPrivateRate { get; set; } = new PersonCompositeDecimal();
        /// <summary>
        /// Indbetaling på privattegnede kapitalpensioner.
        /// </summary>
        public PersonCompositeDecimal DepositPrivateKap { get; set; } = new PersonCompositeDecimal();

        /// <summary>
        /// Renteindtægter.
        /// </summary>
        public PersonCompositeDecimal InterestIncome { get; set; } = new PersonCompositeDecimal();
        /// <summary>
        /// Renteudgifter.
        /// </summary>
        public PersonCompositeDecimal InterestExpenses { get; set; } = new PersonCompositeDecimal();
        /// <summary>
        /// Øvrig kapital indkomst.
        /// </summary>
        public PersonCompositeDecimal CapitalIncome { get; set; } = new PersonCompositeDecimal();

        /// <summary>
        /// Aktieudbytte inkl. udbytteskat. 
        /// </summary>
        public PersonCompositeDecimal StockDividendBeforeTax { get; set; } = new PersonCompositeDecimal();
        /// <summary>
        /// Aktieudbytte ekskl. udbytteskat. 
        /// </summary>
        public PersonCompositeDecimal StockDividendAfterTax { get; set; } = new PersonCompositeDecimal();

        protected void ApplyValues(DanishCalculationInput other)
        {
            Year = other.Year;
            NumberOfPersons = other.NumberOfPersons;
            PersonBirthdays = other.PersonBirthdays;
            Married = other.Married;
            MunicipalityCode = other.MunicipalityCode;
            Salary = other.Salary;
            DepositPrivateLifelongAnnuity = other.DepositPrivateLifelongAnnuity;
            DepositPrivateRate = other.DepositPrivateRate;
            DepositPrivateKap = other.DepositPrivateKap;
            InterestIncome = other.InterestIncome;
            InterestExpenses = other.InterestExpenses;
            CapitalIncome = other.CapitalIncome;
            StockDividendAfterTax = other.StockDividendAfterTax;
            StockDividendBeforeTax = other.StockDividendBeforeTax;
        }
    }
}