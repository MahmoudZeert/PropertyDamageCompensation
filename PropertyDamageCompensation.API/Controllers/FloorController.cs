using Microsoft.AspNetCore.Mvc;
using PropertyDamageCompensation.API.Filters;
using PropertyDamageCompensation.Application.Persistence.FloorService;
using PropertyDamageCompensation.Contracts.Dtos;

namespace PropertyDamageCompensation.API.Controllers
{


    [ApiController]
    [Route("api/[Controller]")]

    public class FloorController : ControllerBase
    {
        private readonly IFloorService _floorService;

        public FloorController(IFloorService floorService)
        {
            _floorService = floorService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var floorDtos = await _floorService.GetAllAsync();
            return Ok(floorDtos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var floor = await _floorService.GetByIdAsync(id);
            if (floor == null) { return NotFound(); };
            return Ok(floor);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FloorDto floorDto)
        {
            var floor=await _floorService.AddAsync(floorDto);
      
            return CreatedAtAction(nameof(GetById), new { id = floor.Id },floor);

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id,[FromBody] FloorDto floorDto)
        {
            await _floorService.UpdateAsync(id,floorDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult>   Delete(int id)

        {
            await _floorService.DeleteAsync(id);
            return NoContent();
        }

    }
}
