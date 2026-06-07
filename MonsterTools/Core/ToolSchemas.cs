using System.Collections.Generic;
using System.IO;

namespace MonsterTools.Core;

public static class ToolSchemas
{
    public static readonly Dictionary<string, ToolDefinition> Tools = new()
    {
        ["SearchWorker"] = new ToolDefinition
        {
            Required = new[] { "pattern" },
            Optional = new[] { "workspaceRoot" }
        }
    };
}

public class ToolDefinition
{
    public string[] Required { get; set; } = [];
    public string[] Optional { get; set; } = [];
}
