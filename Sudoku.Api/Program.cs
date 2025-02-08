using Microsoft.OpenApi.Models;
using Sudoku.Api;
using Sudoku.Api.Swagger;
using Sudoku.Library;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Sudoku.Console API", 
        Version = "v1",
        Description = "API for generating Sudoku.Console puzzles"
    });
    
    // Configure enum naming
    c.SchemaFilter<EnumSchemaFilter>();
});

// Register Sudoku.Console services
builder.Services.AddSingleton<GridValidator>();
builder.Services.AddSingleton<SudokuSolver>();
builder.Services.AddSingleton<ICustomLogger, Logger>();
builder.Services.AddSingleton<SudokuGenerator>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sudoku.Console API V1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run(); 