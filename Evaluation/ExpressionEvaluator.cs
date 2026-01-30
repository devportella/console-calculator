using ConsoleCalculator.Models;

namespace ConsoleCalculator.Evaluation;

public static class ExpressionEvaluator
{
    public static bool TryEvaluate(Expression expression, out int result)
    {
        result = default;

        return expression.Operator switch
        {
            '+' => TryAdd(expression.Left, expression.Right, out result),
            '-' => TrySubtract(expression.Left, expression.Right, out result),
            '*' => TryMultiply(expression.Left, expression.Right, out result),
            '/' => TryDivide(expression.Left, expression.Right, out result),
            _ => false
        };
    }

    private static bool TryAdd(int left, int right, out int result)
    {
        result = left + right;
        return true;
    }

    private static bool TrySubtract(int left, int right, out int result)
    {
        result = left - right;
        return true;
    }

    private static bool TryMultiply(int left, int right, out int result)
    {
        result = left * right;
        return true;
    }

    private static bool TryDivide(int left, int right, out int result)
    {
        result = default;

        if (right == 0)
            return false;

        result = left / right;
        return true;
    }
}