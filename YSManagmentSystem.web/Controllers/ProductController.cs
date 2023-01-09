using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using YSManagmentSystem.BLL.Categories;
using YSManagmentSystem.BLL.Products;
using YSManagmentSystem.Domain.Product;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductServices _product;
        private readonly ICategoryServices _category;
        public ProductController(IProductServices product,ICategoryServices category)
        {
            _product= product;
            _category= category;
        }
        public IActionResult Index()
        {
           var prod = _product.GetAllProducts();
            return View(prod);
        }

        [HttpGet]
        public IActionResult CreateOrEditProduct(int? id)
        {
            if (id > 0)
            {
                var pro = _product.GetProductByID(id.GetValueOrDefault());
                ViewBag.CId1 = new SelectList(_product.GetAllCategory().ToList(), "Id", "CategoryName", pro.CategoryId);
                ViewBag.LId1 = new SelectList(_product.GetAllLocation().ToList(), "Id", "Location", pro.LocationId);
                ViewBag.BId1 = new SelectList(_product.GetAllBrand().ToList(), "Id", "BrandName", pro.BrandId);
                ViewBag.SId1 = new SelectList(_product.GetAllSupplier().ToList(), "Id", "SupplierName", pro.SupplierId);
                return View(pro);
            }

            ViewBag.CId = new SelectList(_product.GetAllCategory().ToList(), "Id", "CategoryName" );
            ViewBag.LId = new SelectList(_product.GetAllLocation().ToList(), "Id", "Location");
            ViewBag.BId = new SelectList(_product.GetAllBrand().ToList(), "Id", "BrandName");
            ViewBag.SId = new SelectList(_product.GetAllSupplier().ToList(), "Id", "SupplierName");
           
            return View();
        }

        [HttpPost]
        public IActionResult CreateOrEditProduct(int? id,Product pro)
        {
            long result = 0;
            int Status;
            string Value;
            if (id > 0)
            {
                ViewBag.CId1 = new SelectList(_product.GetAllCategory().ToList(), "Id", "CategoryName", pro.CategoryId);
                ViewBag.LId1 = new SelectList(_product.GetAllLocation().ToList(), "Id", "Location", pro.LocationId);
                ViewBag.BId1 = new SelectList(_product.GetAllBrand().ToList(), "Id", "BrandName", pro.BrandId);
                ViewBag.SId1 = new SelectList(_product.GetAllSupplier().ToList(), "Id", "SupplierName", pro.SupplierId);
                pro.CreatedAt = DateTime.Now;
                pro.UpdatedAt = DateTime.Now;
                result = _product.UpdateProduct(pro);
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
                ViewBag.CId = new SelectList(_product.GetAllCategory().ToList(), "Id", "CategoryName");
                ViewBag.LId = new SelectList(_product.GetAllLocation().ToList(), "Id", "Location");
                ViewBag.BId = new SelectList(_product.GetAllBrand().ToList(), "Id", "BrandName");
                ViewBag.SId = new SelectList(_product.GetAllSupplier().ToList(), "Id", "SupplierName");
                pro.CreatedAt = DateTime.Now;
                pro.UpdatedAt = DateTime.Now;

                result =_product.AddProduct(pro);
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
        public IActionResult DetailProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var prod = _product.GetProductByID(id.GetValueOrDefault());
            if (prod == null)

                return NotFound();

            return View(prod);
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
        public IActionResult DeleteProduct(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            _product.DeleteProduct(id.GetValueOrDefault());
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteProduct(int id, Product pro)
        {
            if (_product.DeleteProduct(id) > 0)
            {
                return RedirectToAction("Index");
            }
            return View(pro);
        }

        //[HttpPost]
        //public IActionResult GetAllProduct()
        //{
        //    int totalRecord = 0;
        //    int filterRecord = 0;

        //    var draw = Request.Form["draw"].FirstOrDefault();

        //    // Sort Column Name
        //    var sortCoulmn = Request.Form["sortColumn[" + Request.Form["order[0] [column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

        //    var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

        //    //Search value from (Search box)
        //    var searchValue = Request.Form["search[value]"].FirstOrDefault();

        //    //Paging Size (10,20,50,100)
        //    int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");

        //    // Skip Number of Count
        //    int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");

        //    //getting all Employee data 
        //    var data = _product.GetAllProducts();

        //    //get total count of data in table
        //    totalRecord = data.Count();

        //    // search data when search value found
        //    if (!string.IsNullOrEmpty(searchValue))
        //    {
        //        data = data.Where(a => a.ProductName.ToLower().Contains(searchValue.ToLower()) || a.Description.ToLower().Contains(searchValue.ToLower()) || a.Quantity.ToLower().Contains(searchValue.ToLower()));
        //    }

        //    //get total count of records after search 
        //    filterRecord = data.Count();

        //    //sort data
        //    //if (!string.IsNullOrEmpty(sortCoulmn) && !string.IsNullOrEmpty(sortColumnDirection)) data = data.OrderBy(sortCoulmn + " " + sortColumnDirection);

        //    //pagination
        //    var proList = data.Skip(skip).Take(pageSize).ToList();
        //    return Json(new
        //    {
        //        draw = draw,
        //        recordsTotal = totalRecord,
        //        recordsFiltered = filterRecord,
        //        data = proList
        //    });
        //}

        [HttpPost]
        //Data Table, Searching, sorting, Paging, Total Count,Filtering
        public JsonResult GetAllProduct()
        {
            var request = new DTReq();
            request.draw = Convert.ToInt32(Request.Form["draw"]);
            request.StartRowIndex= Convert.ToInt32(Request.Form["start"]);
            request.SortExpression = Request.Form["order[0][dir]"];
            request.PageSize = Convert.ToInt32(Request.Form["length"]);
            request.SearchText = Request.Form["search[value]"];
            request.CategoryId = Convert.ToInt32(Request.Form["category"]);
            request.LocationId = Convert.ToInt32(Request.Form["location"]);


            var pro = _product.GetAllProductDT(request).Result;
            return Json(pro);
        }

       
    }
}

