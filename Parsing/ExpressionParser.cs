using ConsoleCalculator.Models;

namespace ConsoleCalculator.Parsing;

public static class ExpressionParser
{
    public static bool TryParseExpression(string input, out Expression expression)
    {
        expression = default;

        if (string.IsNullOrWhiteSpace(input))
            return false;

        if (!TryFindSingleOperator(input, out char op, out int operatorIndex))
            return false;

        // We have a valid operator in a valid position.
        // Next step: parse left and right operands around operatorIndex.
        return false;
    }

    private static bool TryFindSingleOperator(string input, out char op, out int operatorIndex)
    {
        op = default;
        operatorIndex = -1;

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];

            if (c == ' ')
                continue;

            if (!IsOperator(c))
                continue;

            // Found an operator; ensure it's the only one.
            if (operatorIndex != -1)
                return false;

            operatorIndex = i;
            op = c;
        }

        // Must contain exactly one operator.
        if (operatorIndex == -1)
            return false;

        // Operator cannot be at the beginning or end (ignoring spaces).
        if (IsOnlySpacesOnLeft(input, operatorIndex) || IsOnlySpacesOnRight(input, operatorIndex))
            return false;

        return true;
    }

    private static bool IsOperator(char c) => c is '+' or '-' or '*' or '/';

    private static bool IsOnlySpacesOnLeft(string input, int operatorIndex)
    {
        for (int i = 0; i < operatorIndex; i++)
        {
            if (input[i] != ' ')
                return false;
        }
        return true;
    }

    private static bool IsOnlySpacesOnRight(string input, int operatorIndex)
    {
        for (int i = operatorIndex + 1; i < input.Length; i++)
        {
            if (input[i] != ' ')
                return false;
        }
        return true;
    }
}