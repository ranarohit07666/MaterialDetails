using MaterialDetails.Models;
using MaterialDetails.ViewModels;

namespace MaterialDetails.Interfaces
{
    public interface ITypeService
    {
        Task<IList<Types>> GetTypes();
        Task<Types> GetTypeById(int id);
        Task DeleteType(int id);
        Task CreateType(Types types);
    }
}
