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
                    s.seller_buyer_reference.Contains(searchString) ||
                    s.seller_type_roles.Contains(searchString));
            }

            var orderedSellers = sellers.OrderByDescending(s => s.Id).ToList();
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
                seller_buyer_reference = sellerDto.seller_buyer_reference,
                fullname = sellerDto.fullname,
                reporting_to = sellerDto.reporting_to,
            };

            context.sellers.Add(seller);
            context.SaveChanges();

            return RedirectToAction("Index", "sellers");
        }

        public IActionResult Edit(int Id)
        {
            var sellers = context.sellers.Find(Id);

            if (sellers == null)
            {
                return RedirectToAction("Index", "sellers");
            }

            var sellerDto = new SellerDto()
            {
                seller_type_roles = sellers.seller_type_roles,
                seller_level = sellers.seller_level,
                seller_buyer_reference = sellers.seller_buyer_reference,
                fullname = sellers.fullname,
                reporting_to = sellers.reporting_to,
            };

            ViewData["Id"] = sellers.Id;

            return View(sellerDto);
        }

        [HttpPost]
        public IActionResult Edit(int Id, SellerDto sellerDto)
        {
            var sellers = context.sellers.Find(Id);

            if (sellers == null)
            {
                return RedirectToAction("Index", "sellers");
            }

            if (!ModelState.IsValid)
            {
                ViewData["Id"] = sellers.Id;
                return View(sellerDto);
            }

            sellers.seller_buyer_reference = sellerDto.seller_buyer_reference;
            sellers.seller_type_roles = sellerDto.seller_type_roles;
            sellers.seller_level = sellerDto.seller_level;
            sellers.fullname = sellerDto.fullname;
            sellers.reporting_to = sellerDto.reporting_to;

            context.SaveChanges();

            return RedirectToAction("Index", "sellers");
        }

        public IActionResult Delete(int Id)
        {
            var sellers = context.sellers.Find(Id);
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
