using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RcHand.Mcp.Stdio.Server;
using Serilog;

var port = "COM5";
var baudRate = 115200;
Parser.Default.ParseArguments<CommandLineArgs>(args)
                   .WithParsed<CommandLineArgs>(o =>
                   {
                       port = o.Port;
                       baudRate = o.BaudRate;
                   });
var builder = Host.CreateEmptyApplicationBuilder(settings: null);
builder.Services.AddSingleton<ISerialHandService>(_ => new SerialHandService(port, baudRate));
builder.Services.AddSerilog((services, loggerConfiguration) => loggerConfiguration
    .WriteTo.File("logs.txt"));
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<RcHandTool>();

var host = builder.Build();
var bluetoothService = host.Services.GetRequiredService<ISerialHandService>();
await bluetoothService.StartAsync();
await host.RunAsync();

