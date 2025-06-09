
using Microsoft.Extensions.DependencyInjection;

namespace RcHand.Core;
public static class Module
{
    public static IServiceCollection AddHandSerialController(this IServiceCollection services, string portName, int baudRate = 115200)
    {
        services.AddSingleton<ISerialHandService>(_ => new SerialHandService(portName, baudRate));
        return services;
    }
}
