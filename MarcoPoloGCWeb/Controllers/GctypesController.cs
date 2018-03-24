using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MarcoPoloGCWeb.Models;

namespace MarcoPoloGCWeb.Controllers
{
    public class GcTypesController : Controller
    {
        private readonly MarcoPoloGCDBContext _context;

        public GcTypesController(MarcoPoloGCDBContext context)
        {
            _context = context;
        }

        // GET: Gctypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gctype.ToListAsync());
        }

        // GET: Gctypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gctype = await _context.Gctype
                .SingleOrDefaultAsync(m => m.Id == id);
            if (gctype == null)
            {
                return NotFound();
            }

            return View(gctype);
        }

        // GET: Gctypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gctypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Gctype1,LastModifiedBy,CreatedDate,ModifiedDate")] Gctype gctype)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gctype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gctype);
        }

        // GET: Gctypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gctype = await _context.Gctype.SingleOrDefaultAsync(m => m.Id == id);
            if (gctype == null)
            {
                return NotFound();
            }
            return View(gctype);
        }

        // POST: Gctypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Gctype1,LastModifiedBy,CreatedDate,ModifiedDate")] Gctype gctype)
        {
            if (id != gctype.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gctype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GctypeExists(gctype.Id))
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
            return View(gctype);
        }

        // GET: Gctypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gctype = await _context.Gctype
                .SingleOrDefaultAsync(m => m.Id == id);
            if (gctype == null)
            {
                return NotFound();
            }

            return View(gctype);
        }

        // POST: Gctypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gctype = await _context.Gctype.SingleOrDefaultAsync(m => m.Id == id);
            _context.Gctype.Remove(gctype);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GctypeExists(int id)
        {
            return _context.Gctype.Any(e => e.Id == id);
        }
    }
}
