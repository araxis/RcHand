using RcHand.Core;
using RcHand.Mcp.Http.Server;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddHandSerialController("COM8", 115200)
    .AddMcpServer()
    .WithHttpTransport()
    .WithTools<RcHandTool>();

var host = builder.Build();
host.MapMcp();
await host.RunAsync();