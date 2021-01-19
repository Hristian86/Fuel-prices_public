namespace AkciqApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AkciqApp.Data;
    using AkciqApp.Models.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Authorize]
    public class VisitorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VisitorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Visitors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ipAddresses
                .OrderByDescending(a => a.ModifiedOn)
                .Include(i => i.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Visitors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ipAddress = await _context.ipAddresses
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ipAddress == null)
            {
                return NotFound();
            }

            return View(ipAddress);
        }

        // GET: Visitors/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Visitors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Ip,Email,Visits,Info,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] IpAddress ipAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ipAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ipAddress.UserId);
            return View(ipAddress);
        }

        // GET: Visitors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ipAddress = await _context.ipAddresses.FindAsync(id);
            if (ipAddress == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ipAddress.UserId);
            return View(ipAddress);
        }

        // POST: Visitors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Ip,Email,Visits,Info,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] IpAddress ipAddress)
        {
            if (id != ipAddress.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ipAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IpAddressExists(ipAddress.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ipAddress.UserId);
            return View(ipAddress);
        }

        // GET: Visitors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ipAddress = await _context.ipAddresses
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ipAddress == null)
            {
                return NotFound();
            }

            return View(ipAddress);
        }

        // POST: Visitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ipAddress = await _context.ipAddresses.FindAsync(id);
            _context.ipAddresses.Remove(ipAddress);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IpAddressExists(int id)
        {
            return _context.ipAddresses.Any(e => e.Id == id);
        }
    }
}
