namespace Sudoku.Web.Models;

public class SudokuResponse
{
    public string Puzzle { get; set; } = null!;
    public string Solution { get; set; } = null!;
    public DifficultyLevel Difficulty { get; set; }
} 