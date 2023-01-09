using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using YSManagmentSystem.BLL.Products;
using YSManagmentSystem.BLL.SuppliersService;
using YSManagmentSystem.Domain.Product;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.web.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierServices _supplier;
        private readonly IProductServices _product;
        public SupplierController(ISupplierServices supplier, IProductServices product)
        {
            _supplier = supplier;
            _product = product;
        }

        public IActionResult Index()
        {
            var supp = _product.GetAllSupplier();
            return View(supp);
        }
        [HttpGet]
        public IActionResult AddOrEditSupplier(int? id)
        {
            if (id > 0)
            {
                var sup = _supplier.GetSupplierByID(id.GetValueOrDefault());
                return View(sup);
            }
            else
            {
                ViewBag.BId = new SelectList(_product.GetAllBrand().ToList(), "Id", "BrandName");

                return View();

            }
            
        }

        [HttpPost]
        public IActionResult AddOrEditSupplier(int? id, Supplier sup)
        {
            long result = 0;
            int Status;
            string Value;

            if (id > 0)
            {
                result = _supplier.UpdateSupplier(sup);
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

                result = _supplier.AddSupplier(sup);
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
        public IActionResult DetailSupplier(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sup = _supplier.GetSupplierByID(id.GetValueOrDefault());
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
        public IActionResult DeleteSupplier(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            _supplier.DeleteSupllier(id.GetValueOrDefault());
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteSupplier(int id, Product pro)
        {
            if (_supplier.DeleteSupllier(id) > 0)
            {
                return RedirectToAction("Index");
            }
            return View(pro);
        }

        //Data Table, Searching, sorting, Paging, Total Count,Filtering
        [HttpPost]
        public JsonResult GetAllSupplier()
        {
            var request = new DTReq();
            request.draw = Convert.ToInt32(Request.Form["draw"]);
            request.StartRowIndex = Convert.ToInt32(Request.Form["start"]);
            request.SortExpression = Request.Form["order[0][dir]"];
            request.PageSize = Convert.ToInt32(Request.Form["length"]);
            request.SearchText = Request.Form["search[value]"];
            request.BrandId = Convert.ToInt32(Request.Form["brand"]);


            var pro = _supplier.GetAllSupplierDT(request).Result;
            return Json(pro);
        }


    }
}
