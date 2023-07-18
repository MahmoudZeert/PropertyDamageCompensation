using PropertyDamageCompensation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyDamageCompensation.Domain.Interfaces.Persistence
{
    public interface IFloorRepository
    {
        Task<Floor> GetByFloorIdAsync(int floorId);
        Task<Floor> getByNameAsync(string name);
        Task<IEnumerable<Floor>> GetAllAsync();
        Task AddAsync(Floor floor);
        Task UpdateAsync(Floor floor);
        Task DeleteAsync(Floor floor);
    }
}
