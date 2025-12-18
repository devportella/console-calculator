namespace ConsoleCalculator.Models;

public readonly record struct Expression(int Left, char Operator, int Right);
