namespace Sudoku.Library;

public interface ICustomLogger
{
    void Write(string text);
    void WriteLine(string text);
    void WriteLine();
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