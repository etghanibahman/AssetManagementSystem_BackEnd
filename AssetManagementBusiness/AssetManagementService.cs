using AssetManagementData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagementBusiness
{
    public class AssetManagementService : IAssetManagementService
    {
        private readonly AssetDbContext _context;

        public AssetManagementService(AssetDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(AssetModel entity)
        {
            _context.Assets.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(AssetModel entity)
        {

            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employeeModel = await _context.Assets.FindAsync(id);
            if (employeeModel == null)
            {
                return false;
            }
            _context.Assets.Remove(employeeModel);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<AssetModel>> GetAllAsync()
        {
            return await _context.Assets.ToListAsync();
        }

        public async Task<AssetModel> GetByIdAsync(int id)
        {
            return await _context.Assets.Where(a => a.AssetID == id).FirstOrDefaultAsync();
        }
    }
}
