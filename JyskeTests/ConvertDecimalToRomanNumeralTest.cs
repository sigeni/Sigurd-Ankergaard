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
        ISet<char> acceptedRomanNumerals = new SortedSet<char> { 'I', 'V', 'X', 'L', 'C', 'D', 'M' };

        // When
        string romanNumeral = IntegerToRomanNumeralConverter.ConvertInteger(integerValueToConvert);

        // Then
        ISet<char> charactersInResult = new SortedSet<char>(romanNumeral.ToArray());
        Assert.ProperSubset(acceptedRomanNumerals, charactersInResult);
    }
    [Theory]
    [InlineData(1999, 'I', new[]{'C','L','D','M'})]
    [InlineData(2400, 'I', new[]{'C','L','D','M'})]
    [InlineData(90, 'I', new[]{'C','L','D','M'})]
    [InlineData(1999, 'X', new[] {'I', 'V', 'D', 'M' })]
    [InlineData(2400, 'X', new[] {'I', 'V', 'D', 'M' })]
    [InlineData(90, 'X', new[]{ 'I', 'V', 'D', 'M' })]
    [InlineData(1999, 'C', new[]{ 'I', 'V', 'X', 'L' })]
    [InlineData(2400, 'C', new[]{ 'I', 'V', 'X', 'L' })]
    [InlineData(90, 'C', new[]{ 'I', 'V', 'X', 'L' })]
    public void ConvertedIntegerOnlyHasAllowedRomanNumeralsBeforeAnyGreaterRomanNumeral(int integerValueToConvert, char characterToCheck, char[] charactersNotAllowedAfter)
    {
        // Given

        // When
        string romanNumeral = IntegerToRomanNumeralConverter.ConvertInteger(integerValueToConvert);
        
        // Then
        foreach (var character in charactersNotAllowedAfter)
        {
            Assert.DoesNotContain(romanNumeral, characterToCheck + character.ToString());
        }
    }
    [Theory]
    [InlineData(1999)]
    [InlineData(2400)]
    [InlineData(90)]
    public void ConvertedIntegerDoesNotRepeatNonRepeatableRomanNumerals(int integerValueToConvert)
    {
        
        // Given
        char[] nonRepeatableCharacters = {'V','L','D'};
        
        // When
        string romanNumeral = IntegerToRomanNumeralConverter.ConvertInteger(integerValueToConvert);
        
        // Then
        Assert.Distinct(romanNumeral.Where(character => nonRepeatableCharacters.Contains(character)));
    }
    [Theory]
    [InlineData(1999)]
    [InlineData(2400)]
    [InlineData(90)]
    public void ConvertedIntegerRepeatsNumeralsMaximumThreeTimes(int integerValueToConvert)
    {
        // Given
        const int maximumNumberOfRepeats = 3;
        const int minimumNumberOfRepeats = 0;
        
        // When
        string romanNumeral = IntegerToRomanNumeralConverter.ConvertInteger(integerValueToConvert);
        
        // Then
        int mostRepeatedCharacter = romanNumeral.GroupBy(x => x).Select(y => y.Count()).Max();
        Assert.InRange(mostRepeatedCharacter,minimumNumberOfRepeats,maximumNumberOfRepeats);
    }

    [Theory]
    [InlineData(1999,"MCMXCIX")]
    [InlineData(2400,"MMCD")]
    [InlineData(90,"XC")]
    [InlineData(3549, "MMMDXLIX")]
    public void ConvertsIntegerCorrectly(int integerValueToConvert, string romanNumeralToConvertTo)
    {
        // Given

        // When
        string romanNumeral = IntegerToRomanNumeralConverter.ConvertInteger(integerValueToConvert);
        
        // Then
        Assert.Equal(romanNumeralToConvertTo, romanNumeral);
    }
}
