using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BuildingApp.Models;

namespace BuildingApp.Controllers
{
    public class RoomsController : Controller
    {
        private readonly ApartmentDBContext _context;

        public RoomsController(ApartmentDBContext context)
        {
            _context = context;    
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            var apartmentDBContext = _context.Room.Include(r => r.Price).Include(r => r.Type);
            return View(await apartmentDBContext.ToListAsync());

        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Room
                .Include(r => r.Price)
                .Include(r => r.Type)
                .SingleOrDefaultAsync(m => m.RoomID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create(long? priceID, int? typeID)
        {
            SelectList type;
            SelectList price;
           
            if (priceID != null && priceID != 0)
            {
                price = new SelectList(_context.Price, "PriceID", "Price1", priceID);
            }
            else
            {
                price = new SelectList(_context.Price, "PriceID", "Price1");
            }
            if (typeID != null && typeID != 0)
            {
                if (priceID != null && priceID != 0)
                {
                    type = new SelectList(_context.Type.Where(x => x.PriceID == priceID).OrderBy(s => s.Name), "TypeID", "Name", typeID);
                }
                else
                {
                    type = new SelectList(_context.Type.OrderBy(s => s.Name), "TypeID", "Name", typeID);
                }
            }
            else
            {
                if (priceID != null && priceID != 0)
                {
                    type = new SelectList(_context.Type.Where(x => x.PriceID == priceID).OrderBy(s => s.Name), "TypeID", "Name");
                }
                else
                {
                    type = new SelectList(_context.Type.OrderBy(s => s.Name), "TypeID", "Name");
                }
            }
            ViewBag.Types = type;
            ViewBag.Prices = price;
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomID,Name,Image,TypeID,PriceID,Capacity,Floor,MetaTiTle,Status")] Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["PriceID"] = new SelectList(_context.Price, "PriceID", "Price1", room.PriceID);
            ViewData["TypeID"] = new SelectList(_context.Type, "TypeID", "Name", room.TypeID);
            return View(room);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Room.SingleOrDefaultAsync(m => m.RoomID == id);
            if (room == null)
            {
                return NotFound();
            }
            ViewData["PriceID"] = new SelectList(_context.Price, "PriceID", "Price1", room.PriceID);
            ViewData["TypeID"] = new SelectList(_context.Type, "TypeID", "Name", room.TypeID);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomID,Name,Image,TypeID,PriceID,Capacity,Floor,MetaTiTle,Status")] Room room)
        {
            if (id != room.RoomID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.RoomID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["PriceID"] = new SelectList(_context.Price, "PriceID", "Price1", room.PriceID);
            ViewData["TypeID"] = new SelectList(_context.Type, "TypeID", "Name", room.TypeID);
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Room
                .Include(r => r.Price)
                .Include(r => r.Type)
                .SingleOrDefaultAsync(m => m.RoomID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Room.SingleOrDefaultAsync(m => m.RoomID == id);
            _context.Room.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool RoomExists(int id)
        {
            return _context.Room.Any(e => e.RoomID == id);
        }
    }
}
