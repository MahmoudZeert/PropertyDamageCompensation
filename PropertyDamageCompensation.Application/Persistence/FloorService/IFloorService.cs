using PropertyDamageCompensation.Application.Services;
using PropertyDamageCompensation.Contracts.Dtos;
using PropertyDamageCompensation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyDamageCompensation.Application.Persistence.FloorService
{
    public interface IFloorService
    {
        Task<IEnumerable<FloorDto>> GetAllAsync();
        Task<FloorDto> GetByIdAsync(int id);
        Task<FloorDto> GetBynameAsync(string name);
        Task<FloorDto> AddAsync(FloorDto entity);
        Task UpdateAsync(int id,FloorDto entity);
        Task DeleteAsync(int id);
    }
}
