﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MarcoPoloGCWeb.Models;

namespace MarcoPoloGCWeb.Controllers
{
    public class UsersController : Controller
    {
        private readonly MarcoPoloGCDBContext _context;

        public UsersController(MarcoPoloGCDBContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.AspNetUsers.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUsers = await _context.AspNetUsers
                .SingleOrDefaultAsync(m => m.Id == id);
            if (aspNetUsers == null)
            {
                return NotFound();
            }

            return View(aspNetUsers);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName")] AspNetUsers aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aspNetUsers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aspNetUsers);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUsers = await _context.AspNetUsers.SingleOrDefaultAsync(m => m.Id == id);
            if (aspNetUsers == null)
            {
                return NotFound();
            }
            return View(aspNetUsers);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName")] AspNetUsers aspNetUsers)
        {
            if (id != aspNetUsers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aspNetUsers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspNetUsersExists(aspNetUsers.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aspNetUsers);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUsers = await _context.AspNetUsers
                .SingleOrDefaultAsync(m => m.Id == id);
            if (aspNetUsers == null)
            {
                return NotFound();
            }

            return View(aspNetUsers);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var aspNetUsers = await _context.AspNetUsers.SingleOrDefaultAsync(m => m.Id == id);
            _context.AspNetUsers.Remove(aspNetUsers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspNetUsersExists(string id)
        {
            return _context.AspNetUsers.Any(e => e.Id == id);
        }
    }
}