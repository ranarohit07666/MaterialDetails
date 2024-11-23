using MaterialDetails.Models;
using MaterialDetails.ViewModels;

namespace MaterialDetails.Interfaces
{
    public interface IMaterialService
    {
        Task<IList<Material>> GetMaterials();
        Task<Material> GetMaterialById(int id);
        Task CreateMaterial(MaterialViewModel material);
        Task DeleteMaterial(int id);
    }
}
