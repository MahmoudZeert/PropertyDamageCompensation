using PropertyDamageCompensation.Contracts.Dtos;
using PropertyDamageCompensation.Domain.Entities;
using PropertyDamageCompensation.Domain.Exceptions;
using PropertyDamageCompensation.Domain.Interfaces.Persistence;

namespace PropertyDamageCompensation.Application.Persistence.FloorService
{
    public class FloorService : IFloorService
    {
        private readonly IFloorRepository _floorRepository;

        public FloorService(IFloorRepository floorRepository)
        {
            _floorRepository = floorRepository;
        }



        async Task<IEnumerable<FloorDto>> IFloorService.GetAllAsync()
        {
            var floors= await  _floorRepository.GetAllAsync();
            if (floors != null) {
                return floors.Select(floor => new FloorDto
                {
                    Id = floor.Id,
                    Name = floor.Name
                }); }
            return null;
           
        }

        async Task<FloorDto> IFloorService.GetByIdAsync(int id)
        {
            var floor= await _floorRepository.GetByFloorIdAsync(id);
            if (floor==null)
            {
                return null;

            }
            return MapFloorToDto(floor);
        }

        async Task<FloorDto> IFloorService.GetBynameAsync(string name)
        {
            var floor=await _floorRepository.getByNameAsync(name);
            if (floor==null)
            {
                return null;
            }
            return MapFloorToDto(floor);
        }

        async Task<FloorDto> IFloorService.AddAsync(FloorDto entity)
        {
            var floor = new Floor
            {
                Name = entity.Name
            };
            await _floorRepository.AddAsync(floor);
            return MapFloorToDto(floor);
        }

        async Task IFloorService.DeleteAsync(int id)
        {
            var floor = await _floorRepository.GetByFloorIdAsync(id);
            if (floor == null)
            {
                throw new NotFoundException("floor not found !");
               
               
            }
             await _floorRepository.DeleteAsync(floor);

        }
        async Task IFloorService.UpdateAsync(int id, FloorDto entity)
        {
            var floor = await _floorRepository.GetByFloorIdAsync(id);
            if (entity == null)
            {
                throw new NotFoundException("floor not found !");
            }
            floor.Name = entity.Name;

            await _floorRepository.UpdateAsync(floor);
        }

        private FloorDto MapFloorToDto(Floor floor)
        {
            return new FloorDto
            {
                Id = floor.Id,
                Name = floor.Name
            };
        }
        private Floor MapDtoToFloor(FloorDto floor)
        {
            return new Floor
            {
                Id = floor.Id,
                Name = floor.Name
            };
        }
    }
}
