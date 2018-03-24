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
    public class GcOutletsController : Controller
    {
        private readonly MarcoPoloGCDBContext _context;

        public GcOutletsController(MarcoPoloGCDBContext context)
        {
            _context = context;
        }

        // GET: GcOutlets
        public async Task<IActionResult> Index()
        {
            var marcoPoloGCDBContext = _context.Gcoutlet.Include(g => g.GiftCertificate).Include(g => g.Outlet);
            return View(await marcoPoloGCDBContext.ToListAsync());
        }

        // GET: GcOutlets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gcoutlet = await _context.Gcoutlet
                .Include(g => g.GiftCertificate)
                .Include(g => g.Outlet)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (gcoutlet == null)
            {
                return NotFound();
            }

            return View(gcoutlet);
        }

        // GET: GcOutlets/Create
        public IActionResult Create()
        {
            ViewData["GiftCertificateId"] = new SelectList(_context.GiftCertificate, "Id", "Id");
            ViewData["OutletId"] = new SelectList(_context.Outlet, "Id", "Id");
            return View();
        }

        // POST: GcOutlets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GiftCertificateId,OutletId,LastModifiedBy,CreatedDate,ModifiedDate")] Gcoutlet gcoutlet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gcoutlet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GiftCertificateId"] = new SelectList(_context.GiftCertificate, "Id", "Id", gcoutlet.GiftCertificateId);
            ViewData["OutletId"] = new SelectList(_context.Outlet, "Id", "Id", gcoutlet.OutletId);
            return View(gcoutlet);
        }

        // GET: GcOutlets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gcoutlet = await _context.Gcoutlet.SingleOrDefaultAsync(m => m.Id == id);
            if (gcoutlet == null)
            {
                return NotFound();
            }
            ViewData["GiftCertificateId"] = new SelectList(_context.GiftCertificate, "Id", "Id", gcoutlet.GiftCertificateId);
            ViewData["OutletId"] = new SelectList(_context.Outlet, "Id", "Id", gcoutlet.OutletId);
            return View(gcoutlet);
        }

        // POST: GcOutlets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GiftCertificateId,OutletId,LastModifiedBy,CreatedDate,ModifiedDate")] Gcoutlet gcoutlet)
        {
            if (id != gcoutlet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gcoutlet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GcoutletExists(gcoutlet.Id))
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
            ViewData["GiftCertificateId"] = new SelectList(_context.GiftCertificate, "Id", "Id", gcoutlet.GiftCertificateId);
            ViewData["OutletId"] = new SelectList(_context.Outlet, "Id", "Id", gcoutlet.OutletId);
            return View(gcoutlet);
        }

        // GET: GcOutlets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gcoutlet = await _context.Gcoutlet
                .Include(g => g.GiftCertificate)
                .Include(g => g.Outlet)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (gcoutlet == null)
            {
                return NotFound();
            }

            return View(gcoutlet);
        }

        // POST: GcOutlets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gcoutlet = await _context.Gcoutlet.SingleOrDefaultAsync(m => m.Id == id);
            _context.Gcoutlet.Remove(gcoutlet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GcoutletExists(int id)
        {
            return _context.Gcoutlet.Any(e => e.Id == id);
        }
    }
}
