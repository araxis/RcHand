namespace RcHand.Mcp.Stdio.Server;

public interface IBluetoothService
{
    Task<bool> StartAsync();
    Task StopAsync();
    Task SendCommandAsync(string command);
    bool IsConnected { get; }
}
