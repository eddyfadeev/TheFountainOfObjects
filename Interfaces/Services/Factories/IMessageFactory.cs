using Interfaces.Models.Messages;
using Shared.Enums.Views.Messages;

namespace Interfaces.Services.Factories;

public interface IMessageFactory
{
    IMessage Create(MessageType messageType);
}