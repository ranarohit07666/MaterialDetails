using MaterialDetails.DataContext;
using MaterialDetails.Interfaces;
using MaterialDetails.Models;
using Microsoft.EntityFrameworkCore;

namespace MaterialDetails.Services
{
    public class TypeService:ITypeService
    {
        private readonly MaterialDataContext _materialDataContext;
        public TypeService(MaterialDataContext materialDataContext)
        {
            _materialDataContext = materialDataContext;
        }

        public async Task CreateType(Types types)
        {

            if (types == null) throw new Exception("Type not valid");
            if (types.Id > 0)
            {
                await _materialDataContext.tbl_type.AddAsync(types);
            }
            else
            {
                _materialDataContext.tbl_type.Update(types);
            }
            await _materialDataContext.SaveChangesAsync();
        }

        public async Task DeleteType(int id)
        {
            if (id > 0)
            {
                var typeById = await _materialDataContext.tbl_type.FirstOrDefaultAsync(x => x.Id == id);
                if (typeById != null)
                {
                    _materialDataContext.tbl_type.Remove(typeById);
                    await _materialDataContext.SaveChangesAsync();
                }
            }
        }

        public async Task<Types> GetTypeById(int id)
        {
            return await _materialDataContext.tbl_type.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<Types>> GetTypes()
        {
            return await _materialDataContext.tbl_type.ToListAsync();
        }
    }
}
