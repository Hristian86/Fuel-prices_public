namespace AkciqApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AkciqApp.Data;
    using AkciqApp.Models;
    using AkciqApp.Models.Models;
    using AkciqApp.Services.GasStation;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Authorize]
    public class GasStationsController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IGasStationService gasService;

        public GasStationsController(ApplicationDbContext context,
            IGasStationService gasService)
        {
            db = context;
            this.gasService = gasService;
        }

        // GET: GasStations
        public async Task<IActionResult> Index()
        {
            return View(await db.GasStations.ToListAsync());
        }

        public async Task<IActionResult> UpdateTables()
        {
            await this.gasService.UpdateGasStationsManually();

            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: GasStations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasStation = await db.GasStations
                .FirstOrDefaultAsync(m => m.id == id);
            if (gasStation == null)
            {
                return NotFound();
            }

            return View(gasStation);
        }

        // GET: GasStations/Create
        public IActionResult Create()
        {
            GasViewModel gasStation = new GasViewModel();
            gasStation.Addresses = this.db.Addresses.ToList();
            return View(gasStation);
        }

        // POST: GasStations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GasStationURL,GasStationName,ParamStart,ParamEnd,City,Country,id,CreatedOn")] GasViewModel gasStation)
        {
            if (ModelState.IsValid)
            {
                if (gasStation.ParamStart == null)
                {
                    gasStation.ParamStart = "<tbody>";
                }

                if (gasStation.ParamEnd == null)
                {
                    gasStation.ParamEnd = "</tbody>";
                }

                //bool containCity = this.db.Addresses.Any(x => x.City == gasStation.City);
                //if (!containCity)
                //{
                //    this.db.Addresses.Add(new Address()
                //    {
                //        City = gasStation.City,
                //    });
                //}

                GasStation stationEntity = new GasStation()
                {
                    City = gasStation.City,
                    Country = gasStation.Country,
                    CreatedOn = DateTime.Now,
                    GasStationName = gasStation.GasStationName,
                    GasStationURL = gasStation.GasStationURL,
                    ModifiedOn = gasStation.ModifiedOn,
                    ParamEnd = gasStation.ParamEnd,
                    ParamStart = gasStation.ParamStart,
                    TableData = gasStation.TableData,
                    IpAddress = gasStation.IpAddress,
                };

                db.Add(stationEntity);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(gasStation);
        }

        // GET: GasStations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasStation = await db.GasStations.FindAsync(id);
            if (gasStation == null)
            {
                return NotFound();
            }
            return View(gasStation);
        }

        // POST: GasStations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GasStationURL,GasStationName,ParamStart,ParamEnd,City,Country,id")] GasStation gasStation)
        {
            if (id != gasStation.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(gasStation);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GasStationExists(gasStation.id))
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
            return View(gasStation);
        }

        // GET: GasStations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasStation = await db.GasStations
                .FirstOrDefaultAsync(m => m.id == id);
            if (gasStation == null)
            {
                return NotFound();
            }

            return View(gasStation);
        }

        // POST: GasStations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gasStation = await db.GasStations.FindAsync(id);
            db.GasStations.Remove(gasStation);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GasStationExists(int id)
        {
            return db.GasStations.Any(e => e.id == id);
        }
    }
}
