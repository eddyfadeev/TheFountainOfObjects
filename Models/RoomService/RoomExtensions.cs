using Model.Enums;
using Spectre.Console;

namespace Model.RoomService;

public static class RoomExtensions
{
    public static Canvas SetRoomColor(this IRoom room, MazeSize mazeSize)
    {
        var cellSize = CalculateCellSize(mazeSize);
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
    
    private static int CalculateCellSize(MazeSize mazeSize) => 
        mazeSize switch
        {
            MazeSize.Small => 6,
            MazeSize.Medium => 4,
            MazeSize.Large => 3,
            _ => 3
        };
}