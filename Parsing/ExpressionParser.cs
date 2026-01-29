using System.Diagnostics;
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

        if (!TryParseLeftOperand(input, operatorIndex, out int left))
            return false;

        if (!TryParseRightOperand(input, operatorIndex, out int right))
            return false;

        expression = new Expression(left, op, right);
        return true;
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

    private static bool TryParseLeftOperand(string input, int operatorIndex, out int left)
    {
        left = 0;
        bool foundDigit = false;

        for (int i = 0; i < operatorIndex; i++)
        {
            char c = input[i];

            if (c == ' ')
                continue;

            if (c < '0' || c > '9')
                return false;

            int digit = c - '0';
            left = (left * 10) + digit;
            foundDigit = true;
        }

        return foundDigit;
    }

    private static bool TryParseRightOperand(string input, int operatorIndex, out int right)
    {
        right = 0;
        bool foundDigit = false;

        for (int i = operatorIndex + 1; i < input.Length; i++)
        {
            char c = input[i];

            if (c == ' ')
                continue;

            if (c < '0' || c > '9')
                return false;

            int digit = c - '0';
            right = (right * 10) + digit;
            foundDigit = true;
        }

        return foundDigit;
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