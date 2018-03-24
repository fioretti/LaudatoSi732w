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
    public class RedemptionsController : Controller
    {
        private readonly MarcoPoloGCDBContext _context;

        public RedemptionsController(MarcoPoloGCDBContext context)
        {
            _context = context;
        }

        // GET: Gcredemptions
        public async Task<IActionResult> Index()
        {
            var marcoPoloGCDBContext = _context.Gcredemption.Include(g => g.GiftCertificate);
            return View(await marcoPoloGCDBContext.ToListAsync());
        }

        // GET: Gcredemptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gcredemption = await _context.Gcredemption
                .Include(g => g.GiftCertificate)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (gcredemption == null)
            {
                return NotFound();
            }

            return View(gcredemption);
        }

        // GET: Gcredemptions/Create
        public IActionResult Create()
        {
            ViewData["GiftCertificateId"] = new SelectList(_context.GiftCertificate, "Id", "Id");
            return View();
        }

        // POST: Gcredemptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GiftCertificateId,RedemptionDate,LastModifiedBy,CreatedDate,ModifiedDate")] Gcredemption gcredemption)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gcredemption);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GiftCertificateId"] = new SelectList(_context.GiftCertificate, "Id", "Id", gcredemption.GiftCertificateId);
            return View(gcredemption);
        }

        // GET: Gcredemptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gcredemption = await _context.Gcredemption.SingleOrDefaultAsync(m => m.Id == id);
            if (gcredemption == null)
            {
                return NotFound();
            }
            ViewData["GiftCertificateId"] = new SelectList(_context.GiftCertificate, "Id", "Id", gcredemption.GiftCertificateId);
            return View(gcredemption);
        }

        // POST: Gcredemptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GiftCertificateId,RedemptionDate,LastModifiedBy,CreatedDate,ModifiedDate")] Gcredemption gcredemption)
        {
            if (id != gcredemption.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gcredemption);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GcredemptionExists(gcredemption.Id))
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
            ViewData["GiftCertificateId"] = new SelectList(_context.GiftCertificate, "Id", "Id", gcredemption.GiftCertificateId);
            return View(gcredemption);
        }

        // GET: Gcredemptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gcredemption = await _context.Gcredemption
                .Include(g => g.GiftCertificate)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (gcredemption == null)
            {
                return NotFound();
            }

            return View(gcredemption);
        }

        // POST: Gcredemptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gcredemption = await _context.Gcredemption.SingleOrDefaultAsync(m => m.Id == id);
            _context.Gcredemption.Remove(gcredemption);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GcredemptionExists(int id)
        {
            return _context.Gcredemption.Any(e => e.Id == id);
        }
    }
}
