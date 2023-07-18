using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PropertyDamageCompensation.Web.Areas.Compensation.Entities;
using PropertyDamageCompensation.Web.Areas.Compensation.Models;
using PropertyDamageCompensation.Web.Data;
using PropertyDamageCompensation.Web.Exceptions;

namespace PropertyDamageCompensation.Web.Areas.Compensation.Controllers
{
    [Authorize]
    [Area("Compensation")]
    public class ApplicationStateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationStateController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Compensation/ApplicationState
        [Authorize(Roles = "Admin,Engineer,Applicant")]
        public async Task<IActionResult> Index()
        {
            var applicationStateViewModel = await (from apps in _context.ApplicationState
                                          select new ApplicationStateViewModel
                                          {
                                              Id = apps.Id,
                                              Name = apps.Name,
                                              IsDefault=apps.IsDefault
                                          }).ToListAsync();
              return View(applicationStateViewModel);
        }
        [Authorize(Roles= "Admin,Engineer,Applicant")]
        // GET: Compensation/ApplicationState/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ApplicationState == null)
            {
                throw new BadRequestException($"Application State with this Id {id} is invalid Or Some thing wrong in the server !"); ;
            }

            var applicationStateViewModel = await (from aps in _context.ApplicationState
                                                   where aps.Id == id
                                                   select new ApplicationStateViewModel
                                                   {
                                                       Id = aps.Id,
                                                       Name = aps.Name
                                                   }).FirstOrDefaultAsync();

            if (applicationStateViewModel == null)
            {
                throw new NotFoundException($"Application State with this Id {id} is not found !");
            }

            return View(applicationStateViewModel);
        }
        [Authorize(Policy = "IsAdmin")]
        // GET: Compensation/ApplicationState/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Compensation/ApplicationState/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Create([Bind("Name")] ApplicationStateViewModel applicationStateViewModel)
        {
            if (ModelState.IsValid)
            {
                var applicationState = new ApplicationState
                {
                    Name = applicationStateViewModel.Name
                };
                _context.Add(applicationState);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(applicationStateViewModel);
        }
        [Authorize(Policy = "IsAdmin")]
        // GET: Compensation/ApplicationState/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ApplicationState == null)
            {
                throw new BadRequestException($" Id : {id} is invalid Or Some thing wrong in the server !"); ;
            }

            var applicationStateViewModel = await (from aps in _context.ApplicationState
                                                   where aps.Id == id
                                                   select new ApplicationStateViewModel
                                                   {
                                                       Name = aps.Name,
                                                       Id = aps.Id
                                                   }).FirstOrDefaultAsync();
            if (applicationStateViewModel == null)
            {
                throw new NotFoundException($"Application State with this Id {id} is not found !");
            }
            return View(applicationStateViewModel);
        }

        // POST: Compensation/ApplicationState/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ApplicationStateViewModel applicationStateViewModel)
        {
            if (id != applicationStateViewModel.Id)
            {
                throw new BadRequestException($"Wrong Id : {id}  !"); ;
            }

            if (ModelState.IsValid)
            {
                var applicatipmState = await _context.ApplicationState.FindAsync(id);
                if (applicatipmState == null)
                {
                    throw new NotFoundException($" Application State with this Id : {id} no longer exists");
                };
                applicatipmState.Name = applicationStateViewModel.Name;                  
                _context.Update(applicatipmState);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicationStateViewModel);
        }
        [Authorize(Policy = "IsAdmin")]
        // GET: Compensation/ApplicationState/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ApplicationState == null)
            {
                throw new NotFoundException($"Application State with this Id {id} is not found !");             }

            var applicationStateViewModel = await (from aps in _context.ApplicationState
                                                   where aps.Id == id
                                                   select new ApplicationStateViewModel
                                                   {
                                                       Id = aps.Id,
                                                       Name = aps.Name
                                                   }).FirstOrDefaultAsync();

            if (applicationStateViewModel == null)
            {
                throw new NotFoundException($"Application State with this Id {id} is not found !");  
            }

            return View(applicationStateViewModel);
        }
        [Authorize(Policy = "IsAdmin")]
        // POST: Compensation/ApplicationState/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ApplicationState == null)
            {
                throw new NotFoundException($" Internal Server error ");
            }
            var applicationState = await _context.ApplicationState.FindAsync(id);
            if (applicationState != null)
            {
                _context.ApplicationState.Remove(applicationState);
            }
            else
            {
                throw new DbUpdateConcurrencyException();
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool ApplicationStateViewModelExists(int id)
        {
          return _context.ApplicationState.Any(e => e.Id == id);
        }
    }
}
