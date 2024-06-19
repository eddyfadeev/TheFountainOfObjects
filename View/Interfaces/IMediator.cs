namespace View.Interfaces;

public interface IMediator
{
    void Notify(object sender, string ev);
    void Register(string eventName, Action action);
}