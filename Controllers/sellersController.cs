using Microsoft.AspNetCore.Mvc;
using sellerMVC.Services;
using SellerMVC.Models;
using System.Linq;

namespace SellerMVC.Controllers
{
    public class sellersController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;

        public sellersController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }

        public IActionResult Index(string searchString)
        {
            var sellers = from s in context.sellers
                          select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                sellers = sellers.Where(s =>
                    s.fullname.Contains(searchString) ||
                    s.sellerbp.Contains(searchString) ||
                    s.seller_type_roles.Contains(searchString));
            }

            var orderedSellers = sellers.OrderByDescending(s => s.sellerbp).ToList();
            return View(orderedSellers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SellerDto sellerDto)
        {
            if (!ModelState.IsValid)
            {
                return View(sellerDto);
            }

            sellers seller = new sellers()
            {
                seller_type_roles = sellerDto.seller_type_roles,
                seller_level = sellerDto.seller_level,
                sellerbp = sellerDto.sellerbp,
                fullname = sellerDto.fullname,
                reporting_to = sellerDto.reporting_to,
            };

            context.sellers.Add(seller);
            context.SaveChanges();

            return RedirectToAction("Index", "sellers");
        }

        public IActionResult Edit(string sellerbp)
        {
            var sellers = context.sellers.Find(sellerbp);

            if (sellers == null)
            {
                return RedirectToAction("Index", "sellers");
            }

            var sellerDto = new SellerDto()
            {
                seller_type_roles = sellers.seller_type_roles,
                seller_level = sellers.seller_level,
                sellerbp = sellers.sellerbp,
                fullname = sellers.fullname,
                reporting_to = sellers.reporting_to,
            };

            ViewData["sellerbp"] = sellers.sellerbp;

            return View(sellerDto);
        }

        [HttpPost]
        public IActionResult Edit(string sellerbp, SellerDto sellerDto)
        {
            var sellers = context.sellers.Find(sellerbp);

            if (sellers == null)
            {
                return RedirectToAction("Index", "sellers");
            }

            if (!ModelState.IsValid)
            {
                ViewData["sellerbp"] = sellers.sellerbp;
                return View(sellerDto);
            }

            sellers.sellerbp = sellerDto.sellerbp;
            sellers.seller_type_roles = sellerDto.seller_type_roles;
            sellers.seller_level = sellerDto.seller_level;
            sellers.fullname = sellerDto.fullname;
            sellers.reporting_to = sellerDto.reporting_to;

            context.SaveChanges();

            return RedirectToAction("Index", "sellers");
        }

        public IActionResult Delete(string sellerbp)
        {
            var sellers = context.sellers.Find(sellerbp);
            if (sellers == null)
            {
                return RedirectToAction("Index", "sellers");
            }

            context.sellers.Remove(sellers);
            context.SaveChanges(true);
            return RedirectToAction("Index", "sellers");
        }
    }
}
