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
    [Area("Compensation")]
    public class DistrictController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DistrictController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles="Admin,Engineer,Applicant")]
        // GET: Compensation/District
        public async Task<IActionResult> Index()
        {
            var districtViewModel=await (from district in _context.District
                                        select new DistrictViewModel
                                        {
                                            Id= district.Id,
                                            Name= district.Name
                                        }).ToListAsync();
            return View(districtViewModel);
        }

        [Authorize(Roles = "Admin,Engineer,Applicant")]
        // GET: Compensation/District/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.District == null)
            {
                throw new BadRequestException($"Floor with this Id : {id} is invalid Or Some thing wrong in the server !"); ;
            }
            var districViewModel = await (from district in _context.District
                                        where district.Id == id
                                        select new DistrictViewModel
                                        {
                                            Id = district.Id,
                                            Name = district.Name
                                        }).FirstOrDefaultAsync();

            if (districViewModel == null)
            {
                throw new NotFoundException($"District with this Id {id} is not found !");
            }

            return View(districViewModel);

        }
        [Authorize(Policy = "IsAdmin")]
        // GET: Compensation/District/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Compensation/District/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] DistrictViewModel districtViewModel)
        {
            if (ModelState.IsValid)
            {
                var district = new District
                {
                    Name = districtViewModel.Name
                };
                _context.Add(district);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(districtViewModel);
        }
        [Authorize(Policy = "IsAdmin")]
        // GET: Compensation/District/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.District == null)
            {
                throw new BadRequestException($"District with this Id : {id} is invalid Or Some thing wrong in the server !"); ;
            }

            var districtViewModel = await (from distritc in _context.District
                                           where distritc.Id == id
                                           select new DistrictViewModel
                                           {
                                               Id = distritc.Id,
                                               Name = distritc.Name
                                           }).FirstOrDefaultAsync();

            if (districtViewModel == null)
            {
                throw new NotFoundException($"District with this Id : {id} is not found!"); ;
            }
            return View(districtViewModel);
        }

        // POST: Compensation/District/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsAdmin")]

        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] DistrictViewModel districtViewModel)
        {
            if (id != districtViewModel.Id)
            {
                throw new BadRequestException($"Wrong Id : {id}  !"); ;
            }

            if (ModelState.IsValid)
            {

                var district = await _context.District.FindAsync(id);
                if (district == null)
                {
                    throw new NotFoundException($" District with this Id : {id} no longer exists");
                }
                district.Name = districtViewModel.Name;
                _context.Update(district);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(districtViewModel);
        }
        [Authorize(Policy ="IsAdmin")]
        // GET: Compensation/District/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.District == null)
            {
                throw new BadRequestException($"District with this Id : {id} is invalid Or Some thing wrong in the server !"); ;
            }

            var districtViewModel = await (from distritc in _context.District
                                           where distritc.Id == id
                                           select new DistrictViewModel
                                           {
                                               Id = distritc.Id,
                                               Name = distritc.Name
                                           }).FirstOrDefaultAsync();

            if (districtViewModel == null)
            {
                throw new NotFoundException($"District with this Id : {id} is not found!"); ;
            }

            return View(districtViewModel);
        }

        // POST: Compensation/District/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.District == null)
            {
                throw new NotFoundException($" Internal Server error ");
            }
            var district = await _context.District.FindAsync(id);
            if (district != null)
            {
                _context.District.Remove(district);
            }
            else
            {
                throw new DbUpdateConcurrencyException();
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DistrictExists(int id)
        {
          return (_context.District?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
