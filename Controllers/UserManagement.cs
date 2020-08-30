using InventoryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Controllers
{
    public class UserManagement : Controller
    {
        private readonly UserContext _context;
        public UserManagement(UserContext context)
        {
            _context = context;
        }
        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserManagement.ToListAsync());
        }
        // GET: Users/Details/5
        public async Task<IActionResult> Details(string? uname)
        {
            if (uname == null)
            {
                return NotFound();
            }

            var users = await _context.UserManagement
                .FirstOrDefaultAsync(m => m.UserName == uname);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,UserName,Password,Role,TelNo,Gender,Dob")] UserManagement userManagement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userManagement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userManagement);
        }
    }
}
