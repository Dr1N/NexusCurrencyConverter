using Converter.Domain.Currency;
using Converter.Domain.Exceptions;
using Converter.Domain.Localization;
using Xunit;

namespace Converter.Tests.Domain;

public class TemplateTests
{
    [Fact]
    public void Create_Template_For_Currency()
    {
        // Arrange
        var currency = new Currency("CR1");
        const string template = "For {0} USD you will get {1} CHF";

        // Act
        var actual = new ResultTemplate(currency, template);
        
        // Assert
        Assert.NotNull(actual);
        Assert.Equal(template, actual.MessageTemplate);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Empty_Currency_Message_Template(string template)
    {
        // Arrange
        var currency = new Currency("CR1");
        
        // Act | Assert
        Assert.Throws<DomainException>(() => new ResultTemplate(currency, template));
    }

    [Fact]
    public void Equal_Templates_Same_Currency_Different_Message()
    {
        // Arrange
        const string message1 = "For {0} USD you will get {1} CHF";
        const string message2 = "For {0} CHF you will get {1} USD";
        var currency = new Currency("CR1");
        
        // Act
        var template1 = new ResultTemplate(currency, message1);
        var template2 = new ResultTemplate(currency, message2);
        
        // Assert
        Assert.True(template1.Equals(template2));
    }
    
    [Fact]
    public void Not_Equal_Templates_Different_Currency()
    {
        // Arrange
        const string message1 = "For {0} USD you will get {1} CHF";
        var currency1 = new Currency("CR1");
        var currency2 = new Currency("CR2");
        
        // Act
        var template1 = new ResultTemplate(currency1, message1);
        var template2 = new ResultTemplate(currency2, message1);
        
        // Assert
        Assert.False(template1.Equals(template2));
    }
}
