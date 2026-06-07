using MonsterTools.Core;
using System.IO;

namespace MonsterTools.Runner.Workers;

public class FileWorker : IToolWorker
{
    public string Name => "file";

    public ToolResult Run(ToolRequest request)
    {
        try
        {
            var path = request.Get<string>("path");

            if (string.IsNullOrWhiteSpace(path))
                return ToolResult.Fail("Invalid path");

            if (!File.Exists(path))
                return ToolResult.Fail("File does not exist");

            var content = File.ReadAllText(path);

            return ToolResult.Ok(content);
        }
        catch (Exception ex)
        {
            return ToolResult.Fail($"FileWorker error: {ex.Message}");
        }
    }
}