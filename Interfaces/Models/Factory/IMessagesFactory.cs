using Interfaces.Models.Messages;
using Shared.Enums.Models.Messages;

namespace Interfaces.Models.Factory;

public interface IMessagesFactory
{
    IMessage CreateMessage(MessageType messageType);
}