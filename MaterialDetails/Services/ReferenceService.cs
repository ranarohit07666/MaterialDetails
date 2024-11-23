using MaterialDetails.DataContext;
using MaterialDetails.Interfaces;
using MaterialDetails.Models;
using Microsoft.EntityFrameworkCore;

namespace MaterialDetails.Services
{
    public class ReferenceService:IReferenceService
    {
        private readonly MaterialDataContext _materialDataContext;
        public ReferenceService(MaterialDataContext materialDataContext)
        {
            _materialDataContext = materialDataContext;
        }

        public async Task DeleteReferenceDetail(int id)
        {
            if (id > 0)
            {
                var referenceDetailById = await _materialDataContext.tbl_referencedetail.FirstOrDefaultAsync(x => x.Id == id);
                if (referenceDetailById != null)
                {
                    _materialDataContext.tbl_referencedetail.Remove(referenceDetailById);
                    await _materialDataContext.SaveChangesAsync();
                }
            }
        }

        public async Task<ReferenceDetail> GetReferenceDetailById(int id)
        {
            return await _materialDataContext.tbl_referencedetail.Include(x=>x.Materials).ThenInclude(x=>x.Unit).Include(x => x.Materials).ThenInclude(x => x.Types).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<ReferenceDetail>> GetReferenceDetails()
        {
            return await _materialDataContext.tbl_referencedetail.Include(x => x.Materials).ThenInclude(x => x.Unit).Include(x => x.Materials).ThenInclude(x => x.Types).ToListAsync();
        }
    }
}
