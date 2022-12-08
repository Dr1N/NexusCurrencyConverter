using Converter.Domain.Currency;
using Converter.Domain.Exceptions;
using Xunit;

namespace Converter.Tests.Domain;

public class CurrencyRateTests
{
    [Fact]
    public void Create_Rate_For_Different_Currencies()
    {
        // Arrange
        var currency1 = new Currency("CR1");
        var currency2 = new Currency("CR2");
        const decimal rate = decimal.One;
        
        // Act
        var actual = new CurrencyRate(currency1, currency2, rate);
        
        // Assert
        Assert.True(actual.Rate == rate);
        Assert.True(actual.Source.Equals(currency1));
        Assert.True(actual.Destination.Equals(currency2));
    }
    
    [Fact]
    public void Create_Rate_For_Same_Currencies()
    {
        // Arrange
        var currency1 = new Currency("CR1");
        var currency2 = new Currency("CR1");
        const decimal rate = decimal.One;
        
        // Act | Assert
        Assert.Throws<DomainException>(() => new CurrencyRate(currency1, currency2, rate));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Invalid_CurrencyRate_Rate(decimal rate)
    {
        // Arrange
        var currency1 = new Currency("CR1");
        var currency2 = new Currency("CR2");
        
        // Act | Assert
        Assert.Throws<DomainException>(() => new CurrencyRate(currency1, currency2, rate));
    }

    [Fact]
    public void Equal_CurrencyRate_For_Same_Currency_Pair()
    {
        // Arrange
        var currency1 = new Currency("CR1");
        var currency2 = new Currency("CR2");
        const decimal rate = decimal.One;
        
        // Act
        var currencyRate1 = new CurrencyRate(currency1, currency2, rate);
        var currencyRate2 = new CurrencyRate(currency2, currency1, rate); 
        
        // Assert
        Assert.True(currencyRate1.Equals(currencyRate2));
    }
    
    [Fact]
    public void Not_Equal_CurrencyRate_For_Different_Currency_Pair()
    {
        // Arrange
        var currency1 = new Currency("CR1");
        var currency2 = new Currency("CR2");
        var currency3 = new Currency("CR3");
        const decimal rate = decimal.One;
        
        // Act
        var currencyRate1 = new CurrencyRate(currency1, currency2, rate);
        var currencyRate2 = new CurrencyRate(currency2, currency3, rate); 
        
        // Assert
        Assert.False(currencyRate1.Equals(currencyRate2));
    }
    
    [Fact]
    public void Equal_Currency_Rate_HashCode_For_Same_Currency_Pair()
    {
        // Arrange
        var currency1 = new Currency("CR1");
        var currency2 = new Currency("CR2");
        const decimal rate = decimal.One;
        
        // Act
        var currencyRate1 = new CurrencyRate(currency1, currency2, rate);
        var currencyRate2 = new CurrencyRate(currency2, currency1, rate); 
        
        // Assert
        Assert.True(currencyRate1.GetHashCode() == currencyRate2.GetHashCode());
    }
    
    [Fact]
    public void Not_Equal_Currency_Rate_HashCode_For_Different_Currency_Pair()
    {
        // Arrange
        var currency1 = new Currency("CR1");
        var currency2 = new Currency("CR2");
        var currency3 = new Currency("CR3");
        const decimal rate = decimal.One;
        
        // Act
        var currencyRate1 = new CurrencyRate(currency1, currency2, rate);
        var currencyRate2 = new CurrencyRate(currency2, currency3, rate); 
        
        // Assert
        Assert.False(currencyRate1.GetHashCode() == currencyRate2.GetHashCode());
    }
}
