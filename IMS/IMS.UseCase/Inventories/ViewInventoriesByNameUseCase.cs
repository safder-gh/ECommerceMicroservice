using IMS.CoreBusiness;
using IMS.UseCase.Inventories.Interfaces;
using IMS.UseCase.PluginInterfaces;

namespace IMS.UseCase.Inventories
    {
    public class ViewInventoriesByNameUseCase(IInventoryRepository inventoryRepository) : IViewInventoriesByNameUseCase
        {
        public async Task<IEnumerable<Inventory>> ExecuteAsync(string name = "")
            {
            return await inventoryRepository.GetByNameAsync(name);
            }
        }
    }
