using MonsterTools.Core;
using MonsterTools.Services;
using MonsterTools.Runner.Workers;

Console.WriteLine("MonsterTools MCP Server Starting...");

// 1. Register workers (UNCHANGED)
var workers = new IToolWorker[]
{
    new SearchWorker(),
    new FileWorker(),
    new ValidationWorker(),
    new BuildWorker()
};

// 2. Core tool execution stack
var executor = new ToolExecutor(workers);

// 3. LM service still exists (for future hybrid mode, NOT used in MCP loop)
var llm = new LMStudioService();

// 4. Agent loop (ONLY used internally if you later extend LLM tool routing)
var agent = new AgentLoop(llm, executor);

// 5. MCP server wrapper (THIS is now the REAL entry point)
var server = new McpServer(agent);

Console.WriteLine("MCP Ready (STDIO mode)");

// 6. HARD BLOCKING LOOP (MCP PIPE)
server.Run();