using System.Net.Http.Json;

namespace MonsterTools.Runner.Services;

public class LlmClient
{
    private readonly HttpClient _http = new();

    public async Task<string> AskAsync(string prompt)
    {
        var request = new
        {
            model = "granite",
            messages = new[]
            {
                new { role = "user", content = prompt }
            }
        };

        var response = await _http.PostAsJsonAsync(
            "http://localhost:1234/v1/chat/completions",
            request
        );

        var json = await response.Content.ReadAsStringAsync();
        return json;
    }
}