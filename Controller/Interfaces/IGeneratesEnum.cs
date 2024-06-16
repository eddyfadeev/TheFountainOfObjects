using DataObjects.Player;

namespace Controller.Interfaces;

public interface IGeneratesEnum
{
    protected IEnumerable<PlayerDTO> GetDataForEnum();
}