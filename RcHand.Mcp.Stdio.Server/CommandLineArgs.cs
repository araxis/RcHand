using CommandLine;

namespace RcHand.Mcp.Stdio.Server;
internal class CommandLineArgs
{
    [Option('p', "port", Required = true)]
    public string Port { get; set; } = string.Empty;

    [Option('b', "baudRate", Required = false)]
    public int BaudRate { get; set; } = 115200;
}