using Spectre.Console;

namespace Model.RoomService;

public interface IRoomService
{
    IRoom[,] MazeRooms { get; set; }
    //static Canvas SetRoomColor(IRoom room);
}