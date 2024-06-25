using Model.Interfaces;
using Services.GameSettingsRepository;
using Spectre.Console;

namespace Services.RoomService;

public class RoomService : IRoomService
{
    private readonly IGameSettingsRepository _gameSettingsRepository;
    public IRoom[,] MazeRooms { get; set; }

    public RoomService(IGameSettingsRepository gameSettingsRepository)
    {
        _gameSettingsRepository = gameSettingsRepository;
        var mazeSize = (int)_gameSettingsRepository.MazeSize;

        MazeRooms = new IRoom[mazeSize, mazeSize];
    }

    public Canvas SetRoomColor(IRoom room)
    {
        var cellSize = _gameSettingsRepository.CellSize;
        var roomColor = room.RoomColor;

        var roomCanvas = new Canvas(cellSize, cellSize);

        for (int i = 0; i < cellSize; i++)
        {
            for (int j = 0; j < cellSize; j++)
            {
                roomCanvas.SetPixel(j, i, roomColor);
            }
        }

        return roomCanvas;
    }
}