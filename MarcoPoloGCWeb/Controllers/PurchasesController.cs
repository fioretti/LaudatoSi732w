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
    public class PurchasesController : Controller
    {
        private readonly MarcoPoloGCDBContext _context;

        public PurchasesController(MarcoPoloGCDBContext context)
        {
            _context = context;
        }

        // GET: Gcpurchases
        public async Task<IActionResult> Index()
        {
            var marcoPoloGCDBContext = _context.Gcpurchase.Include(g => g.GiftCertificate);
            return View(await marcoPoloGCDBContext.ToListAsync());
        }

        // GET: Gcpurchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gcpurchase = await _context.Gcpurchase
                .Include(g => g.GiftCertificate)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (gcpurchase == null)
            {
                return NotFound();
            }

            return View(gcpurchase);
        }

        // GET: Gcpurchases/Create
        public IActionResult Create()
        {
            ViewData["GiftCertificateId"] = new SelectList(_context.GiftCertificate, "Id", "Id");
            return View();
        }

        // POST: Gcpurchases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GiftCertificateId,PurchaseDate,LastModifiedBy,CreatedDate,ModifiedDate")] Gcpurchase gcpurchase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gcpurchase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GiftCertificateId"] = new SelectList(_context.GiftCertificate, "Id", "Id", gcpurchase.GiftCertificateId);
            return View(gcpurchase);
        }

        // GET: Gcpurchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gcpurchase = await _context.Gcpurchase.SingleOrDefaultAsync(m => m.Id == id);
            if (gcpurchase == null)
            {
                return NotFound();
            }
            ViewData["GiftCertificateId"] = new SelectList(_context.GiftCertificate, "Id", "Id", gcpurchase.GiftCertificateId);
            return View(gcpurchase);
        }

        // POST: Gcpurchases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GiftCertificateId,PurchaseDate,LastModifiedBy,CreatedDate,ModifiedDate")] Gcpurchase gcpurchase)
        {
            if (id != gcpurchase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gcpurchase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GcpurchaseExists(gcpurchase.Id))
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
            ViewData["GiftCertificateId"] = new SelectList(_context.GiftCertificate, "Id", "Id", gcpurchase.GiftCertificateId);
            return View(gcpurchase);
        }

        // GET: Gcpurchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gcpurchase = await _context.Gcpurchase
                .Include(g => g.GiftCertificate)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (gcpurchase == null)
            {
                return NotFound();
            }

            return View(gcpurchase);
        }

        // POST: Gcpurchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gcpurchase = await _context.Gcpurchase.SingleOrDefaultAsync(m => m.Id == id);
            _context.Gcpurchase.Remove(gcpurchase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GcpurchaseExists(int id)
        {
            return _context.Gcpurchase.Any(e => e.Id == id);
        }
    }
}
