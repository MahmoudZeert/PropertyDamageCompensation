using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PropertyDamageCompensation.Web.Areas.Compensation.Entities;
using PropertyDamageCompensation.Web.Areas.Compensation.Models;
using PropertyDamageCompensation.Web.Data;
using PropertyDamageCompensation.Web.Exceptions;

namespace PropertyDamageCompensation.Web.Areas.Compensation.Controllers
{
    [Authorize]
    [Area("Compensation")]
    public class TownController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TownController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin,Engineer,Applicant")]
        // GET: Compensation/Town
        public async Task<IActionResult> Index()
        {
            var townvms= await (  from  town in _context.Towns
                                  join district in _context.District on town.DistrictId equals district.Id
                        select new TownViewModel
                        { Id=town.Id,
                        Name=town.Name,DistrictId=town.DistrictId,
                            DistrictName=district.Name}).ToListAsync ();

              return View(townvms);
        }
        [Authorize(Roles = "Admin,Engineer,Applicant")]
        // GET: Compensation/Town/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Towns == null)
            {
                throw new BadRequestException($"Town with this Id {id} Or Some thing wrong in the server !"); ;
            }

            var townViewModel =  await (from town in _context.Towns
                                      where town.Id == id
                                      join district in _context.District on town.DistrictId equals district.Id
                                      select new TownViewModel
                                      { Id = town.Id, Name = town.Name,
                                          DistrictId=town.DistrictId,DistrictName=district.Name }).FirstOrDefaultAsync();
            if (townViewModel == null)
            {
                throw new NotFoundException($"Town with this Id {id} is not found !");

            }

            return View(townViewModel);
        }
        [Authorize(Policy = "IsAdmin")]
        // GET: Compensation/Town/Create
        public IActionResult Create()
        {
            ViewData["DistrictId"] = new SelectList(_context.District, "Id", "Name");

            return View();
        }

        // POST: Compensation/Town/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Create([Bind("Name,DistrictId")] TownViewModel townViewModel)
        {
            if (ModelState.IsValid)
            {
                var town = new Town
                {
                    Name = townViewModel.Name,
                    DistrictId=townViewModel.DistrictId
                };
                _context.Add(town);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(townViewModel);
        }

        [Authorize(Policy = "IsAdmin")]
        // GET: Compensation/Town/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Towns == null)
            {
                throw new BadRequestException($" Id : {id} is invalid Or Some thing wrong in the server !"); ;
            }
            ViewData["DistrictId"] = new SelectList(_context.District, "Id", "Name");
            var townViewModel = await (from town in _context.Towns
                                       where town.Id == id
                                       select new TownViewModel
                                       { Id = town.Id, Name = town.Name,DistrictId=town.DistrictId}).FirstOrDefaultAsync();
            if (townViewModel == null)
            {
                throw new NotFoundException($"Town with this Id {id} is not found !");
            }
            return View(townViewModel);
        }

        // POST: Compensation/Town/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DistrictId")] TownViewModel townViewModel)
        {
            if (id != townViewModel.Id)
            {
                throw new BadRequestException($"Wrong Id : {id}  !"); ;
            }

            if (ModelState.IsValid)
            {
                var town = await _context.Towns.FindAsync(id);
                if (town == null)
                {
                    throw new NotFoundException($" Town with this Id : {id} no longer exists");
                }
                town.Name=townViewModel.Name;
                town.DistrictId = townViewModel.DistrictId;
   
                _context.Update(town);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(townViewModel);
        }


        [Authorize(Policy = "IsAdmin")]
        // GET: Compensation/Town/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Towns == null)
            {
                throw new BadRequestException($"Town with this Id {id} Or Some thing wrong in the server !"); ;
            }

            var townViewModel = await (from town in _context.Towns
                                       where town.Id == id
                                       join district in _context.District
                                       on town.DistrictId equals district.Id
                                       select new TownViewModel
                                       { Id = town.Id, Name = town.Name,DistrictId=town.DistrictId,DistrictName=district.Name }).FirstOrDefaultAsync();
            if (townViewModel == null)
            {
                throw new NotFoundException($" Town with this Id : {id} no longer exists");

            }

            return View(townViewModel);
        }
        [Authorize(Policy = "IsAdmin")]
        // POST: Compensation/Town/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Towns == null)
            {
                throw new NotFoundException($" Internal Server error ");
            }
            var town = await(from mtown in _context.Towns
                             where mtown.Id == id 
                             select mtown ).FirstOrDefaultAsync();
            if (town != null)
            {
                _context.Towns.Remove(town);
            }
            else
            {
                throw new DbUpdateConcurrencyException();
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TownViewModelExists(int id)
        {
          return _context.Towns.Any(e => e.Id == id);
        }
    }
}
