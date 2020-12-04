namespace Comkeen.Economy.Core.Abstractions
{
    public interface ITaxCalculator
    {
        ITaxCalculationResult Calculate(ITaxCalculationBasis basis);
    }
}
