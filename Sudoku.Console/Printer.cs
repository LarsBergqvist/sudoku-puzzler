using System;
using Sudoku.Library;

namespace Sudoku;

public class ConsoleCustomLogger : ICustomLogger
{
    public void Write(string text) => Console.Write(text);
    public void WriteLine(string text) => Console.WriteLine(text);
    public void WriteLine() => Console.WriteLine();
}
