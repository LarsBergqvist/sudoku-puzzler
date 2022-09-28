using System;

namespace Sudoku;

public interface IPrinter
{
    void Write(string text);
    void WriteLine(string text);
    void WriteLine();
}

public class ConsolePrinter : IPrinter
{
    public void Write(string text) => Console.Write(text);

    public void WriteLine(string text) => Console.WriteLine(text);
    public void WriteLine() => Console.WriteLine();
}

public class NullPrinter : IPrinter
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