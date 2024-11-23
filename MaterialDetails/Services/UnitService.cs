using MaterialDetails.DataContext;
using MaterialDetails.Interfaces;
using MaterialDetails.Models;
using Microsoft.EntityFrameworkCore;

namespace MaterialDetails.Services
{
    public class UnitService:IUnitService
    {
        private readonly MaterialDataContext _materialDataContext;
        public UnitService(MaterialDataContext materialDataContext)
        {
            _materialDataContext = materialDataContext;
        }

        public async Task CreateUnit(Unit unit)
        {
            if (unit == null) throw new Exception("Unit not valid");
            if (unit.Id > 0) { 
                await _materialDataContext.tbl_unit.AddAsync(unit);
            }
            else
            {
                _materialDataContext.tbl_unit.Update(unit);
            }
            await _materialDataContext.SaveChangesAsync();
            
        }

        public async Task DeleteUnit(int id)
        {
            if (id > 0)
            {
               var unitById= await _materialDataContext.tbl_unit.FirstOrDefaultAsync(x => x.Id == id);
                if (unitById != null)
                {
                    _materialDataContext.tbl_unit.Remove(unitById);
                    await _materialDataContext.SaveChangesAsync();
                }
            }
        }

        public async Task<Unit> GetUnitById(int id)
        {
            return await _materialDataContext.tbl_unit.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<Unit>> GetUnits()
        {
            return await _materialDataContext.tbl_unit.ToListAsync();
        }
    }
}
