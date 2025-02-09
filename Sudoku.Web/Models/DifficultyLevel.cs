using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Sudoku.Web.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DifficultyLevel
{
    [EnumMember(Value = "Basic")]
    Basic,
    
    [EnumMember(Value = "Hard")]
    Hard,
    
    [EnumMember(Value = "VeryHard")]
    VeryHard
} 