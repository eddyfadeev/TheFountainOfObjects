using Model.Interfaces;
using Spectre.Console;

namespace Services.RoomService;

public interface IRoomService
{
    IRoom[,] MazeRooms { get; set; }
    Canvas SetRoomColor(IRoom room);
}