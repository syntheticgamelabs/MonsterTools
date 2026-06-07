using MonsterTools.Core;
using System.IO;
using System.Linq;

namespace MonsterTools.Workers;

public class WorkspaceWorker : IToolWorker
{
    public string Name => "workspace";

    public ToolResult Run(ToolRequest request)
    {
        var path = request.Get<string>("path")
                   ?? Directory.GetCurrentDirectory();

        if (!Directory.Exists(path))
            return ToolResult.Fail("Path not found");

        var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                             .Take(200)
                             .ToArray();

        return ToolResult.Ok(string.Join("\n", files));
    }
}