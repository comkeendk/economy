using System;

namespace Comkeen.Economy.Dk.Rates
{
    internal class Municipality : IMunicipality
    {
        public int Id { get; }
        public string Name { get; }
        public decimal MunicipalTax { get; }
        public decimal ChurchTax { get; }

        public Municipality(int id, string name, decimal municipalTax, decimal churchTax)
        {
            if(id <= 100 || 1000 <= id) { throw new ArgumentOutOfRangeException(nameof(id), "The municipality id must be a number between 100 and 999 (both included)"); }
            if(municipalTax <= 0) { throw new ArgumentException($"{nameof(municipalTax)} must be greater than zero", nameof(municipalTax)); }
            if(churchTax <= 0) { throw new ArgumentException($"{nameof(churchTax)} must be greater than zero", nameof(churchTax)); }

            Id = id;
            Name = name;
            MunicipalTax = municipalTax;
            ChurchTax = churchTax;
        }

        public decimal GetTotal(bool includeChurchTax = true)
        {
            return MunicipalTax + (includeChurchTax ? ChurchTax : 0m);
        }
    }
}