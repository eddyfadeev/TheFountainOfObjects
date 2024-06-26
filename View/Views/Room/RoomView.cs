using Model.Interfaces;
using Services.RoomService;

namespace View.Views.Room;

public class RoomView : IRoomView
{
    private readonly IRoomService _roomService;
    private readonly IRoom _room;

    public Canvas RoomCanvas => _roomService.SetRoomColor(_room);
    
    public RoomView(IRoomService roomService, IRoom room)
    {
        _roomService = roomService;
        _room = room;
    }
}