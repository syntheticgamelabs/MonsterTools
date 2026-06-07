using System.Text.Json;

namespace MonsterTools.Core;

public class ToolCall
{
    public string? tool { get; set; }
    public Dictionary<string, object?> args { get; set; } = new();

    public static ToolCall Parse(string json)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(json))
                return new ToolCall();

            var clean = json.Trim();

            var result = JsonSerializer.Deserialize<ToolCall>(
                clean,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return result ?? new ToolCall();
        }
        catch
        {
            return new ToolCall();
        }
    }
}