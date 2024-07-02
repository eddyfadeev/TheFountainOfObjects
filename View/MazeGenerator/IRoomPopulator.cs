using Model.GameSettings;
using Model.Interfaces;

namespace View.MazeGenerator;

public interface IRoomPopulator
{
    void GenerateRooms(IMazeService<IRoom> mazeService);
    void SetRoomOccupants(
        IMazeService<IRoom> mazeService, 
        IPlayerRepository playerRepository, 
        IMazeObjectFactory mazeObjectFactory, 
        IGameSettingsRepository gameSettingsRepository);
}