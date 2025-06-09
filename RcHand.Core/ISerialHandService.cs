namespace RcHand.Core;
public interface ISerialHandService
{
    Task StartAsync();
    Task StopAsync();
    Task<HandState> SendCommandAsync(string command);
}
