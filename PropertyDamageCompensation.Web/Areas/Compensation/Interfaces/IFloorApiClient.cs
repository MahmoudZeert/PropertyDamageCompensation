using Microsoft.AspNetCore.Mvc;
using PropertyDamageCompensation.Contracts.Dtos;
using PropertyDamageCompensation.Domain.Entities;

namespace PropertyDamageCompensation.Web.Areas.Compensation.Interfaces
{
    public interface IFloorApiClient
    {
        Task<FloorDto> GetById(int id);
        Task<IEnumerable<FloorDto>> GetAll();
        Task<FloorDto> Create(FloorDto floor);
        Task Update(int id, FloorDto floor);
        Task<bool> Delete(int id);
    }
}
