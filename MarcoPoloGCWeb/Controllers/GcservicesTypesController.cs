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
    public class GcServicesTypesController : Controller
    {
        private readonly MarcoPoloGCDBContext _context;

        public GcServicesTypesController(MarcoPoloGCDBContext context)
        {
            _context = context;
        }

        // GET: GcservicesTypes
        public async Task<IActionResult> Index()
        {
            var marcoPoloGCDBContext = _context.GcservicesType.Include(g => g.GiftCertificate).Include(g => g.ServicesType);
            return View(await marcoPoloGCDBContext.ToListAsync());
        }

        // GET: GcservicesTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gcservicesType = await _context.GcservicesType
                .Include(g => g.GiftCertificate)
                .Include(g => g.ServicesType)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (gcservicesType == null)
            {
                return NotFound();
            }

            return View(gcservicesType);
        }

        // GET: GcservicesTypes/Create
        public IActionResult Create()
        {
            ViewData["GiftCertificateId"] = new SelectList(_context.GiftCertificate, "Id", "Id");
            ViewData["ServicesTypeId"] = new SelectList(_context.ServicesType, "Id", "Id");
            return View();
        }

        // POST: GcservicesTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GiftCertificateId,ServicesTypeId,LastModifiedBy,CreatedDate,ModifiedDate")] GcservicesType gcservicesType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gcservicesType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GiftCertificateId"] = new SelectList(_context.GiftCertificate, "Id", "Id", gcservicesType.GiftCertificateId);
            ViewData["ServicesTypeId"] = new SelectList(_context.ServicesType, "Id", "Id", gcservicesType.ServicesTypeId);
            return View(gcservicesType);
        }

        // GET: GcservicesTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gcservicesType = await _context.GcservicesType.SingleOrDefaultAsync(m => m.Id == id);
            if (gcservicesType == null)
            {
                return NotFound();
            }
            ViewData["GiftCertificateId"] = new SelectList(_context.GiftCertificate, "Id", "Id", gcservicesType.GiftCertificateId);
            ViewData["ServicesTypeId"] = new SelectList(_context.ServicesType, "Id", "Id", gcservicesType.ServicesTypeId);
            return View(gcservicesType);
        }

        // POST: GcservicesTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GiftCertificateId,ServicesTypeId,LastModifiedBy,CreatedDate,ModifiedDate")] GcservicesType gcservicesType)
        {
            if (id != gcservicesType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gcservicesType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GcservicesTypeExists(gcservicesType.Id))
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
            ViewData["GiftCertificateId"] = new SelectList(_context.GiftCertificate, "Id", "Id", gcservicesType.GiftCertificateId);
            ViewData["ServicesTypeId"] = new SelectList(_context.ServicesType, "Id", "Id", gcservicesType.ServicesTypeId);
            return View(gcservicesType);
        }

        // GET: GcservicesTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gcservicesType = await _context.GcservicesType
                .Include(g => g.GiftCertificate)
                .Include(g => g.ServicesType)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (gcservicesType == null)
            {
                return NotFound();
            }

            return View(gcservicesType);
        }

        // POST: GcservicesTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gcservicesType = await _context.GcservicesType.SingleOrDefaultAsync(m => m.Id == id);
            _context.GcservicesType.Remove(gcservicesType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GcservicesTypeExists(int id)
        {
            return _context.GcservicesType.Any(e => e.Id == id);
        }
    }
}
