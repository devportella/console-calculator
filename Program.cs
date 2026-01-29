using ConsoleCalculator.Models;
using ConsoleCalculator.Parsing;

while (true)
{
    Console.Write("Enter an expression (e.g. 12 + 34) or 'q' to quit: ");
    string? input = Console.ReadLine();

    if (input is null)
        continue;

    input = input.Trim();

    if (input.Equals("q", StringComparison.OrdinalIgnoreCase))
        break;

    if (!ExpressionParser.TryParseExpression(input, out Expression expression))
    {
        Console.WriteLine("Invalid expression. Example: 12 + 34");
        continue;
    }

    if (!TryEvaluate(expression, out int result))
    {
        Console.WriteLine("Error: division by zero.");
        continue;
    }

    Console.WriteLine($"Result: {result}");
}

static bool TryEvaluate(Expression expression, out int result)
{
    result = 0;

    switch (expression.Operator)
    {
        case '+':
            result = expression.Left + expression.Right;
            return true;

        case '-':
            result = expression.Left - expression.Right;
            return true;

        case '*':
            result = expression.Left * expression.Right;
            return true;

        case '/':
            if (expression.Right == 0)
                return false;

            result = expression.Left / expression.Right;
            return true;

        default:
            return false;
    }
}