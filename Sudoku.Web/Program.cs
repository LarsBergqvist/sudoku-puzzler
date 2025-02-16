using Microsoft.OpenApi.Models;
using Sudoku.Web;
using Sudoku.Web.Swagger;
using Sudoku.Library;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

var builder = WebApplication.CreateBuilder(args);

// Add logging configuration
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();
});

// Add Razor Pages support
builder.Services.AddRazorPages();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Sudoku.Web API", 
        Version = "v1",
        Description = "API for generating Sudoku puzzles"
    });
    
    // Configure enum naming
    c.SchemaFilter<EnumSchemaFilter>();
});

// Register Sudoku services
builder.Services.AddSingleton<GridValidator>();
builder.Services.AddSingleton<SudokuSolver>();
builder.Services.AddSingleton<SudokuGenerator>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    });
}

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sudoku.Web API V1");
    c.RoutePrefix = "swagger";
});

// Add UseCors before routing and authorization
app.UseCors();
app.UseHttpsRedirection();
app.UseStaticFiles(); // Add static files middleware
app.UseRouting();    // Add routing middleware
app.UseAuthorization();

// Configure the request pipeline for both API and Razor Pages
app.MapControllers();
app.MapRazorPages();

app.Run(); 