namespace JyskeLogic;

public static class IntegerToRomanNumeralConverter
{
    private static readonly Dictionary<int, char>
        s_romanNumeralsIntegerValueDictionary = new()
        {
            {1,'I'},
            {5,'V'},
            {10,'X'},
            {50,'L'},
            {100,'C'},
            {500,'D'},
            {1000,'M'}
        };
    public static string ConvertInteger(int decimalValueToConvert)
    {
        string romanNumeral = string.Empty;
        foreach (var currentRomanNumeralToCheck in s_romanNumeralsIntegerValueDictionary.Keys.OrderDescending())
        {
            romanNumeral = CheckAndAddRomanNumeral(ref decimalValueToConvert, currentRomanNumeralToCheck, romanNumeral);
            if (decimalValueToConvert == 0)
            {
                break;
            }
        }

        return romanNumeral;
    }

    private static string CheckAndAddRomanNumeral(ref int decimalValue, int romanValue, string result)
    {
        int minusValue = 0;
        string minusChar = string.Empty;
        switch (romanValue)
        {
            case 1000 or 500:
                minusValue = 111;
                minusChar = "C";
                break;
            case 50 or 100:
                minusValue = 11;
                minusChar = "X";
                break;
            case 5 or 10:
                minusValue = 1;
                minusChar = "I";
                break;
        }

        int numberOfRomanLetters = GetNumberOfRomanLettersToAdd(decimalValue, minusValue, romanValue);
        bool subtract = decimalValue / romanValue != numberOfRomanLetters;
        for (int i = 0; i < int.Abs(numberOfRomanLetters); i++)
        {
            if (!subtract || i + 1 < int.Abs(numberOfRomanLetters))
            {
                result += s_romanNumeralsIntegerValueDictionary[romanValue];
                decimalValue -= romanValue;
            }
            else
            {
                result += $"{minusChar}{s_romanNumeralsIntegerValueDictionary[romanValue]}";
                decimalValue -= romanValue - s_romanNumeralsIntegerValueDictionary.FirstOrDefault(x => x.Value.ToString() == minusChar).Key;
                break;
            }
        }
        return result;
    }

    private static int GetNumberOfRomanLettersToAdd(int decimalValue, int minusValue, int romanValue)
    {
        int numberOfRomanLetters = (Math.Abs(decimalValue) + minusValue) / romanValue;
        if (numberOfRomanLetters > 3)
        {
            return 0;
        }
        return romanValue is 5 or 50 or 500 ? Math.Min(1, numberOfRomanLetters) : numberOfRomanLetters;
    }
}