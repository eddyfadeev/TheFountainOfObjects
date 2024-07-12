using Interfaces.Models.Maze;
using Interfaces.View.Maze;
using Model.Extensions;
using Shared.Enums.Models.Objects.Maze;

namespace View.Views.Room;

public class RoomView : IRoomView
{
    public Canvas RoomCanvas { get; }
    
    public RoomView(IRoom room, MazeSize mazeSize)
    {
        RoomCanvas = room.PaintRoom(mazeSize);
    }
}