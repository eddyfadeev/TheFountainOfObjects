using Model.Player;

namespace Controller.Interfaces;

public interface IGeneratesEnum
{
    protected List<PlayerDTO> GetDataForEnum();
}