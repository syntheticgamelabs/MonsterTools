using MonsterTools.Services;
using System.Text.Json;

namespace MonsterTools.Core;

public class AgentLoop
{
    private readonly LMStudioService _llm;
    private readonly WorkerDispatcher _dispatcher;

    public AgentLoop(LMStudioService llm, ToolExecutor executor)
    {
        _llm = llm;
        _dispatcher = new WorkerDispatcher(executor);
    }

    public string Run(string prompt)
    {
        var response = _llm.Ask(prompt);

        var toolCall = ToolCall.Parse(response);

        if (string.IsNullOrWhiteSpace(toolCall.tool))
            return JsonSerializer.Serialize(new
            {
                success = false,
                error = "NO TOOL SELECTED",
                raw = response
            });

        var result = _dispatcher.Dispatch(toolCall.tool, toolCall.args ?? new());

        return JsonSerializer.Serialize(result);
    }

    public ToolResult RunToolDirect(string tool, Dictionary<string, object?> args)
{
    return _dispatcher.Dispatch(tool, args);
}
}