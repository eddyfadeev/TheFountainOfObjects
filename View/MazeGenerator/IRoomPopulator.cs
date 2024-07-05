using Model.GameSettings;
using Model.Interfaces;
using Model.Maze;

namespace View.MazeGenerator;

public interface IRoomPopulator
{
    void GenerateRooms(IMaze<IRoom> maze, IMazeService<IRoom> mazeService);
    void SetRoomOccupants(
        IMaze<IRoom> maze, 
        IPlayerRepository playerRepository, 
        IMazeObjectFactory mazeObjectFactory, 
        IGameSettingsRepository gameSettingsRepository);
}