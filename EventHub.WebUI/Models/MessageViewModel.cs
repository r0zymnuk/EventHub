namespace EventHub.WebUI.Models;

public record MessageViewModel
(
    string Message,
    MessageType Type = MessageType.Info
);
