using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Apartment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.Text;
using Microsoft.AspNetCore.Hosting;


namespace Apartment.Controllers
{

    [Authorize]
    public class CustomersController : Controller
    {
        private readonly ApartmentContext _context;

        public CustomersController(ApartmentContext context)
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
                province = new SelectList(_context.Province.OrderBy(s => s.ProvinceName), "ProvinceId", "ProvinceName", ProvinceId);
            }
            else
            {
                province = new SelectList(_context.Province.OrderBy(s => s.ProvinceName), "ProvinceId", "ProvinceName");
            }
            if (DistrictId != null && DistrictId != 0)
            {
                if (ProvinceId != null && ProvinceId != 0)
                {
                    district = new SelectList(_context.District.Where(x => x.ProvinceId == ProvinceId).OrderBy(s => s.DistrictName), "DistrictId", "DistrictName", DistrictId);
                }
                else
                {
                    district = new SelectList(_context.District.OrderBy(s => s.DistrictName), "DistrictId", "DistrictName", DistrictId);
                }
            }
            else
            {
                if (ProvinceId != null && ProvinceId != 0)
                {
                    district = new SelectList(_context.District.Where(x => x.ProvinceId == ProvinceId).OrderBy(s => s.DistrictName), "DistrictId", "DistrictName");
                }
                else
                {
                    district = new SelectList(_context.District.OrderBy(s => s.DistrictName), "DistrictId", "DistrictName");
                }
            }
            var CustomerContext = _context.Customer.ToAsyncEnumerable();
            if (ProvinceId != null && ProvinceId != 0)
            {
                CustomerContext = CustomerContext.Where(x => x.ProvinceId == ProvinceId);
            }
            if (DistrictId != null && DistrictId != 0)
            {
                CustomerContext = CustomerContext.Where(x => x.DistrictId == DistrictId);
            }
            var customer = CustomerContext.Select(j => new Customer()
            {
                CustomerId = j.CustomerId,
                CustomerName = j.CustomerName,
                BirthDay = j.BirthDay,
                IdentityCardNo = j.IdentityCardNo,
                PhoneNo = j.PhoneNo,
                Email = j.Email,
                Gender = j.Gender,
                Province = j.Province,
                District = j.District,
                Status = j.Status
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
                .Include(c => c.District)
                .Include(c => c.Province)
                .SingleOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create(int? ProvinceId)
        {
            ViewData["ProvinceId"] = new SelectList(_context.Province, "ProvinceId", "ProvinceName");
            if (ProvinceId != null)
            {
                ViewBag.Districts = new SelectList(_context.District.Where(x => x.ProvinceId == ProvinceId),"DistrictId","DistrictName");
            }
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CustomerName,BirthDay,IdentityCardNo,PhoneNo,Email,Gender,Status,DistrictId,ProvinceId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (customer.ProvinceId != null)
            {
                ViewBag.Districts = new SelectList(_context.District.Where(x => x.ProvinceId == customer.ProvinceId), "DistrictId", "DistrictName");
            }
            ViewData["ProvinceId"] = new SelectList(_context.Province, "ProvinceId", "ProvinceName", customer.ProvinceId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            if (id != null)
            {
                ViewBag.Districts = new SelectList(_context.District.Where(x => x.ProvinceId == id), "DistrictId", "DistrictName");
            }
            ViewData["ProvinceId"] = new SelectList(_context.Province, "ProvinceId", "ProvinceName", customer.ProvinceId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,CustomerName,BirthDay,IdentityCardNo,PhoneNo,Email,Gender,Status,DistrictId,ProvinceId")] Customer customer)
        {
            if (id != customer.CustomerId)
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
                    if (!CustomerExists(customer.CustomerId))
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
            
                ViewBag.Districts = new SelectList(_context.District.Where(x => x.ProvinceId == customer.ProvinceId), "DistrictName", "DistrictName",customer.DistrictId);
           
            ViewData["ProvinceId"] = new SelectList(_context.Province, "DistrictId", "DistrictName", customer.ProvinceId);
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
                .Include(c => c.District)
                .Include(c => c.Province)
                .SingleOrDefaultAsync(m => m.CustomerId == id);
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
            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.CustomerId == id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult GetDistrictByProId(int id)
        {
            
                var data = _context.District.Where(x => x.ProvinceId == id).OrderBy(x => x.DistrictName).Select(x=> new {Name= x.DistrictName, Id=x.DistrictId }).ToList();
                return Ok(data);
            
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustomerId == id);
        }
    }
}
