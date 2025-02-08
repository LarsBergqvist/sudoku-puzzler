namespace Sudoku.Api;

public class ConsolePrinter : IPrinter
{
    public void Write(string message)
    {
        // No-op for API
    }

    public void WriteLine(string message)
    {
        // No-op for API
    }

    public void WriteLine()
    {
        throw new NotImplementedException();
    }
} 