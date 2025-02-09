using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sudoku.Library;

namespace Sudoku.Web.Pages;

public class IndexModel : PageModel
{
    private readonly SudokuGenerator _generator;

    public byte[] Grid { get; private set; }

    public IndexModel(SudokuGenerator generator)
    {
        _generator = generator;
    }

    public ActionResult OnGet()
    {
        try
        {
            Grid = _generator.GeneratePuzzle(new BasicPuzzlePolicy()).PuzzleGrid;
            return Page();
        }
        catch (Exception ex)
        {
            // Handle error - you might want to show an error message to the user
            return Page();
        }
    }

    public IActionResult OnPost()
    {
        try
        {
            Grid = _generator.GeneratePuzzle(new BasicPuzzlePolicy()).PuzzleGrid;
            return Page();
        }
        catch (Exception ex)
        {
            // Handle error - you might want to show an error message to the user
            return Page();
        }
    }
} 