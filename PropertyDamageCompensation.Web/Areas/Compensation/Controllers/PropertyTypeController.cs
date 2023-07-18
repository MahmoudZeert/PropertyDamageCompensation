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
    public class PropertyTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropertyTypeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin,Engineer,Applicant")]
        // GET: Compensation/PropertyType
        public async Task<IActionResult> Index()
        {
            var propertyTypeViewModel = await (from pt in _context.PropertyType
                                               select new PropertyTypeViewModel
                                               {
                                                   Id=pt.Id,
                                                   Name=pt.Name
                                               }).ToListAsync();
              return View(propertyTypeViewModel);
        }
        [Authorize(Roles= "Admin,Engineer,Applicant")]
        // GET: Compensation/PropertyType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PropertyType == null)
            {
                throw new BadRequestException($" Id :" +
                    $"{id} is invalid Or Some thing wrong in the server !"); ;
            }

            var propertyTypeVM = await (from pt in _context.PropertyType
                                        where pt.Id == id
                                        select new PropertyTypeViewModel
                                        {
                                            Name = pt.Name,
                                            Id = pt.Id
                                        }).FirstOrDefaultAsync();
                
            if (propertyTypeVM == null)
            {
                throw new NotFoundException($"Town with this Id {id} is not found !");
            }

            return View(propertyTypeVM);
        }
        [Authorize(Policy = "IsAdmin")]
        // GET: Compensation/PropertyType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Compensation/PropertyType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Create([Bind("Id,Name")] PropertyTypeViewModel propertyTypeVM)
        {
            if (ModelState.IsValid)
            {
                var properttype = new PropertyType
                {
                    Name = propertyTypeVM.Name
                };
                _context.Add(properttype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(propertyTypeVM);
        }

        [Authorize(Policy = "IsAdmin")]
        // GET: Compensation/PropertyType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PropertyType == null)
            {
                throw new BadRequestException($" Id : {id} is invalid Or Some thing wrong in the server !"); ;
            }

            var propertyTypeVM = await (from pt in _context.PropertyType
                                        where pt.Id == id
                                        select new PropertyTypeViewModel
                                        {
                                            Id = pt.Id,
                                            Name = pt.Name
                                        }).FirstOrDefaultAsync();
            if (propertyTypeVM == null)
            {
                throw new NotFoundException($"Town with this Id {id} is not found !");
            }
            return View(propertyTypeVM);
        }

        // POST: Compensation/PropertyType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PropertyTypeViewModel propertyTypeVM)
        {
            if (id != propertyTypeVM.Id)
            {
                throw new BadRequestException($"Wrong Id : {id}  !"); ;
            }

            if (ModelState.IsValid)
            {
                var propertType=await _context.PropertyType.FindAsync(id);
                if (propertType == null)
                {
                    throw new NotFoundException($" property Type with this Id : {id} no longer exists");
                }
                propertType.Name=propertyTypeVM.Name;
                _context.Update(propertType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(propertyTypeVM);
        }
        [Authorize(Policy = "IsAdmin")]
        // GET: Compensation/PropertyType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PropertyType == null)
            {
                throw new BadRequestException($" Id :{id} is invalid Or Some thing wrong in the server !"); ;
            }

            var propertyTypeVM = await (from pt in _context.PropertyType
                                      where pt.Id == id
                                      select new PropertyTypeViewModel
                                      {
                                          Id = pt.Id,
                                          Name = pt.Name
                                      }).FirstOrDefaultAsync();

            if (propertyTypeVM == null)
            {
                throw new NotFoundException($" Property Type  with this Id : {id} is not found");
            }

            return View(propertyTypeVM);
        }
        [Authorize(Policy = "IsAdmin")]
        // POST: Compensation/PropertyType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PropertyType == null)
            {
                throw new NotFoundException($" Internal Server error ");
            }
            var propertyType = await _context.PropertyType.FindAsync(id);
            if (propertyType != null)
            {
                _context.PropertyType.Remove(propertyType);
            }
            else
            {
                throw new DbUpdateConcurrencyException();
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyTypeExists(int id)
        {
          return _context.PropertyType.Any(e => e.Id == id);
        }
    }
}
