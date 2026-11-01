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
    new Device { Id = 5, AssetTag = "ECC-0023", Name = "Lenovo ThinkPad E14", Category = "Laptop", PurchaseYear = 2017, IsOnLoan = "no" },
};
        [HttpGet("devices")]
        public IActionResult show(string devices)
        {
            ViewData["Title"] = "All Devices";
            ViewBag.DeviceCount = alldevices.Count;
            ViewBag.Devices = devices;
            return View(alldevices);
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
    }
}
