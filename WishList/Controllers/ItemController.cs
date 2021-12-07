using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WishList.Data;
using WishList.Models;

namespace WishList.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var items = _context.Items;
            return View("Index", items.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(Item item)
        {
            //var nwItem = new Item
            //{
            //    Id = item.Id,
            //    Description = item.Description
            //};

            _context.Items.Add(item);
            _context.SaveChanges();

            return RedirectToAction("Index", _context.Items);
        }

        public IActionResult Delete(int id)
        {
            var selectedItem = _context.Items.FirstOrDefault(i => i.Id == id);
            if (selectedItem != null)
            {
                _context.Items.Remove(selectedItem);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
