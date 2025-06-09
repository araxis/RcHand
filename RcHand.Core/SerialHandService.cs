using System.IO.Ports;

namespace RcHand.Core;

internal class SerialHandService : ISerialHandService
{
    private readonly string _portName;
    private readonly int _baudRate;
    private readonly SerialPort _serialPort;

    public SerialHandService(string portName, int baudRate = 115200)
    {
        _portName = portName;
        _baudRate = baudRate;
        _serialPort = new SerialPort(_portName, _baudRate)
        {
            NewLine = "\n",
            DtrEnable = true
        };
    }

    public Task StartAsync()
    {
        _serialPort.Open();
        return Task.CompletedTask;
    }

    public Task StopAsync()
    {
        if (_serialPort.IsOpen == true)
        {
            _serialPort.Close();
        }

        return Task.CompletedTask;
    }

    public async Task<HandState> SendCommandAsync(string command)
    {
        if (_serialPort.IsOpen != true) await StartAsync();

        _serialPort.WriteLine(command);
        return ParseHandCommand(command);


    }
    public static HandState ParseHandCommand(string message)
    {
        // Initialize with defaults (or use -1 if you want "unset" status)
        int thumb = 0, index = 0, middle = 0, ring = 0, little = 0;

        var segments = message.Split(';', StringSplitOptions.RemoveEmptyEntries);

        foreach (var part in segments)
        {
            var pair = part.Split(':');
            if (pair.Length != 2) continue;

            if (!int.TryParse(pair[0], out int id)) continue;
            if (!int.TryParse(pair[1], out int val)) continue;

            switch (id)
            {
                case 0: thumb = val; break;
                case 1: index = val; break;
                case 2: middle = val; break;
                case 3: ring = val; break;
                case 4: little = val; break;
                case 9: Thread.Sleep(val); break; // Simulates Arduino delay
            }
        }

        return new HandState(thumb, index, middle, ring, little);
    }
}
