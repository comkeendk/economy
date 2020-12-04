using System;
using Comkeen.Economy.Core.Abstractions;
using Comkeen.Economy.Dk.Rates;

namespace Comkeen.Economy.Dk.Rules
{
    public class StockIncomeTaxRule : DkTaxRuleBase
    {
        public StockIncomeTaxRule(ITaxRatesProvider<DkTaxRates> taxRatesProvider)
            : base(taxRatesProvider)
        { }

        protected override void ApplyInner(Person person, DanishTaxCalculationBasis basis, DanishTaxCalculationResult result)
        {
            var progressionsgraense = _taxRatesProvider.GetRate(basis.Year, DkTaxRates.AktieskatProgressionsgraense); //TODO: Udregn for gifte par.
                var stockIncomeTotal = basis.StockDividendBeforeTax[person] + basis.StockDividendAfterTax[person];
                result.StockTax[person] = (Math.Min(stockIncomeTotal, progressionsgraense) * _taxRatesProvider.GetRate(basis.Year, DkTaxRates.AktieskatUnderProgressionsgraenseProcent)) 
                                        + (Math.Max(0, stockIncomeTotal - progressionsgraense) * _taxRatesProvider.GetRate(basis.Year, DkTaxRates.AktieskatOverProgressionsgraenseProcent));
                
                result.StockTax[person] -= Math.Max(0, basis.StockDividendBeforeTax[person]) * _taxRatesProvider.GetRate(basis.Year, DkTaxRates.UdbytteskatProcent);
        }
    }
}