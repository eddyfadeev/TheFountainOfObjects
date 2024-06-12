using Model.DataObjects;

namespace Controller.Interfaces;

public interface IGeneratesEnum
{
    protected IEnumerable<PlayerDTO> GetDataForEnum();
}