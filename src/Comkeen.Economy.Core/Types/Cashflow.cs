using System;
using System.Collections.Generic;
using System.Linq;
using Comkeen.Economy.Core.Abstractions;

namespace Comkeen.Economy.Core.Types
{
    public class Cashflow
    {
        private readonly IDictionary<DateTime, decimal> _values = new Dictionary<DateTime, decimal>();

        public decimal this[DateTime date] => _values[date.Date];

        public Cashflow(decimal value, DateTime date)
        {
            Add(value, date);
        }

        public Cashflow(decimal valuePerFrequency, Frequency frequency)
            : this(valuePerFrequency, frequency, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31))
        { }

        public Cashflow(decimal valuePerFrequency, Frequency frequency, DateTime start, DateTime end)
        {
            Add(valuePerFrequency, frequency, start, end);
        }

        public void Add(decimal value, DateTime date)
        {
            if (_values.TryGetValue(date.Date, out _))
            {
                _values[date.Date] += value;
            }
            else
            {
                _values[date.Date] = value;
            }
        }

        public void Add(decimal value, Frequency frequency, DateTime start, DateTime end)
        {
            if(start.Date >= end.Date) { throw new ArgumentOutOfRangeException(nameof(start), $"{nameof(start)} must be before {nameof(end)}"); }
            if(frequency == Frequency.Custom) { throw new ArgumentException("Frequency must be set to one of the recurring options", nameof(frequency)); }

            var monthsToAdd = 12 / (int)frequency;
            var date = start.Date.AddMonths(0);

            while (date.Date <= end.Date)
            {
                Add(value, date.Date);

                date = date.AddMonths(monthsToAdd);
            }
        }

        public decimal Sum()
        {
            return _values.Values.Sum();
        }

        public decimal Sum(DateTime start, DateTime end)
        {
            if(start.Date >= end.Date) { throw new ArgumentOutOfRangeException(nameof(start), $"{nameof(start)} must be before {nameof(end)}"); }
            
            return _values
                .Where(kvp => start.Date <= kvp.Key.Date && kvp.Key.Date <= end)
                .Sum(kvp => kvp.Value);
        }
    }
}