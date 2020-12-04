
/*
* This class was generated using a tool.
* Please do not edit the generated class directly!
* Instead apply changes in the template, and re-generate the provider.
*/
using Comkeen.Economy.Core.Abstractions;
using Comkeen.Economy.Dk.Rates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Comkeen.Economy.Dk.Rates
{
    public class DkTaxRatesProvider : ITaxRatesProvider<DkTaxRates>
    {
        private readonly IDictionary<DkTaxRates, SortedDictionary<int, decimal>> _rates;

        public DkTaxRatesProvider()
        {
            _rates = new Dictionary<DkTaxRates, SortedDictionary<int, decimal>>();
            PopulateRates();
        }

        public decimal GetRate(int year, DkTaxRates taxRateKey)
        {
            if(_rates.TryGetValue(taxRateKey, out SortedDictionary<int, decimal> yearDictionary))
            {
                if(yearDictionary.TryGetValue(year, out decimal rate))
                {
                    return rate;
                }
                var yearRange = GetYearRange(yearDictionary);
                if(year < yearRange.First) { return 0m; }
                if(yearRange.Last < year) { return yearDictionary[yearRange.Last]; }
            }
            throw new ArgumentException("The key does not exist", nameof(taxRateKey));
        }

        private void PopulateRates()
        {
            _rates[DkTaxRates.AMBProcent] = new SortedDictionary<int, decimal>()
{
{ 2016, 0.08m },
{ 2017, 0.08m },
{ 2018, 0.08m },
{ 2019, 0.08m },
{ 2020, 0.08m }
};
_rates[DkTaxRates.BeskaeftigelsesfradragMax] = new SortedDictionary<int, decimal>()
{
{ 2016, 28000m },
{ 2017, 30000m },
{ 2018, 34300m },
{ 2019, 37200m },
{ 2020, 39400m }
};
_rates[DkTaxRates.BeskaeftigelsesfradragProcent] = new SortedDictionary<int, decimal>()
{
{ 2016, 0.083m },
{ 2017, 0.0875m },
{ 2018, 0.095m },
{ 2019, 0.101m },
{ 2020, 0.105m }
};
_rates[DkTaxRates.BundskatProcent] = new SortedDictionary<int, decimal>()
{
{ 2016, 0.0908m },
{ 2017, 0.1008m },
{ 2018, 0.1113m },
{ 2019, 0.1213m },
{ 2020, 0.1214m }
};
_rates[DkTaxRates.GroenCheck] = new SortedDictionary<int, decimal>()
{
{ 2016, 950m },
{ 2017, 940m },
{ 2018, 765m },
{ 2019, 525m },
{ 2020, 525m }
};
_rates[DkTaxRates.PersonFradrag] = new SortedDictionary<int, decimal>()
{
{ 2016, 44000m },
{ 2017, 45000m },
{ 2018, 46000m },
{ 2019, 46200m },
{ 2020, 46500m }
};
_rates[DkTaxRates.PersonFradragUnder18] = new SortedDictionary<int, decimal>()
{
{ 2016, 33000m },
{ 2017, 33800m },
{ 2018, 34500m },
{ 2019, 35300m },
{ 2020, 36100m }
};
_rates[DkTaxRates.SkatteloftPersonligIndkomst] = new SortedDictionary<int, decimal>()
{
{ 2016, 0.5195m },
{ 2017, 0.5195m },
{ 2018, 0.5202m },
{ 2019, 0.5205m },
{ 2020, 0.5206m }
};
_rates[DkTaxRates.SundhedsbidragProcent] = new SortedDictionary<int, decimal>()
{
{ 2016, 0.03m },
{ 2017, 0.02m },
{ 2018, 0.01m },
{ 2019, 0m },
{ 2020, 0m }
};
_rates[DkTaxRates.TopskatProcent] = new SortedDictionary<int, decimal>()
{
{ 2016, 0.15m },
{ 2017, 0.15m },
{ 2018, 0.15m },
{ 2019, 0.15m },
{ 2020, 0.15m }
};
_rates[DkTaxRates.TopskatGraense] = new SortedDictionary<int, decimal>()
{
{ 2016, 467300m },
{ 2017, 479600m },
{ 2018, 498900m },
{ 2019, 513400m },
{ 2020, 531000m }
};
_rates[DkTaxRates.NegativKapitalIndkomstMax] = new SortedDictionary<int, decimal>()
{
{ 2016, -50000m },
{ 2017, -50000m },
{ 2018, -50000m },
{ 2019, -50000m },
{ 2020, -50000m }
};
_rates[DkTaxRates.NegativKapitalIndkomstProcent] = new SortedDictionary<int, decimal>()
{
{ 2016, 0.05m },
{ 2017, 0.06m },
{ 2018, 0.07m },
{ 2019, 0.08m },
{ 2020, 0.08m }
};
_rates[DkTaxRates.SkatteloftPositivKapitalIndkomstProcent] = new SortedDictionary<int, decimal>()
{
{ 2016, 0.42m },
{ 2017, 0.42m },
{ 2018, 0.42m },
{ 2019, 0.42m },
{ 2020, 0.42m }
};
_rates[DkTaxRates.BundfradragTopskatKapitalIndkomst] = new SortedDictionary<int, decimal>()
{
{ 2016, 41900m },
{ 2017, 42800m },
{ 2018, 43800m },
{ 2019, 44800m },
{ 2020, 45800m }
};
_rates[DkTaxRates.AktieskatProgressionsgraense] = new SortedDictionary<int, decimal>()
{
{ 2016, 50600m },
{ 2017, 51700m },
{ 2018, 52900m },
{ 2019, 54000m },
{ 2020, 55300m }
};
_rates[DkTaxRates.AktieskatUnderProgressionsgraenseProcent] = new SortedDictionary<int, decimal>()
{
{ 2016, 0.27m },
{ 2017, 0.27m },
{ 2018, 0.27m },
{ 2019, 0.27m },
{ 2020, 0.27m }
};
_rates[DkTaxRates.AktieskatOverProgressionsgraenseProcent] = new SortedDictionary<int, decimal>()
{
{ 2016, 0.42m },
{ 2017, 0.42m },
{ 2018, 0.42m },
{ 2019, 0.42m },
{ 2020, 0.42m }
};
_rates[DkTaxRates.UdbytteskatProcent] = new SortedDictionary<int, decimal>()
{
{ 2016, 0.27m },
{ 2017, 0.27m },
{ 2018, 0.27m },
{ 2019, 0.27m },
{ 2020, 0.27m }
};
_rates[DkTaxRates.UdligningsskatNedreGraense] = new SortedDictionary<int, decimal>()
{
{ 2016, 379900m },
{ 2017, 388200m },
{ 2018, 397.000m },
{ 2019, 405.700m },
{ 2020, 414.700m }
};
_rates[DkTaxRates.GroenCheckAftrapningsprocent] = new SortedDictionary<int, decimal>()
{
{ 2016, 0.075m },
{ 2017, 0.075m },
{ 2018, 0.075m },
{ 2019, 0.075m },
{ 2020, 0.075m }
};
_rates[DkTaxRates.GroenCheckSupplerendeAftrapningsprocent] = new SortedDictionary<int, decimal>()
{
{ 2016, 0.075m },
{ 2017, 0.075m },
{ 2018, 0.075m },
{ 2019, 0.075m },
{ 2020, 0.075m }
};
_rates[DkTaxRates.GroenCheckLavIndkomstTillaeg] = new SortedDictionary<int, decimal>()
{
{ 2016, 280m },
{ 2017, 280m },
{ 2018, 280m },
{ 2019, 280m },
{ 2020, 280m }
};
_rates[DkTaxRates.GroenCheckLavIndkomstMax] = new SortedDictionary<int, decimal>()
{
{ 2016, 222000m },
{ 2017, 226900m },
{ 2018, 232.000m },
{ 2019, 237.100m },
{ 2020, 242.400m }
};
_rates[DkTaxRates.JobfradragMax] = new SortedDictionary<int, decimal>()
{
{ 2016, 0m },
{ 2017, 0m },
{ 2018, 1400m },
{ 2019, 2100m },
{ 2020, 2600m }
};
_rates[DkTaxRates.JobfradragProcent] = new SortedDictionary<int, decimal>()
{
{ 2016, 0m },
{ 2017, 0m },
{ 2018, 0.025m },
{ 2019, 0.0375m },
{ 2020, 0.045m }
};
_rates[DkTaxRates.JobfradragBundgraense] = new SortedDictionary<int, decimal>()
{
{ 2016, 0m },
{ 2017, 0m },
{ 2018, 187500m },
{ 2019, 191600m },
{ 2020, 195800m }
};

        }

        private static (int First, int Last) GetYearRange(SortedDictionary<int, decimal> rates)
        {
            var keys = rates.Keys;

            return (First: keys.First(), Last: keys.Last());
        }
    }
}
