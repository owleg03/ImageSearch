using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ImageSearch.Data;
using ImageSearch.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ImageSearch.Controllers
{
    [Authorize]
    public class AdminsController : Controller
    {
        private readonly DataContext _context;

        public AdminsController(DataContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        // GET: Admins/LogIn
        [AllowAnonymous]
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        // POST: Admins/LogIn/
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn([Bind("Login,Password")] LogInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Login == model.Login &&
                                                                      a.Password == model.Password);
                if (admin != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, admin.Login)
                    };
                    ClaimsIdentity identity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Incorrect login/password.");
            }
            return View(model);
        }

        // GET: Admins
        public async Task<IActionResult> Index()
        {
            return _context.Admins != null ? 
                   View(await _context.Admins.ToListAsync()) :
                   Problem("Entity set 'DataContext.Admins'  is null.");
        }

        // GET: Admins/Details
        public async Task<IActionResult> Details()
        {
            string login = User.Claims.Select(x => x.Subject.Name).First();

            var admin = await _context.Admins.FirstOrDefaultAsync(m => m.Login == login);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: Admins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Login,Password")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                Admin adminDb = await _context.Admins.FirstOrDefaultAsync(a => a.Login == admin.Login);
                if (adminDb != null)
                {
                    ModelState.AddModelError("", "This login is taken.");
                    return View(admin);
                }

                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // GET: Admins/Edit
        public async Task<IActionResult> Edit()
        {
            string login = User.Claims.Select(x => x.Subject.Name).First();

            var admin = await _context.Admins.FirstOrDefaultAsync(m => m.Login == login);

            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Login,Password")] Admin admin)
        {
            if (id != admin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("index");
            }
            return View(admin);
        }

        // GET: Admins/Delete
        public async Task<IActionResult> Delete()
        {
            string login = User.Claims.Select(x => x.Subject.Name).First();

            var admin = await _context.Admins.FirstOrDefaultAsync(m => m.Login == login);

            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Admins == null)
            {
                return Problem("Entity set 'DataContext.Admin'  is null.");
            }
            var admin = await _context.Admins.FindAsync(id);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("LogOut");
        }

        private bool AdminExists(int id)
        {
          return (_context.Admins?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
