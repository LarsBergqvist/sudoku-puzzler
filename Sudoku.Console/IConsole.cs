namespace Sudoku;

public interface IConsole
{
    void Write(string text);
    void WriteLine(string text);
    void WriteLine();
    string ReadLine();
}
