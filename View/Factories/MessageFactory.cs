using Interfaces.Models.Messages;
using Interfaces.Services.Factories;
using Shared.Enums.Views.Messages;
using View.Messages;

namespace View.Factories;

public class MessageFactory : IMessageFactory
{
    private readonly Dictionary<MessageType, Func<IMessage>> _messageFactories;

    public MessageFactory()
    {
        _messageFactories = InitializeMessageFactories();
    }
    
    public IMessage Create(MessageType messageType)
    {
        if (_messageFactories.TryGetValue(messageType, out var factory))
        {
            return factory();
        }
        
        throw new ArgumentException($"No such message type { messageType.ToString() }.", nameof(messageType));
    }

    private static Dictionary<MessageType, Func<IMessage>> InitializeMessageFactories() =>
        new()
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
            { MessageType.CantMoveThere, () => new CantMoveThere() },
            { MessageType.PlayerAlreadyExistsMessage, () => new AlreadyExistsMessage() },
            { MessageType.PlayerCreatedMessage, () => new PlayerCreated() },
            { MessageType.EnterNameMessage, () => new EnterName() },
            { MessageType.PressAnyKeyToContinue, () => new PressAnyKeyToContinue() },
            { MessageType.InvalidAttackKey, () => new InvalidAttackKey() },
        };
}