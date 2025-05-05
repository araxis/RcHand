using System.IO.Ports;

namespace RcHand.Mcp.Stdio.Server
{
    public class SerialHandService : ISerialHandService
    {
        private readonly string _portName;
        private readonly int _baudRate;
        private SerialPort? _serialPort;

        public SerialHandService(string portName = "COM5", int baudRate = 115200)
        {
            _portName = portName;
            _baudRate = baudRate;
        }

        public Task StartAsync()
        {
            _serialPort = new SerialPort(_portName, _baudRate)
            {
                NewLine = "\n",
                DtrEnable = true
            };

            _serialPort.Open();
            Console.WriteLine($"[SerialHandService] Connected to {_portName}");

            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            if (_serialPort?.IsOpen == true)
            {
                _serialPort.Close();
                Console.WriteLine("[SerialHandService] Disconnected");
            }

            return Task.CompletedTask;
        }

        public Task SendCommandAsync(string command)
        {
            if (_serialPort?.IsOpen != true)
                throw new InvalidOperationException("Serial port is not open.");

            _serialPort.WriteLine(command);
            Console.WriteLine($"[SerialHandService] Sent: {command}");

            return Task.CompletedTask;
        }
    }
}