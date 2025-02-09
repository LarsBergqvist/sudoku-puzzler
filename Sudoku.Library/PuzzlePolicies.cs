namespace Sudoku.Library;

public interface IPuzzlePolicy
{
    public int MaxBlanks { get; }
    public int MaxNumRetries { get; }
}

public class BasicPuzzlePolicy : IPuzzlePolicy
{
    public int MaxBlanks => 45;
    public int MaxNumRetries => 10;
}

public class HardPuzzlePolicy : IPuzzlePolicy
{
    public int MaxBlanks => 50;
    public int MaxNumRetries => 50;
}

public class VeryHardPuzzlePolicy : IPuzzlePolicy
{
    public int MaxBlanks => 53;
    public int MaxNumRetries => 100;
}
