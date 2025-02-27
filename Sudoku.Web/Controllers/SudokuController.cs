using Microsoft.AspNetCore.Mvc;
using Sudoku.Library;
using Sudoku.Web.Models;

namespace Sudoku.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SudokuController : ControllerBase
{
    private readonly SudokuGenerator _generator;

    public SudokuController(SudokuGenerator generator)
    {
        _generator = generator;
    }

    [HttpGet]
    public ActionResult<SudokuResponse> GeneratePuzzle([FromQuery] DifficultyLevel difficulty = DifficultyLevel.Basic)
    {
        var policy = GetPolicyForDifficulty(difficulty);
        var puzzle = _generator.GeneratePuzzle(policy);

        return new SudokuResponse
        {
            Puzzle = ConvertToString(puzzle.PuzzleGrid),
            Solution = ConvertToString(puzzle.FullGrid),
            Difficulty = difficulty
        };
    }

    private static string ConvertToString(byte[] grid)
    {
        return string.Concat(grid.Select(b => b == 0 ? ' ' : (char)('0' + b)));
    }

    private static IPuzzlePolicy GetPolicyForDifficulty(DifficultyLevel difficulty)
    {
        return difficulty switch
        {
            DifficultyLevel.Basic => new BasicPuzzlePolicy(),
            DifficultyLevel.Hard => new HardPuzzlePolicy(),
            DifficultyLevel.VeryHard => new VeryHardPuzzlePolicy(),
            _ => throw new ArgumentOutOfRangeException(nameof(difficulty))
        };
    }
} 