using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechDesk.Data;
using TechDesk.Models;

namespace TechDesk.Controllers
{
    public class DevicesController : Controller
    {
        private readonly AppDbContext _context;

        public DevicesController(AppDbContext context)
        {
            _context = context;
        }
        private List<Device> alldevices = new List<Device>
{
    new Device { Id = 1, AssetTag = "ECC-0042", Name = "Dell Latitude 5440", Category = "Laptop", PurchaseYear = 2023, IsOnLoan = "yes" },
    new Device { Id = 2, AssetTag = "ECC-0007", Name = "HP Probook 450", Category = "Laptop", PurchaseYear = 2019, IsOnLoan = "no" },
    new Device { Id = 3, AssetTag = "ECC-0115", Name = "Ipad 9th Generation", Category = "Tablet", PurchaseYear = 2022, IsOnLoan = "yes" },
    new Device { Id = 4, AssetTag = "ECC-0090", Name = "Samsung Galaxy Tab", Category = "Tablet", PurchaseYear = 2021, IsOnLoan = "no" },
    new Device { Id = 5, AssetTag = "ECC-0023", Name = "Lenovo ThinkPad E14", Category = "Laptop", PurchaseYear = 2017, IsOnLoan = "no"  },
};
        [HttpGet("devices")]
        public IActionResult show(string name, bool availableOnly )
        {
            var devices = _context.Devices.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                devices = devices.Where(d => d.Name.Contains(name));

            if (availableOnly)
                devices = devices.Where(d => d.IsOnLoan == "no");

            devices = devices.OrderBy(d => d.AssetTag);

            ViewBag.DeviceCount = devices.Count();
            ViewBag.Name = name;
            ViewBag.AvailableOnly = availableOnly;

            return View(devices.ToList());
        }

        [HttpGet("/devices/{id:int}")]
        public IActionResult details(int id)
        {
            var device = _context.Devices.Find(id);

            if (device == null)
            {
                return NotFound();
            }
            return View(device);
        }

        [HttpGet("/devices/add")]
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Create(Device device)
        {
            if (ModelState.IsValid)
            {
                _context.Devices.Add(device);
                _context.SaveChanges();

                TempData["Message"] = device.AssetTag + " "
                                    + device.Name + " was added";

                return RedirectToAction("Show");
            }

            return View(device);
        }
        [HttpGet("/devices/edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var device = _context.Devices.Find(id);

            if (device == null)
                return NotFound();

            return View(device);
        }
        [HttpPost("/devices/edit/{id:int}")]
        public IActionResult Edit(int id, Device device)
        {
            var existingDevice = _context.Devices.Find(id);

            if (existingDevice == null)
                return NotFound();

          
            if (_context.Devices.Any(d =>
                d.AssetTag == device.AssetTag && d.Id != id))
            {
                ModelState.AddModelError(
                    "AssetTag",
                    "Another device already has that asset tag");
            }

            if (ModelState.IsValid)
            {
                existingDevice.AssetTag = device.AssetTag;
                existingDevice.Name = device.Name;
                existingDevice.Category = device.Category;
                existingDevice.PurchaseYear = device.PurchaseYear;
                existingDevice.IsOnLoan = device.IsOnLoan;

                _context.SaveChanges();

                TempData["Message"] = existingDevice.AssetTag + " "
                                    + existingDevice.Name + " was updated";

                return RedirectToAction("Show");
            }

            return View(device);
        }
       
        [HttpGet("/devices/delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var device = _context.Devices.Find(id);

            if (device == null)
                return NotFound();

            return View(device);
        }

        
        [HttpPost("/devices/delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var device = _context.Devices.Find(id);

            if (device == null)
            {
                TempData["Message"] = "That device was already deleted";
                return RedirectToAction("Show");
            }

            _context.Devices.Remove(device);
            _context.SaveChanges();

            TempData["Message"] = device.AssetTag + " "
                                + device.Name + " was deleted";

            return RedirectToAction("Show");
        }
    }
}
