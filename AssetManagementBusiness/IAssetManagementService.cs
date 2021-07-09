using AssetManagementData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagementBusiness
{
    public interface IAssetManagementService
    {
        Task<bool> CreateAsync(AssetModel entity);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<AssetModel>> GetAllAsync();
        Task<AssetModel> GetByIdAsync(int id);
        Task<bool> Update(AssetModel entity);
    }
}