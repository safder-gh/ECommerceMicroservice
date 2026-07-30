using IMS.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.UseCase.PluginInterfaces
    {
    public interface IInventoryRepository
        {
        Task<IEnumerable<Inventory>> GetByNameAsync(string name);
        }
    }
