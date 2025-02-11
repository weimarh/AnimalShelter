using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public partial record PhoneNumber
{
    private const int DefaultLenght = 8;
    private const string Pattern = @"^\d+$";

    private PhoneNumber(string value) => Value = value;

    public static PhoneNumber? Create(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);

        if (value.Length != DefaultLenght)
            return null;

        if (!PhoneNumberRegex().IsMatch(value))
            return null;

        return new PhoneNumber(value);
    }
    public string Value { get; init; }

    [GeneratedRegex(Pattern)]
    private static partial Regex PhoneNumberRegex();
}
