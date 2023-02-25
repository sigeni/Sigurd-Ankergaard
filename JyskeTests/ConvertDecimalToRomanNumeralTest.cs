global using Xunit;
using JyskeLogic;

namespace JyskeTests;

public class ConvertIntegerToRomanNumeralTest
{
    [Theory]
    [InlineData(1999)]
    [InlineData(2400)]
    [InlineData(90)]
    public void ConvertedIntegerOnlyContainsAcceptedRomanNumerals(int integerValueToConvert)
    {
        // Given
        
        // When
        string romanNumeral = IntegerToRomanNumeralConverter.ConvertInteger(integerValueToConvert);
        
        // Then
    }
    [Theory]
    [InlineData(1999)]
    [InlineData(2400)]
    [InlineData(90)]
    public void ConvertedIntegerDoesNotContainTwoSmallerRomanNumeralsBeforeAnyGreater(int integerValueToConvert)
    {
        // Given
        
        // When
        string romanNumeral = IntegerToRomanNumeralConverter.ConvertInteger(integerValueToConvert);
        
        // Then
    }
    [Theory]
    [InlineData(1999)]
    [InlineData(2400)]
    [InlineData(90)]
    public void ConvertedIntegerOnlyHasRomanNumeralsIXCBeforeAnyGreaterRomanNumeral(int integerValueToConvert)
    {
        // Given
        
        // When
        string romanNumeral = IntegerToRomanNumeralConverter.ConvertInteger(integerValueToConvert);
        
        // Then
    }
    [Theory]
    [InlineData(1999)]
    [InlineData(2400)]
    [InlineData(90)]
    public void ConvertedIntegerDoesNotRepeatRomanNumeralsVLD(int integerValueToConvert)
    {
        
        // Given
        
        // When
        string romanNumeral = IntegerToRomanNumeralConverter.ConvertInteger(integerValueToConvert);
        
        // Then
    }
    [Theory]
    [InlineData(1999)]
    [InlineData(2400)]
    [InlineData(90)]
    public void ConvertedIntegerRepeatsNumeralsMaximumThreeTimes(int integerValueToConvert)
    {
        // Given
        
        // When
        string romanNumeral = IntegerToRomanNumeralConverter.ConvertInteger(integerValueToConvert);
        
        // Then
    }
}
