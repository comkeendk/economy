using System;

namespace Comkeen.Economy.Core.Abstractions
{
    public interface ITaxRatesProvider : ITaxRatesProvider<int>
    { }

    public interface ITaxRatesProvider<TTaxRateKeyType>
    {
        decimal GetRate(int year, TTaxRateKeyType taxRateKey);
    }
}
