namespace RcHand.Mcp.Stdio.Server;
public interface ISerialHandService
{
    Task StartAsync();
    Task StopAsync();
    Task SendCommandAsync(string command);
}
