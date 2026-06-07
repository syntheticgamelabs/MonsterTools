using MonsterTools.Core;
using System.IO;

namespace MonsterTools.Workers;

public class FileSystemWorker : IToolWorker
{
    public string Name => "filesystem";

    public ToolResult Run(ToolRequest request)
    {
        var action = request.Get<string>("action");
        var path = request.Get<string>("path");

        if (string.IsNullOrWhiteSpace(action))
            return ToolResult.Fail("Missing action");

        if (string.IsNullOrWhiteSpace(path))
            return ToolResult.Fail("Missing path");

        if (action == "read")
        {
            if (!File.Exists(path))
                return ToolResult.Fail("File not found");

            var content = File.ReadAllText(path);
            return ToolResult.Ok(content);
        }

        if (action == "write")
        {
            var content = request.Get<string>("content") ?? "";

            File.WriteAllText(path, content);
            return ToolResult.Ok("written");
        }

        return ToolResult.Fail("Unknown action");
    }
}