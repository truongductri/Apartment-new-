using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BuildingApp.Models;

using BuildingApp.Models.ModelView;

namespace BuildingApp.Controllers
{
    public class ContractsController : Controller
    {
        private readonly ApartmentDBContext _context;
       


        public ContractsController(ApartmentDBContext context)
        {
            _context = context;    
        }

        // GET: Contracts
        public async Task<IActionResult> Index()
        {
            var apartmentDBContext = _context.Contract.Include(c => c.Customer).Include(c => c.Room);
            return View(await apartmentDBContext.ToListAsync());
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contract
                .Include(c => c.Customer)
                .Include(c => c.Room)
                .SingleOrDefaultAsync(m => m.ConstractID == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // GET: Contracts/Create
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

        // POST: Contracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConstractID,Name,RoomID,CustomerID,NumOfMonth,BeginDate,EndDate,MetaTitle,Status")] ContractViewModel contract, long? priceID, int? typeID)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contract);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            
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
            return View(contract);
        }
        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contract.SingleOrDefaultAsync(m => m.ConstractID == id);
            if (contract == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "CustomerID", contract.CustomerID);
            ViewData["RoomID"] = new SelectList(_context.Room, "RoomID", "RoomID", contract.RoomID);
            return View(contract);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConstractID,Name,RoomID,CustomerID,NumOfMonth,BeginDate,EndDate,MetaTitle,Status")] Contract contract)
        {
            if (id != contract.ConstractID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contract);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractExists(contract.ConstractID))
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
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Name", contract.CustomerID);
            ViewData["RoomID"] = new SelectList(_context.Room, "RoomID", "Name", contract.RoomID);
            return View(contract);
        }

        // GET: Contracts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contract
                .Include(c => c.Customer)
                .Include(c => c.Room)
                .SingleOrDefaultAsync(m => m.ConstractID == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract = await _context.Contract.SingleOrDefaultAsync(m => m.ConstractID == id);
            _context.Contract.Remove(contract);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ContractExists(int id)
        {
            return _context.Contract.Any(e => e.ConstractID == id);
        }
    }
}
