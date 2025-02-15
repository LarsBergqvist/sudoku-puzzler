using Sudoku.Library;
using Scalar.AspNetCore;

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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger(options =>
{
    options.RouteTemplate = "/openapi/{documentName}.json";
});
app.MapScalarApiReference();

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