using System;

namespace Sudoku;

public interface ICustomLogger
{
    void Write(string text);
    void WriteLine(string text);
    void WriteLine();
}

public class ConsoleCustomLogger : ICustomLogger
{
    public void Write(string text) => Console.Write(text);
    public void WriteLine(string text) => Console.WriteLine(text);
    public void WriteLine() => Console.WriteLine();
}

public class NullCustomLogger : ICustomLogger
{
    public void Write(string text)
    {
    }

    public void WriteLine(string text)
    {
    }

    public void WriteLine()
    {
    }
}