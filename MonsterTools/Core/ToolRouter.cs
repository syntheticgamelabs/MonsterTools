namespace MonsterTools.Core;

public class ToolRouter
{
    private readonly ToolExecutor _executor;

    public ToolRouter(ToolExecutor executor)
    {
        _executor = executor;
    }

    public ToolResult Run(
        string toolName,
        ToolRequest request)
    {
        return _executor.Execute(
            toolName,
            request
        );
    }
}