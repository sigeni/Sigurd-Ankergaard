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
    [InlineData(1999)]
    [InlineData(2400)]
    [InlineData(90)]
    public void ConvertedIntegerDoesNotContainTwoSmallerRomanNumeralsBeforeAnyGreater(int integerValueToConvert)
    {
        // Given
        List<char> acceptedRomanNumerals = new() { 'I', 'V', 'X', 'L', 'C', 'D', 'M' };
        
        // When
        string romanNumeral = IntegerToRomanNumeralConverter.ConvertInteger(integerValueToConvert);
        
        // Then

        // number of lower with index less than greater is 1 or 0
        // greater should probably be checked by last index
    }
    [Theory]
    [InlineData(1999, new[]{'V','L','D','M'})]
    [InlineData(2400,new[]{'V','L','D','M'})]
    [InlineData(90,new[]{'V','L','D','M'})]
    public void ConvertedIntegerOnlyHasAllowedRomanNumeralsBeforeAnyGreaterRomanNumeral(int integerValueToConvert, char[] characters)
    {
        // Given
        List<char> acceptedRomanNumerals = new() { 'I', 'V', 'X', 'L', 'C', 'D', 'M' };
        
        // When
        string romanNumeral = IntegerToRomanNumeralConverter.ConvertInteger(integerValueToConvert);
        
        // Then
        Assert.False(characters.Any(character =>
        {
            int firstIndexOfLowerNumeral = romanNumeral.IndexOf(character);
            int lastIndexOfGreaterNumeral = GetMaximumLastIndexOfLargerNumerals(character, romanNumeral, acceptedRomanNumerals);
            return firstIndexOfLowerNumeral < lastIndexOfGreaterNumeral;
        }));

        // I only in front of  V X
        // X -> L C
        // C -> D M
    }
    [Theory]
    [InlineData(1999, new[]{'I','X','C'})]
    [InlineData(2400,new[]{'I','X','C'})]
    [InlineData(90,new[]{'I','X','C'})]
    public void ConvertedIntegerOnlyHasRomanNumeralsBeforeAnyAllowedGreaterRomanNumeral(int integerValueToConvert, char[] characters)
    {
        // Given
        List<char> acceptedRomanNumerals = new() { 'I', 'V', 'X', 'L', 'C', 'D', 'M' };

        // When
        string romanNumeral = IntegerToRomanNumeralConverter.ConvertInteger(integerValueToConvert);
        
        // Then
        Assert.False(characters.Any(character =>
        {
            List<char> acceptedRomanNumeralsToCheck = acceptedRomanNumerals.Skip(acceptedRomanNumerals.IndexOf(character) + 2).ToList();
            int firstIndexOfLowerNumeral = romanNumeral.IndexOf(character);
            int lastIndexOfGreaterNumeral = GetMaximumLastIndexOfLargerNumerals(character, romanNumeral, acceptedRomanNumeralsToCheck);
            return firstIndexOfLowerNumeral < lastIndexOfGreaterNumeral;
        }));
    }
    [Theory]
    [InlineData(1999)]
    [InlineData(2400)]
    [InlineData(90)]
    public void ConvertedIntegerDoesNotRepeatNonRepeatableRomanNumerals(int integerValueToConvert)
    {
        
        // Given
        char[] nonRepeatableCharacters = new []{'V','L','D'};
        
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
    [InlineData(1999,"IMM")]
    [InlineData(2400,"CMMD")]
    [InlineData(90,"XC")]
    public void ConvertsIntegerCorrectly(int integerValueToConvert, string romanNumeralToConvertTo)
    {
        // Given

        // When
        string romanNumeral = IntegerToRomanNumeralConverter.ConvertInteger(integerValueToConvert);
        
        // Then
        Assert.Equal(romanNumeralToConvertTo, romanNumeral);
    }
    private static int GetMaximumLastIndexOfLargerNumerals(char lowerCharacter, string romanNumeral, IList<char> acceptedRomanNumerals)
    {
        char[] greaterRomanNumerals = acceptedRomanNumerals.Where(character =>
            acceptedRomanNumerals.IndexOf(character) > acceptedRomanNumerals.IndexOf(lowerCharacter)).ToArray();
        return romanNumeral.LastIndexOfAny(greaterRomanNumerals);
    }
}
