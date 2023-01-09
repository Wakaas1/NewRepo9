using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using YSManagmentSystem.BLL.Categories;
using YSManagmentSystem.BLL.Products;
using YSManagmentSystem.Domain.Product;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.web.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationServices _location;
        private readonly IProductServices _product;
        public LocationController(ILocationServices location, IProductServices product)
        {
            _location = location;
            _product = product;
        }

        public IActionResult Index()
        {
            var loc = _product.GetAllLocation();
            return View(loc);
        }

        [HttpGet]
        public IActionResult AddOrEditLocation(int? id)
        {
            if (id > 0)
            {
                var loc = _location.GetLocationByID(id.GetValueOrDefault());
                return View(loc);
            }
            else
            {
                ViewBag.BId = new SelectList(_product.GetAllLocation().ToList(), "Id", "Location");

                return View();

            }

        }

        [HttpPost]

        public IActionResult AddOrEditLocation(int? id, Locations loc)
        {
            long result = 0;
            int Status;
            string Value;

            if (id > 0)
            {
                result = _location.UpdateLocation(loc);
                if (result > 0)
                {
                    Status = 200;
                    Value = Url.Content("~/Design/View/");
                }
                else
                {
                    Status = 500;
                    Value = "There is some error at server side";
                }
            }
            else
            {
                ViewBag.BId = new SelectList(_product.GetAllLocation().ToList(), "Id", "Location");

                result = _location.AddLocation(loc);
                if (result > 0)
                {
                    Status = 200;
                    Value = Url.Content("~/Design/View/");
                }
                else
                {
                    Status = 500;
                    Value = "There is some error at server side";
                }
            }
            return Json(new { status = Status, value = Value });
        }


        [HttpGet]
        public IActionResult DetailLocation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var loc = _location.GetLocationByID(id.GetValueOrDefault());
            if (loc == null)

                return NotFound();

            return View(loc);
        }

    
        [HttpPost]
        public IActionResult DeleteLocation(int id)
        {
            if (_location.DeleteLocation(id) > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        //Data Table, Searching, sorting, Paging, Total Count,Filtering
        [HttpPost]
        public JsonResult GetAllLocation()
        {
            var request = new DTReq();
            request.draw = Convert.ToInt32(Request.Form["draw"]);
            request.StartRowIndex = Convert.ToInt32(Request.Form["start"]);
            request.SortExpression = Request.Form["order[0][dir]"];
            request.PageSize = Convert.ToInt32(Request.Form["length"]);
            request.SearchText = Request.Form["search[value]"];



            var loc = _location.GetAllLocationDT(request).Result;
            return Json(loc);
        }
    }
}
