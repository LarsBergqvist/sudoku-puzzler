using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sudoku.Library;

namespace Sudoku.Web.Pages;

public class IndexModel : PageModel
{
    private readonly SudokuGenerator _generator;

    public byte[]? Grid { get; private set; }

    public IndexModel(SudokuGenerator generator)
    {
        _generator = generator;
    }

    public ActionResult OnGet()
    {
        return Page();
    }

    public IActionResult OnPost(string difficulty)
    {
        IPuzzlePolicy policy;
        switch (difficulty)
        {
            case "Basic":
                policy = new BasicPuzzlePolicy();
                break;
            case "Hard":
                policy = new HardPuzzlePolicy();
                break;
            case "VeryHard":
                policy = new VeryHardPuzzlePolicy();
                break;
            default:
                policy = new BasicPuzzlePolicy();
                break;
        }

        try
        {
            Grid = _generator.GeneratePuzzle(policy).PuzzleGrid;
            return Page();
        }
        catch (Exception)
        {
            return Page();
        }
    }
} 