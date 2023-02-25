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
        string minusChar = String.Empty;
        if (romanValue == 1000 || romanValue == 500)
        {
            minusValue = 111;
            minusChar = "C";
        }
        else if (romanValue == 50 || romanValue == 100)
        {
            minusValue = 11;
            minusChar = "X";
        }
        else if (romanValue == 5 || romanValue == 10)
        {
            minusValue = 1;
            minusChar = "I";
        }
        int numberOfRomanLetters = (Math.Abs(decimalValue) + minusValue) / romanValue;
        if (numberOfRomanLetters > 3)
        {
            numberOfRomanLetters = 0;
        }
        else if (romanValue == 5 || romanValue == 50 || romanValue == 500)
        {
            numberOfRomanLetters = Math.Min(1, numberOfRomanLetters);
        }
        bool subtract = decimalValue / romanValue != numberOfRomanLetters;
        for (int i = 0; i < Int32.Abs(numberOfRomanLetters); i++)
        {
            if (!subtract || i + 1 < Int32.Abs(numberOfRomanLetters))
            {
                result += s_romanNumeralsIntegerValueDictionary[romanValue];
                decimalValue -= romanValue;
            }
            else
            {
                result += $"{minusChar}{s_romanNumeralsIntegerValueDictionary[romanValue]}";
                decimalValue -= (romanValue - s_romanNumeralsIntegerValueDictionary.FirstOrDefault(x => x.Value.ToString() == minusChar).Key);
                break;
            }

        }
        return result;
    }
}