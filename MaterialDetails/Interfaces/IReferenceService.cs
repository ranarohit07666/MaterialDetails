using MaterialDetails.Models;
using MaterialDetails.ViewModels;

namespace MaterialDetails.Interfaces
{
    public interface IReferenceService
    {
        Task<IList<ReferenceDetail>> GetReferenceDetails();
        Task<ReferenceDetail> GetReferenceDetailById(int id);
        Task DeleteReferenceDetail(int id);
    }
}
