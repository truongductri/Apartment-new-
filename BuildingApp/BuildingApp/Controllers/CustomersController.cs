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
    public class CustomersController : Controller
    {
        private readonly ApartmentDBContext _context;

        public CustomersController(ApartmentDBContext context)
        {
            _context = context;    
        }

        // GET: Customers
        public IActionResult Index(int? ProvinceId, int? DistrictId)
        {
            SelectList province;
            SelectList district;
            if (ProvinceId != null && ProvinceId != 0)
            {
                province = new SelectList(_context.Province.OrderBy(s => s.ProvinceName), "ProvinceID", "ProvinceName", ProvinceId);
            }
            else
            {
                province = new SelectList(_context.Province.OrderBy(s => s.ProvinceName), "ProvinceID", "ProvinceName");
            }
            if (DistrictId != null && DistrictId != 0)
            {
                if (ProvinceId != null && ProvinceId != 0)
                {
                    district = new SelectList(_context.District.Where(x => x.ProvinceID == ProvinceId).OrderBy(s => s.DistrictName), "DistrictID", "DistrictName",DistrictId);
                }
                else
                {
                    district = new SelectList(_context.District.OrderBy(s => s.DistrictName), "DistrictID", "DistrictName", DistrictId);
                }
            }
            else
            {
                if (ProvinceId != null && ProvinceId != 0)
                {
                    district = new SelectList(_context.District.Where(x => x.ProvinceID == ProvinceId).OrderBy(s => s.DistrictName), "DistrictID", "DistrictName");
                }
                else
                {
                    district = new SelectList(_context.District.OrderBy(s => s.DistrictName), "DistrictID", "DistrictName");
                }
            }
            var CustomerContext = _context.Customer.ToAsyncEnumerable();
            if (ProvinceId != null && ProvinceId != 0)
            {
                CustomerContext = CustomerContext.Where(x => x.ProvinceID == ProvinceId);
            }
            if (DistrictId != null && DistrictId != 0)
            {
                CustomerContext = CustomerContext.Where(x => x.DistrictID == DistrictId);
            }
            var customer = CustomerContext.Select(j => new Customer()
            {
                CustomerID = j.CustomerID,
                Name = j.Name,
                Age = j.Age,
                BirthDay = j.BirthDay,
                Sex = j.Sex,
                Identify = j.Identify,
                Phone = j.Phone,
                Email = j.Email
            }).ToEnumerable();

            ViewBag.Provinces = province;
            ViewBag.Districts = district;
            ViewBag.Customers = customer;
            return View();
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .SingleOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["ProvinceID"]= new SelectList(_context.Province,"ProvinceID", "ProvinceName");
            ViewData["DistrictID"] = new SelectList(_context.District, "DistrictID", "DistrictName");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID,Name,Age,BirthDay,Sex,ProvinceID,DistrictID,Identify,Phone,Email,MetaTitle")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ProvinceID"] = new SelectList(_context.Province, "ProvinceID", "ProvinceName",customer.ProvinceID);
            ViewData["ProvinceID"] = new SelectList(_context.Province, "ProvinceID", "ProvinceName",customer.DistrictID);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerID,Name,Age,BirthDay,Sex,ProvinceID,DistrictID,Identify,Phone,Email,MetaTitle")] Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerID))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .SingleOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.CustomerID == id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustomerID == id);
        }
    }
}
