using View.Interfaces;

namespace View.Mediator;

public class ConcreteMediator : IMediator
{
    private readonly Dictionary<string, List<Action>> _actions = new();

    public void Register(string eventName, Action action)
    {
        if (!_actions.ContainsKey(eventName))
        {
            _actions[eventName] = new List<Action>();
        }
        _actions[eventName].Add(action);
    }

    public void Notify(object sender, string ev)
    {
        if (_actions.ContainsKey(ev))
        {
            foreach (var action in _actions[ev])
            {
                action.Invoke();
            }
        }
    }

}