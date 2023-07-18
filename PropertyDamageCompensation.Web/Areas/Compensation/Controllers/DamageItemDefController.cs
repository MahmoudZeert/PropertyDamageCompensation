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
    [Authorize]
    public class DamageItemDefController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DamageItemDefController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin,Engineer")]
        // GET: Compensation/DamageItemDef
        public async Task<IActionResult> Index()
        {
            var damageItemDefViewModel = await (from dif in _context.DamageItemDef
                                                   select new DamageItemDefViewModel
                                                   {
                                                       Id = dif.Id,
                                                       Name = dif.Name,
                                                       UnitPrice = dif.UnitPrice
                                                   }).ToListAsync();
            return View(damageItemDefViewModel);
        }
        [Authorize(Roles = "Admin,Engineer")]
        // GET: Compensation/DamageItemDef/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DamageItemDef == null)
            {
                throw new BadRequestException($"Application State with this Id {id} is invalid Or Some thing wrong in the server !"); ;
            }

            var damageItemDefViewModel = await (from dif in _context.DamageItemDef
                                                   where dif.Id == id
                                                   select new DamageItemDefViewModel
                                                   {
                                                       Id = dif.Id,
                                                       Name = dif.Name,
                                                       UnitPrice= dif.UnitPrice

                                                   }).FirstOrDefaultAsync();

            if (damageItemDefViewModel == null)
            {
                throw new NotFoundException($"Damage Item def  with this Id {id} is not found !");
            }

            return View(damageItemDefViewModel);
        }
        [Authorize(Roles = "Admin")]
        // GET: Compensation/DamageItemDef/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Compensation/DamageItemDef/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,UnitPrice")] DamageItemDefViewModel damageItemDefViewModel)
        {

            if (ModelState.IsValid)
            {
                var damageItemDef = new DamageItemDef
                {
                    Name = damageItemDefViewModel.Name,
                    UnitPrice= damageItemDefViewModel.UnitPrice
                };

                _context.Add(damageItemDef);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(damageItemDefViewModel);
        }
        [Authorize(Policy = "IsAdmin")]
        // GET: Compensation/DamageItemDef/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DamageItemDef == null)
            {
                throw new BadRequestException($" Id : {id} is invalid Or Some thing wrong in the server !"); ;
            }

            var damageItemDefViewModel = await (from dif in _context.DamageItemDef
                                                   where dif.Id == id
                                                   select new DamageItemDefViewModel
                                                   {
                                                       Name = dif.Name,
                                                       Id = dif.Id,
                                                       UnitPrice=dif.UnitPrice
                                                   }).FirstOrDefaultAsync();
            if (damageItemDefViewModel == null)
            {
                throw new NotFoundException($"Damage Item Definition State with this Id {id} is not found !");
            }
 
            return View(damageItemDefViewModel);
        }

        // POST: Compensation/DamageItemDef/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UnitPrice")] DamageItemDefViewModel damageItemDefViewModel)
        {
            if (id != damageItemDefViewModel.Id)
            {
                throw new BadRequestException($"Wrong Id : {id}  !"); ;
            }

            if (ModelState.IsValid)
            {
                var damageItemDef= await _context.DamageItemDef.FindAsync(id);
                if (damageItemDef == null)
                {
                    throw new NotFoundException($" Application State with this Id : {id} no longer exists");
                };
                damageItemDef.Name = damageItemDefViewModel.Name;
                damageItemDef.UnitPrice= damageItemDefViewModel.UnitPrice;
                _context.Update(damageItemDef);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(damageItemDefViewModel);
        }

        // GET: Compensation/DamageItemDef/Delete/5
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DamageItemDef == null)
            {
                throw new NotFoundException($"Dmage Item Definition  with this Id {id} is not found !");
            }

            var damageItemDefViewModel = await (from dif in _context.DamageItemDef
                                                   where dif.Id == id
                                                   select new DamageItemDefViewModel
                                                   {
                                                       Id = dif.Id,
                                                       Name = dif.Name,
                                                       UnitPrice=dif.UnitPrice
                                                   }).FirstOrDefaultAsync();

            if (damageItemDefViewModel == null)
            {
                throw new NotFoundException($"Damge Item Def  with this Id {id} is not found !");
            }

            return View(damageItemDefViewModel);
        }

        // POST: Compensation/DamageItemDef/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DamageItemDef == null)
            {
                throw new NotFoundException($" Internal Server error ");
            }
            var damageItemDef = await _context.DamageItemDef.FindAsync(id);
            if (damageItemDef != null)
            {
                _context.DamageItemDef.Remove(damageItemDef);
            }
            else
            {
                throw new DbUpdateConcurrencyException();
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool DamageItemDefViewModelExists(int id)
        //{
        //  return (_context.DamageItemDefViewModel?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
