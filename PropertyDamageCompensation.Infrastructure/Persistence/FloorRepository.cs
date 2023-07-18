using Microsoft.EntityFrameworkCore;
using PropertyDamageCompensation.Domain.Entities;
using PropertyDamageCompensation.Domain.Exceptions;
using PropertyDamageCompensation.Domain.Interfaces.Persistence;
using PropertyDamageCompensation.Infrastructure.Data;

namespace PropertyDamageCompensation.Infrastructure.Persistence
{
    public class FloorRepository : IFloorRepository
    {

        private readonly ApplicationDbContext _context;

        public FloorRepository(ApplicationDbContext context) {
            _context = context;
        
        }
        async Task IFloorRepository.AddAsync(Floor floor)
        {
            await _context.Floor.AddAsync(floor);
            await _context.SaveChangesAsync();
        }
        async Task<Floor> IFloorRepository.GetByFloorIdAsync(int floorId)
        {
            return await _context.Floor.AsNoTracking().FirstOrDefaultAsync(fl=>fl.Id==floorId);
        }

        async Task<Floor> IFloorRepository.getByNameAsync(string name)
        {
            return await _context.Floor.AsNoTracking().FirstOrDefaultAsync(fl => fl.Name == name);
        }

        async Task<IEnumerable<Floor>> IFloorRepository.GetAllAsync()
        {
           return await _context.Floor.AsNoTracking().ToListAsync();
        }

        async Task IFloorRepository.UpdateAsync(Floor floor)
        {
            _context.Floor.Update(floor);
            await _context.SaveChangesAsync();
        }

        async Task IFloorRepository.DeleteAsync(Floor floor)
        {

                _context.Floor.Remove(floor);
                await _context.SaveChangesAsync();

        }

    }
}
