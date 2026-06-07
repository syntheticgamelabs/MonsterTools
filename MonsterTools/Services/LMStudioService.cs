using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace MonsterTools.Services;

public class LMStudioService
{
    private readonly HttpClient _http;
    private readonly string _baseUrl;

    public LMStudioService()
    {
        _baseUrl = "http://127.0.0.1:1234";
        _http = new HttpClient();
    }

    public string Ask(string prompt)
    {
        var url = $"{_baseUrl}/v1/chat/completions";

        var payload = new
        {
            model = "local-model",
            messages = new[]
            {
                new
                {
                    role = "system",
                    content =
                        "You are a strict tool router. " +
                        "Return ONLY valid JSON in this format: " +
                        "{ \"tool\": \"SearchWorker\", \"args\": { \"pattern\": \"...\", \"workspaceRoot\": \".\" } }. " +
                        "If no tool is needed return { \"tool\": null, \"args\": {} }. " +
                        "NEVER output explanations or text."
                },
                new
                {
                    role = "user",
                    content = prompt
                }
            },
            temperature = 0.1
        };

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = _http.PostAsync(url, content).Result;
        var result = response.Content.ReadAsStringAsync().Result;

        using var doc = JsonDocument.Parse(result);

        var output = doc.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();

        return output ?? "";
    }
}