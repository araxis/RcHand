# RcHand - AI-Driven Robotic Hand Controller

**RcHand** is a learning project that demonstrates how to control a robotic right hand using servo motors, ESP32, and AI.

 It integrates with [ModelContextProtocol (MCP)](https://www.anthropic.com/news/model-context-protocol) to allow AI-assisted command execution and gesture scripting.
| Column 1 | Column 2 |
|----------|----------|
|![sticker](https://github.com/user-attachments/assets/bd52ea4e-e909-44fa-bdb6-4eade8ce4578)  | ![image](https://github.com/user-attachments/assets/5d066625-2236-4097-a80f-08a6a75db5b9)|


 


### 🔧 What it does

- Controls 5 servos (thumb to little finger)
- Accepts simple `servoId:angle;` command format with optional delays
- Communicates via USB Serial
- Integrates with .NET using [MCP C# Sdk](https://github.com/modelcontextprotocol/csharp-sdk) for AI and scripting

### 🧠 What is MCP?
Model Context Protocol (MCP) is an open standard that defines how tools and services can communicate with AI models using structured inputs and outputs. 
It allows developers to integrate tools, memory, APIs, and agents in a way that feels seamless to the AI — enabling it to call tools, act on your behalf, or access external resources during conversations.

In this project, MCP is used to let an AI (like Claude or GPT) control a robotic hand by calling a tool (SetHandState) that sends servo commands over serial port.

Key Concepts:
- Stdio Tooling: Tools communicate via standard input/output using JSON messages.
- Server Declaration: Each tool is defined in mcp.json, including its type, command, and arguments.

### 🔠 Command Format

```text
0:180;9:100;1:90;2:60;9:100;3:120;4:90;9:50;
```
## Some useful resources to get started.
- [Model Context Protocol (MCP), clearly explained (why it matters)](https://www.youtube.com/watch?v=7j_NE6Pjv-E&t=25s)
- [Introduction to the C# SDK for Model Context Protocol (MCP)](https://www.youtube.com/watch?v=krB1aA9xpts)
- [Using Model Context Protocol in agents - Introduction](https://www.developerscantina.com/p/mcp-intro/)
- [Using Model Context Protocol in agents - Copilot Studio](https://www.developerscantina.com/p/mcp-copilot-studio/)
- ...

🚀 How to Use
- Publish the  RcHand.Mcp.Stdio.Server as an executable app
  ```bach
   dotnet publish -r win-x64 -p:PublishSingleFile=true --self-contained true
  ```
- Open Your Project Folder in VS Code
  - Create or open any folder (empty is fine)
 
- Create .vscode Folder
  - Inside your project root, create a .vscode folder.
 
- Add mcp.json File
  Inside .vscode/mcp.json, paste the following:
  ```json
    {
      "servers": {
        "RoboticHand": {
          "type": "stdio",
          "command": "path/to/RcHand.Mcp.Stdio.Server.exe",
          "args": [
            "--port",
            "COM8"
          ]
        }
      }
    }
  


📌 Adjust the command and COM port(esp32) to match your system.

- Set Opilot Mode to Agent
- Activate Tools
- Start MCP Server
- Enjoy
