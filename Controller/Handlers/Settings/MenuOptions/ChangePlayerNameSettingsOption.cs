using Interfaces.Controller;
using Interfaces.Models.Repository;

namespace Controller.Handlers.Settings.MenuOptions;

public class ChangePlayerNameSettingsOption : ISettingsOptionHandler
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IPlayerHandler _playerHandler;

    public ChangePlayerNameSettingsOption(IPlayerRepository playerRepository, IPlayerHandler playerHandler)
    {
        _playerRepository = playerRepository;
        _playerHandler = playerHandler;
    }

    public void Handle()
    {
        _playerRepository.Player!.Name = _playerHandler.GetValidUserName();
    }
}