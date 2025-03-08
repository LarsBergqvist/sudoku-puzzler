using System;
namespace Sudoku;

public class CustomConsole : IConsole
{
    public void Write(string text) => Console.Write(text);
    public void WriteLine(string text) => Console.WriteLine(text);
    public void WriteLine() => Console.WriteLine();
    public string ReadLine() => Console.ReadLine();
}
