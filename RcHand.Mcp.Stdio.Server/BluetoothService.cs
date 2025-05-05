using InTheHand.Net.Sockets;
using Microsoft.Extensions.Logging;

namespace RcHand.Mcp.Stdio.Server;
public class BluetoothService : IBluetoothService, IDisposable
{
    private BluetoothClient? _client;
    private StreamWriter? _writer;
    private readonly ILogger<BluetoothService> _logger;

    public BluetoothService(ILogger<BluetoothService> logger)
    {
        _logger = logger;
    }

    public bool IsConnected => _client?.Connected ?? false;
    public async Task<bool> StartAsync()
    {
        try
        {

            _logger.LogInformation("Searching for 'Rc_Hand'...");

            _client = new BluetoothClient();
            var devices = _client.DiscoverDevices();

            var device = devices.FirstOrDefault(d => d.DeviceName == "RcHand");
            if (device == null)
            {
                Console.WriteLine("Device not found.");
                return false;
            }

            await _client.ConnectAsync(device.DeviceAddress, InTheHand.Net.Bluetooth.BluetoothService.SerialPort);
            _writer = new StreamWriter(_client.GetStream()) { AutoFlush = true };

            Console.WriteLine("Connected to RcHand.");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection failed: {ex.Message}");
            return false;
        }
    }

    public async Task SendCommandAsync(string command)
    {
        if (_writer == null)
        {
            Console.WriteLine("Not connected. Call StartAsync first.");
            return;
        }

        try
        {
            await _writer.WriteLineAsync(command);
            Console.WriteLine($"Sent: {command}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send command: {ex.Message}");
        }
    }

    public async Task StopAsync()
    {
        _writer?.Dispose();
        _client?.Close();
        _writer = null;
        _client = null;
        await Task.CompletedTask;

        Console.WriteLine("Disconnected.");
    }

    public void Dispose()
    {
        _writer?.Dispose();
        _client?.Close();
    }
}
