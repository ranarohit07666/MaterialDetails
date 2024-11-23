using MaterialDetails.DataContext;
using MaterialDetails.Interfaces;
using MaterialDetails.Models;
using MaterialDetails.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MaterialDetails.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly MaterialDataContext _materialDataContext;
        public MaterialService(MaterialDataContext materialDataContext)
        {
            _materialDataContext = materialDataContext;
        }


        public async Task DeleteMaterial(int id)
        {
            var entity = await _materialDataContext.tbl_material.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                _materialDataContext.tbl_material.Remove(entity);
                await _materialDataContext.SaveChangesAsync();
            }
        }

        public async Task<IList<Material>> GetMaterials()
        {
            return await _materialDataContext.tbl_material.Include(x => x.Unit).Include(x => x.Types).Include(x => x.ReferenceDetail).ToListAsync();
        }

        public async Task CreateMaterial(MaterialViewModel material)
        {
            var existingMaterial = await _materialDataContext.tbl_material
                .FirstOrDefaultAsync(x => x.Id == material.Id);

            ReferenceDetail referenceDetail;
            if (material.ReferenceId > 0)
            {
                referenceDetail = await _materialDataContext.tbl_referencedetail
                    .FirstOrDefaultAsync(x => x.Id == material.ReferenceId);
            }
            else
            {
                referenceDetail = new ReferenceDetail { ReferenceDate = DateTime.Now };
                await _materialDataContext.tbl_referencedetail.AddAsync(referenceDetail);
                await _materialDataContext.SaveChangesAsync();
            }

            var unitById = material.UnitId > 0
                ? await _materialDataContext.tbl_unit.FirstOrDefaultAsync(x => x.Id == material.UnitId)
                : null;

            var typeById = material.TypeId > 0
                ? await _materialDataContext.tbl_type.FirstOrDefaultAsync(x => x.Id == material.TypeId)
                : null;

            if (unitById != null && typeById != null)
            {
                if (existingMaterial == null)
                {
                    existingMaterial = new Material
                    {
                        MaterialName = material.MaterialName,
                        Rate = material.Rate,
                        Consumption = material.Consumption,
                        Unit = unitById,
                        Types = typeById,
                        ReferenceDetail = referenceDetail
                    };
                    await _materialDataContext.tbl_material.AddAsync(existingMaterial);
                }
                else
                {
                    existingMaterial.MaterialName = material.MaterialName;
                    existingMaterial.Rate = material.Rate;
                    existingMaterial.Consumption = material.Consumption;
                    existingMaterial.Unit = unitById;
                    existingMaterial.Types = typeById;
                    existingMaterial.ReferenceDetail = referenceDetail;
                    _materialDataContext.tbl_material.Update(existingMaterial);
                }
                await _materialDataContext.SaveChangesAsync();
            }
        }

        public async Task<Material> GetMaterialById(int id)
        {
            return await _materialDataContext.tbl_material.Include(x => x.Unit).Include(x => x.Types).Include(x => x.ReferenceDetail).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
