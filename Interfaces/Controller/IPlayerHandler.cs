namespace Interfaces.Controller;

public interface IPlayerHandler
{
    bool TryLoadPlayer();
    bool CreatePlayer();
    bool CheckIfPlayerExists(string playerName);
    string ProcessUserNameInput();
}