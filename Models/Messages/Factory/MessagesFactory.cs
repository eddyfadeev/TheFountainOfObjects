using Model.Messages.Enum;
using Model.Messages.Interfaces;
using View.Messages;

namespace Model.Messages.Factory;

public class MessagesFactory : IMessagesFactory
{
    public IMessage CreateMessage(MessageType messageType) =>
        messageType switch
        {
            MessageType.FeelNothing => new FeelNothing(),
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
            MessageType.NothingHappened => new NothingHappened(),
            MessageType.FountainActivated => new FountainActivated(),
            MessageType.FountainIsAlreadyActivated => new FountainIsAlreadyActivated(),
            MessageType.Attack => new AttackDirection(),
            _ => throw new ArgumentException("No such message type. Message factory.")
        };
}