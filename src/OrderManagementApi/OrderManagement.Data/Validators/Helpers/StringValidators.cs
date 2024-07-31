using System.Text.RegularExpressions;

namespace OrderManagement.Data.Validators.Helpers;

public static class StringValidators
{
    public static bool BeValidInput(string input)
    {
        if (string.IsNullOrEmpty(input))
            return true;

        if (Regex.IsMatch(input, @"<script\b[^<]*(?:(?!<\/script>)<[^<]*)*<\/script>"))
            return false;

        return true;
    }
}