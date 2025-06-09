using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RcHand.Core;
using RcHand.Mcp.Stdio.Server;
using Serilog;

var port = "COM8";
var baudRate = 115200;
Parser.Default.ParseArguments<CommandLineArgs>(args)
                   .WithParsed(o =>
                   {
                       port = o.Port;
                       baudRate = o.BaudRate;
                   });
var builder = Host.CreateEmptyApplicationBuilder(settings: null);
builder.Services.AddHandSerialController(port, baudRate);
builder.Services.AddSerilog((services, loggerConfiguration) => loggerConfiguration
    .WriteTo.File("logs.txt"));
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<RcHandTool>();

var host = builder.Build();
await host.RunAsync();

