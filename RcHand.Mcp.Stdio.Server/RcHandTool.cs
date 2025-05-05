using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace RcHand.Mcp.Stdio.Server;
[McpServerToolType]
internal class RcHandTool
{
    private readonly ISerialHandService _bluetoothService;
    private readonly ILogger<RcHandTool> _logger;

    public RcHandTool(ISerialHandService bluetoothService, ILogger<RcHandTool> logger)
    {
        _bluetoothService = bluetoothService;
        _logger = logger;
    }

    [McpServerTool, Description(
    "Set the servo angles for each finger on the robotic hand.\n\n" +
    "Provide a semicolon-separated command string in the format:\n" +
    "  \"0:angle;1:angle;2:angle;3:angle;4:angle;9:delay;\"\n\n" +
    "Finger IDs:\n" +
    "  0 = Thumb (180 = fully closed, 0 = fully open)\n" +
    "  1 = Index\n" +
    "  2 = Middle\n" +
    "  3 = Ring\n" +
    "  4 = Little\n\n" +
    "For fingers 1–4: 0 = fully close, 180 = fully opend\n" +
    "For thumb (0): 0 = fully open, 180 = fully closed\n\n" +
    "Optional delay:\n" +
    "  9:delay — adds a pause in milliseconds between commands\n\n" +
    "Example:\n" +
    "  \"0:0;9:30;1:180;9:30;2:180;9:30;3:1809:30;;4:180;\" → Fully open all fingers, 30ms delay between commands")]
    public async Task SetHandState(
    [Description("Command string for setting servo angles and optional delays. Format: 'id:angle;...;9:delay;'")]
    string command)
    {
        _logger.LogInformation("Setting hand state with command:{command}", command);
        await _bluetoothService.SendCommandAsync(command);
        // return new HandState();
    }
}

public struct HandState
{
    public int ThumbAngle { get; }
    public int IndexAngle { get; }
    public int MiddleAngle { get; }
    public int RingAngle { get; }
    public int LittleAngle { get; }
    public HandState(int thumbAngle, int indexAngle, int middleAngle, int ringAngle, int littleAngle)
    {
        ThumbAngle = thumbAngle;
        IndexAngle = indexAngle;
        MiddleAngle = middleAngle;
        RingAngle = ringAngle;
        LittleAngle = littleAngle;
    }

}
