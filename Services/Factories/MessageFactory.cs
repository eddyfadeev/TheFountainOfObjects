using Interfaces.Models.Messages;
using Interfaces.Services.Factories;
using Model.Messages;
using Shared.Enums.Models.Messages;
using View.Messages;

namespace Services.Factories;

public class MessageFactory : IMessageFactory
{
    private readonly Dictionary<MessageType, Func<IMessage>> _messageCreators;

    public MessageFactory()
    {
        _messageCreators = new Dictionary<MessageType, Func<IMessage>>
        {
            { MessageType.FeelNothing, () => new FeelNothing() },
            { MessageType.AmarokNearby, () => new AmarokNearby() },
            { MessageType.MaelstromNearby, () => new MaelstromNearby() },
            { MessageType.PitNearby, () => new PitNearby() },
            { MessageType.MissedShot, () => new MissedShot() },
            { MessageType.KilledAmarok, () => new KilledAmarok() },
            { MessageType.NoArrowsLeft, () => new NoArrowsLeft() },
            { MessageType.SteppedIntoMaelstromRoom, () => new SteppedIntoMaelstrom() },
            { MessageType.SteppedIntoPitRoom, () => new SteppedIntoPit() },
            { MessageType.SteppedIntoAmarokRoom, () => new SteppedIntoAmarok() },
            { MessageType.Victory, () => new Victory() },
            { MessageType.AtEntranceRoom, () => new AtEntrance() },
            { MessageType.AtNotActiveFountain, () => new AtNotActiveFountain() },
            { MessageType.AtActiveFountain, () => new AtActiveFountain() },
            { MessageType.NothingHappened, () => new NothingHappened() },
            { MessageType.FountainActivated, () => new FountainActivated() },
            { MessageType.FountainIsAlreadyActivated, () => new FountainIsAlreadyActivated() },
            { MessageType.Attack, () => new AttackDirection() },
            { MessageType.CantShootThere, () => new CantShootThere() },
            { MessageType.CantMoveThere, () => new CantMoveThere() }
        };
    }
    public IMessage Create(MessageType messageType)
    {
        if (_messageCreators.TryGetValue(messageType, out var creator))
        {
            return creator();
        }
        
        throw new ArgumentException($"No such message type { nameof(messageType) }. Message factory.");
    }
}