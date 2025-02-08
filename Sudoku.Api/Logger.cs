using Sudoku.Library;

namespace Sudoku.Api;

public class Logger : ICustomLogger
{
    private readonly ILogger _logger;

    public Logger(ILogger logger)
    {
        _logger = logger;
    }
    public void Write(string text)
    {
        _logger.LogInformation(text);
    }

    public void WriteLine(string text)
    {
        _logger.LogInformation(text);
    }

    public void WriteLine()
    {
    }
}