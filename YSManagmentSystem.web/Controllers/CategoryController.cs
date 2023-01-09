using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using YSManagmentSystem.BLL.Categories;
using YSManagmentSystem.BLL.Products;
using YSManagmentSystem.Domain.Product;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryServices _category;
        private readonly IProductServices _product;
        public CategoryController(ICategoryServices category, IProductServices product)
        {
            _category = category;
            _product = product;
        }
        public IActionResult Index()
        {
            var cat = _product.GetAllCategory();
            return View(cat);
        }
        [HttpGet]
        public IActionResult AddOrEditCategory(int? id)
        {
            if (id > 0)
            {
                var cat = _category.GetCategoryByID(id.GetValueOrDefault());
                return View(cat);
            }
            else
            {
                ViewBag.CId = new SelectList(_product.GetAllCategory().ToList(), "Id", "CategoryName");

                return View();

            }

        }

        [HttpPost]
        
        public IActionResult AddOrEditCategory(int? id, Category cat)
        {
            long result = 0;
            int Status;
            string Value;

            if (id > 0)
            {
                result = _category.UpdateCategory(cat);
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
                ViewBag.BId = new SelectList(_product.GetAllCategory().ToList(), "Id", "CategoryName");

                result = _category.AddCategory(cat);
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
        public IActionResult DetailCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sup = _category.GetCategoryByID(id.GetValueOrDefault());
            if (sup == null)

                return NotFound();

            return View(sup);
        }

        //[HttpGet]
        //public IActionResult EditProduct(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var prod = _product.GetProductByID(id.GetValueOrDefault());
        //    if (prod == null)

        //        return NotFound();

        //    return View(prod);
        //}

        //[HttpPost]
        //public IActionResult EditProduct(int id,Product product) 
        //{
        //    long result = 0;
        //    int Status;
        //    string Value;

        //    if (ModelState.IsValid)
        //    {
        //        result = _product.UpdateProduct(product);
        //        if (result > 0)
        //        {
        //            Status = 200;
        //            Value = Url.Content("~/Design/View/");
        //        }
        //        else
        //        {
        //            Status = 500;
        //            Value = "There is some error at server side";
        //        }
        //    }
        //    else
        //    {
        //        Status = 500;
        //        Value = "There is some error at client side";
        //    }
        //    return Json(new { status = Status, value = Value });
        //}

        //Delete user

        [HttpGet]
        public IActionResult DeleteCategory(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            _category.DeleteCategory(id.GetValueOrDefault());
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteCategory(int id, Category cat)
        {
            if (_category.DeleteCategory(id) > 0)
            {
                return RedirectToAction("Index");
            }
            return View(cat);
        }

        //Data Table, Searching, sorting, Paging, Total Count,Filtering
        [HttpPost]
        public JsonResult GetAllCategory()
        {
            var request = new DTReq();
            request.draw = Convert.ToInt32(Request.Form["draw"]);
            request.StartRowIndex = Convert.ToInt32(Request.Form["start"]);
            request.SortExpression = Request.Form["order[0][dir]"];
            request.PageSize = Convert.ToInt32(Request.Form["length"]);
            request.SearchText = Request.Form["search[value]"];
            


            var pro = _category.GetAllCategoryDT(request).Result;
            return Json(pro);
        }
    }
}
