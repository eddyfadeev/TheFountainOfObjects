using Shared.Enums.Views.Messages;

namespace Interfaces.Controller;

public interface IMessagesHandler
{
    void ShowMessage(MessageType message);
}