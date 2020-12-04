namespace Comkeen.Economy.Core.Abstractions.Types
{
    public interface IPersonCompositeDecimal
    {
        decimal this[Person person] { get; set; }
        decimal this[int personIndex] { get; set; }

        IPersonCompositeDecimal Add(IPersonCompositeDecimal other);
        IPersonCompositeDecimal Subtract(IPersonCompositeDecimal other);
    }
}