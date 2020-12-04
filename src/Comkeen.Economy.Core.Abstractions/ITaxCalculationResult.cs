namespace Comkeen.Economy.Core.Abstractions
{
    public interface ITaxCalculationResult
    {
        int Year { get; set; }
        decimal GetTotal(Person person);
    }
}
