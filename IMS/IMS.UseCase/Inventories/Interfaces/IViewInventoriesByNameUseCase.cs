using IMS.CoreBusiness;

namespace IMS.UseCase.Inventories.Interfaces
    {
    public interface IViewInventoriesByNameUseCase
        {
        Task<IEnumerable<Inventory>> ExecuteAsync(string name = "");
        }
    }