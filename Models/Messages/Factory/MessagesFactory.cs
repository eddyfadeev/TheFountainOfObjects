using Model.Messages.Enum;
using Model.Messages.Interfaces;
using View.Messages;

namespace Model.Messages.Factory;

public class MessagesFactory : IMessagesFactory
{
    public IMessage CreateMessage(MessageType messageType) =>
        messageType switch
        {
            MessageType.Nothing => new Nothing(),
            MessageType.AmarokNearby => new AmarokNearby(),
            MessageType.MaelstromNearby => new MaelstromNearby(),
            MessageType.PitNearby => new PitNearby(),
            MessageType.MissedShot => new MissedShot(),
            MessageType.KilledAmarok => new KilledAmarok(),
            MessageType.NoArrowsLeft => new NoArrowsLeft(),
            MessageType.SteppedIntoMaelstromRoom => new SteppedIntoMaelstrom(),
            MessageType.SteppedIntoPitRoom => new SteppedIntoPit(),
            MessageType.SteppedIntoAmarokRoom => new SteppedIntoAmarok(),
            MessageType.Victory => new Victory(),
            MessageType.AtEntranceRoom => new AtEntrance(),
            MessageType.AtNotActiveFountain => new AtNotActiveFountain(),
            MessageType.AtActiveFountain => new AtActiveFountain(),
            _ => throw new ArgumentException("No such message type")
        };
}