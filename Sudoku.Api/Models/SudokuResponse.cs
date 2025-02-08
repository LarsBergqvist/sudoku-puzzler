namespace Sudoku.Api.Models;

public class SudokuResponse
{
    public string Grid { get; set; } = null!;
    public string Solution { get; set; } = null!;
    public DifficultyLevel Difficulty { get; set; }
} 