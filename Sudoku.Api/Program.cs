using Microsoft.OpenApi.Models;
using Sudoku;
using Sudoku.Api.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Sudoku API", 
        Version = "v1",
        Description = "API for generating Sudoku puzzles"
    });
    
    // Configure enum naming
    c.SchemaFilter<EnumSchemaFilter>();
});

// Register Sudoku services
builder.Services.AddSingleton<GridValidator>();
builder.Services.AddSingleton<SudokuSolver>();
builder.Services.AddSingleton<ICustomLogger, ConsoleCustomLogger>();
builder.Services.AddSingleton<SudokuGenerator>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sudoku API V1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run(); 