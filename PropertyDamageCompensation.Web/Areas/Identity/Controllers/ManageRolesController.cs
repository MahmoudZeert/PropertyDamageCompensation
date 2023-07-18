using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyDamageCompensation.Web.Areas.Identity.Models;
using PropertyDamageCompensation.Web.Data;

namespace PropertyDamageCompensation.Web.Areas.Identity.Controllers
{
    [Authorize(Policy = "IsAdmin")]
    [Area("Identity")]
    public class ManageRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManageRolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var roles = await (from role in _roleManager.Roles
                               select new RoleViewModel
                               {
                                   Id=role.Id,
                                   Name=role.Name 

                               }).ToListAsync();
            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if(roleName == null)
            {
                ModelState.AddModelError("", "Role Name is required");
                return View("Index");
            }
            await _roleManager.CreateAsync(new IdentityRole(roleName));
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async  Task<IActionResult> Edit(string id)
        {
            if(id != null)
            {
                var roleVM = await (from role in _roleManager.Roles
                                    where role.Id == id
                                    select new RoleViewModel
                                    {
                                        Id = role.Id,
                                        Name = role.Name
                                    }).FirstOrDefaultAsync();
                if (roleVM == null)
                {
                    return NotFound();
                }

                return View(roleVM);

            }
            return NotFound();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name")] RoleViewModel roleViewModel)
        {
            if (roleViewModel == null || id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return NotFound();
                }
                role.Name = roleViewModel.Name;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(roleViewModel);



        }
        [HttpGet,ActionName("Delete")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (id != null)
            {
                var roleVM = await (from role in _roleManager.Roles
                                    where role.Id == id
                                    select new RoleViewModel
                                    {
                                        Id = role.Id,
                                        Name = role.Name
                                    }).FirstOrDefaultAsync();
                if (roleVM == null)
                {
                    return NotFound();
                }

                return View(roleVM);

            }
            return NotFound();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (_roleManager.Roles == null || id == null)
            {
                return NotFound();
            }
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            
            return View();



        }


    }
}
