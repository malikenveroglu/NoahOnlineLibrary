using NoahOnlineLibrary.Application.Interfaces.IRepository;
using NoahOnlineLibrary.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoahOnlineLibrary.Application.Services
{
    public class ReservedItemService: IReservedItemService
    {
        private readonly IReservedItemRepository _reservedItemRepository;
            
        public ReservedItemService(IReservedItemRepository reservedItemRepository)
        {
            _reservedItemRepository = reservedItemRepository;
        }
    }
}
