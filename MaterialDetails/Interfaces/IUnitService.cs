using MaterialDetails.Models;

namespace MaterialDetails.Interfaces
{
    public interface IUnitService
    {
        Task<IList<Unit>> GetUnits();
        Task<Unit> GetUnitById(int id);
        Task DeleteUnit(int id);
        Task CreateUnit(Unit unit);
    }
}
