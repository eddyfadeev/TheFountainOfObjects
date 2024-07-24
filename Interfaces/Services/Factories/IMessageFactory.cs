using Interfaces.Models.Messages;
using Shared.Enums.Models.Messages;

namespace Interfaces.Services.Factories;

public interface IMessageFactory
{
    IMessage Create(MessageType messageType);
}