using ModelContextProtocol.Server;
using RcHand.Core;
using System.ComponentModel;

namespace RcHand.Mcp.Http.Server;
[McpServerToolType]
internal class RcHandTool
{
    private readonly ISerialHandService _bluetoothService;

    public RcHandTool(ISerialHandService bluetoothService)
    {
        _bluetoothService = bluetoothService;
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
    public async Task<HandState> SetHandState(
    [Description("Command string for setting servo angles and optional delays. Format: 'id:angle;...;9:delay;'")]
    string command)
    {
        return await _bluetoothService.SendCommandAsync(command);

    }

}


