using Model.Messages.Enum;

namespace Model.Messages.Interfaces;

public interface IMessagesFactory
{
    IMessage CreateMessage(MessageType messageType);
}