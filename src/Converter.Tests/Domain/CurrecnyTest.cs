using Converter.Domain.Currency;
using Converter.Domain.Exceptions;
using Xunit;

namespace Converter.Tests.Domain;

public class CurrencyTest
{
    [Fact]
    public void Create_Currency()
    {
        // Arrange | Act
        var currency = new Currency("USD");

        // Asset
        Assert.Equal("USD", currency.Code);
    }

    [Fact]
    public void Normalize_Currency_Code()
    {
        // Arrange | Act
        var currency = new Currency("usd");

        // Asset
        Assert.Equal("USD", currency.Code);
    }

    [Fact]
    public void Equal_Currencies_With_Same_Codes()
    {
        // Arrange
        var currency1 = new Currency("USD");
        var currency2 = new Currency("usd");

        // Act
        var actual = currency1.Equals(currency2);

        // Assert
        Assert.True(actual);
    }
    
    [Fact]
    public void Equal_Currencies_HashCodes_With_Same_Codes()
    {
        // Arrange
        var currency1 = new Currency("USD");
        var currency2 = new Currency("usd");

        // Assert
        Assert.True(currency1.GetHashCode() == currency2.GetHashCode());
    }
    
    [Fact]
    public void Not_Equal_Currencies()
    {
        // Arrange
        var currency1 = new Currency("USD");
        var currency2 = new Currency("UAH");

        // Act
        var actual = currency1.Equals(currency2);

        // Assert
        Assert.False(actual);
    }
    
    [Fact]
    public void Not_Equal_Currencies_HasCodes()
    {
        // Arrange
        var currency1 = new Currency("USD");
        var currency2 = new Currency("UAH");

        // Assert
        Assert.False(currency1.GetHashCode() == currency2.GetHashCode());
    }

    [Fact]
    public void Invalid_Empty_Code()
    {
        Assert.Throws<DomainException>(() => new Currency(" "));
    }
    
    [Fact]
    public void Invalid_Long_Code()
    {
        Assert.Throws<DomainException>(() => new Currency("123456"));
    }
    
    [Fact]
    public void Invalid_Short_Code()
    {
        Assert.Throws<DomainException>(() => new Currency("1"));
    }
}
