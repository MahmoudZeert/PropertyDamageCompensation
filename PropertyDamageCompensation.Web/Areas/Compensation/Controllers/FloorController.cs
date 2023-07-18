using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyDamageCompensation.Contracts.Dtos;
using PropertyDamageCompensation.Web.Areas.Compensation.Interfaces;
using PropertyDamageCompensation.Web.Areas.Compensation.Models;
using PropertyDamageCompensation.Web.Exceptions;

namespace PropertyDamageCompensation.Web.Areas.Compensation.Controllers
{


    [Area("Compensation")]
    [Authorize]
    public class FloorController : Controller
    {
        private readonly IFloorApiClient _floorApiClient;

        public FloorController( IFloorApiClient floorApiClient)
        {

            _floorApiClient = floorApiClient;
        }

        [Authorize(Roles = "Admin,Engineer,Applicant")]
        // GET: Compensation/FloorViewModel
        public async Task<IActionResult> Index()
        {
            var floorViewModel = (from floor in await _floorApiClient.GetAll()
                                  select new FloorViewModel
                                  {
                                      Id = floor.Id,
                                      Name = floor.Name
                                  }).ToList();

            return View(floorViewModel);
        }

        [Authorize(Roles = "Admin,Engineer,Applicant")]
        // GET: Compensation/FloorViewModel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException($"Floor with this Id : {id} is invalid Or Some thing wrong in the server !"); ;
            }

            var floor = await _floorApiClient.GetById((int)id);
            var floorViewModel = new FloorViewModel
            {
                Id = floor.Id,
                Name = floor.Name
            };

            if (floorViewModel == null)
            {
                throw new NotFoundException($"Floor with this Id {id} is not found !");
            }

            return View(floorViewModel);
        }
        [Authorize(Policy = "IsAdmin")]
        // GET: Compensation/FloorViewModel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Compensation/FloorViewModel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Create([Bind("Name")] FloorViewModel floorViewModel)
        {
            if (ModelState.IsValid)
            {
                var floorDto = new FloorDto
                {
                    Id = floorViewModel.Id,
                    Name = floorViewModel.Name
                };
                await _floorApiClient.Create(floorDto);

            }
            return View(floorViewModel);
        }
        [Authorize(Policy = "IsAdmin")]
        // GET: Compensation/FloorViewModel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException($" Id : {id} is invalid Or Some thing wrong in the server !"); ;
            }

            var floorViewModel =  (from floor in await _floorApiClient.GetAll()
                                        where floor.Id == id
                                        select new FloorViewModel
                                        {
                                            Id = floor.Id,
                                            Name = floor.Name
                                        }).FirstOrDefault(); 
            if (floorViewModel == null)
            {
                throw new NotFoundException($"Floor with this Id {id} is not found !");
            }
            return View(floorViewModel);
        }

        // POST: Compensation/FloorViewModel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] FloorViewModel floorViewModel)
        {
            if (id != floorViewModel.Id)
            {
                throw new BadRequestException($"Wrong Id : {id}  !"); ;
            }

            if (ModelState.IsValid)
            {
                var floor = await _floorApiClient.GetById(id);
                if (floor == null)
                {
                    throw new NotFoundException($" Floor with this Id : {id} no longer exists");
                }
                floor.Name = floorViewModel.Name;
                await _floorApiClient.Update(id,floor);
                return RedirectToAction(nameof(Index));
            }
            return View(floorViewModel);
        }

        [Authorize(Policy = "IsAdmin")]
        // GET: Compensation/FloorViewModel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null )
            {
                throw new BadRequestException($"Floor with this Id {id} Or Some thing wrong in the server !"); ;
            }

            var floorViewModel =  (from floor in await _floorApiClient.GetAll()
                                        where floor.Id == id
                                        select new FloorViewModel
                                        {
                                            Id = floor.Id,
                                            Name = floor.Name
                                        }).FirstOrDefault();
            if (floorViewModel == null)
            {
                throw new NotFoundException($" Floor with this Id : {id} no longer exists");
            }

            return View(floorViewModel);
        }

        // POST: Compensation/FloorViewModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var floor = await _floorApiClient.GetById(id);
            if (floor != null)
            {
                await _floorApiClient.Delete(id);
            }
            else
            {
                throw new DbUpdateConcurrencyException();
            }
            return RedirectToAction(nameof(Index));
        }


    }
}
