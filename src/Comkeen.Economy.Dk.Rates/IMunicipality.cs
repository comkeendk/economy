namespace Comkeen.Economy.Dk.Rates
{
    public interface IMunicipality
    {
        int Id { get; }
        string Name { get; }
        /// <summary>
        /// Kommuneskat.
        /// </summary>
        decimal MunicipalTax { get; }
        /// <summary>
        /// Kirkeskat.
        /// </summary>
        decimal ChurchTax { get; }

        decimal GetTotal(bool includeChurchTax = true);
    }
}