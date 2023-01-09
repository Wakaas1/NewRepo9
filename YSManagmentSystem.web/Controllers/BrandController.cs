
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using YSManagmentSystem.BLL.Categories;
using YSManagmentSystem.BLL.Products;
using YSManagmentSystem.Domain.Product;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.web.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandServices _brand;
        private readonly IProductServices _product;
        public BrandController(IBrandServices brand,IProductServices product)
        {
            _brand = brand;
            _product = product;
        }

        public IActionResult Index()
        {
            var bnd = _product.GetAllBrand();

            return View(bnd);
        }

        // Add or Edit Brand
        [HttpGet]
        public IActionResult AddOrEditBrand(int? id)
        {
            if (id > 0)
            {
                var bnd = _brand.GetBrandByID(id.GetValueOrDefault());
                return View(bnd);
            }
            else
            {
                ViewBag.BId = new SelectList(_product.GetAllBrand().ToList(), "Id", "BrandName");
                return View();
            }
        }

        [HttpPost]
        public IActionResult AddOrEditBrand(int? id, Brand bnd)
        {
            long result = 0;
            int Status;
            string Value;

            if (id > 0)
            {
                result = _brand.UpdateBrand(bnd);
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
                ViewBag.BId = new SelectList(_product.GetAllBrand().ToList(), "Id", "BrandName");

                result = _brand.AddBrand(bnd);
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

        // Detail Brand
        [HttpGet]
        public IActionResult DetailBrand(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sup = _brand.GetBrandByID(id.GetValueOrDefault());
            if (sup == null)

                return NotFound();

            return View(sup);
        }

        
        //Delete Brand

        [HttpPost]
        public IActionResult DeleteBrand(int id)
        {
            _brand.DeleteBrand(id);
            
                return RedirectToAction("Index");
            
            
        }

        //Data Table, Searching, sorting, Paging, Total Count,Filtering
        [HttpPost]
        public JsonResult GetAllBrand()
        {
            var request = new DTReq();
            request.draw = Convert.ToInt32(Request.Form["draw"]);
            request.StartRowIndex = Convert.ToInt32(Request.Form["start"]);
            request.SortExpression = Request.Form["order[0][dir]"];
            request.PageSize = Convert.ToInt32(Request.Form["length"]);
            request.SearchText = Request.Form["search[value]"];

            var pro = _brand.GetAllBrandDT(request).Result;
            return Json(pro);
        }
    }
}
