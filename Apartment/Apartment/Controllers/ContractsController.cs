using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Apartment.Models;
using Microsoft.AspNetCore.Authorization;
using Apartment.Models.ContractViewModels;

namespace Apartment.Controllers
{

    [Authorize]
    public class ContractsController : Controller
    {
        private readonly ApartmentContext _context;

        public ContractsController(ApartmentContext context)
        {
            _context = context;
        }

        // GET: Contracts
        public  IActionResult Index()
        {
            var apartmentContext = _context.Contract.Include(c => c.Customer).Include(c => c.Room);
            return View( apartmentContext.ToList());
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
                .SingleOrDefaultAsync(m => m.ContractId == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // GET: Contracts/Create
        [HttpGet]
        public IActionResult Create(int? provinceId, int? districtId)
        {

                ViewBag.Provinces = new SelectList(_context.Province, "ProvinceId", "ProvinceName");
           
            if (provinceId != null)
            {
                ViewBag.Districts = new SelectList(_context.District.Where(x => x.ProvinceId == provinceId), "DistrictId", "DistrictName");
            }

            
                ViewBag.RoomTypes = new SelectList(_context.RoomType, "RoomTypeId", "RoomTypeName");
           
            
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ContractViewModel contractView)
        {
            if (ModelState.IsValid)
            {
                var contract = new Contract();
                contract.ContractNo = contractView.ContractNo;
                contract.EngageMonth = contractView.EngageMonth;
                contract.ContractValue = contractView.ContractValue;
                var customer = new Customer();
                customer.CustomerName = contractView.CustomerName;
                customer.BirthDay = contractView.BirthDay;
                customer.Gender = contractView.Gender;
                customer.IdentityCardNo = contractView.IdentityCardNo;
                customer.DistrictId = contractView.DistrictId;
                customer.ProvinceId = contractView.ProvinceId;
                customer.PhoneNo = contractView.PhoneNo;
                customer.Email = contractView.Email;
                var room = new Room();
                room.RoomNo = contractView.RoomNo;
                room.RoomTypeId = contractView.RoomTypeId;
                room.Price = contractView.Price;
                room.Area = contractView.Area;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Provinces = new SelectList(_context.Province, "ProvinceId", "ProvinceName", contractView.ProvinceId);
            if (contractView.ProvinceId != null)
            {
                ViewBag.Districts = new SelectList(_context.District.Where(x => x.ProvinceId == contractView.ProvinceId), "DistrictId", "DistrictName",contractView.DistrictId);
            }

           
                ViewBag.RoomTypes = new SelectList(_context.RoomType, "RoomTypeId", "RoomTypeName",contractView.RoomTypeId);
            
            return View(contractView);
        }

        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var contract = await _context.Contract.SingleOrDefaultAsync(m => m.ContractId == id);
        if (contract == null)
        {
            return NotFound();
        }


        ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerName", contract.CustomerId);
        ViewData["RoomId"] = new SelectList(_context.Room, "RoomId", "RoomNo", contract.RoomId);


        return View(contract);
    }

    // POST: Contracts/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ContractId,ContractNo,RoomId,CustomerId,EngageMonth,BeginDate,ContractValue")] Contract contract)
    {
        if (id != contract.ContractId)
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
                if (!ContractExists(contract.ContractId))
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
        ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerName", contract.CustomerId);
        ViewData["RoomId"] = new SelectList(_context.Room, "RoomId", "RoomNo", contract.RoomId);
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
            .SingleOrDefaultAsync(m => m.ContractId == id);
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
        var contract = await _context.Contract.SingleOrDefaultAsync(m => m.ContractId == id);
        _context.Contract.Remove(contract);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }



    private bool ContractExists(int id)
    {
        return _context.Contract.Any(e => e.ContractId == id);
    }
}
}
