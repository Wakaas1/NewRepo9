using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using YSManagmentSystem.BLL.ItemService;
using YSManagmentSystem.Domain.Items;
using YSManagmentSystem.Domain.Product;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.web.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemServices _item;
        public ItemController(IItemServices item)
        {
            _item= item;
        }
        public IActionResult Index()
        {
            var itm = _item.GetAllItems();
            return View(itm);
        }

        [HttpGet]
        public IActionResult AddOrEditItem(int? id)
        {
            if (id > 0)
            {
                var pro = _item.GetItemByID(id.GetValueOrDefault());
                return View(pro);
            }

            ViewBag.PId = new SelectList(_item.GetAllProducts().ToList(), "Id", "ProductName");
            ViewBag.SId = new SelectList(_item.GetAllSupplier().ToList(), "Id", "SupplierName");
            ViewBag.BId = new SelectList(_item.GetAllBrand().ToList(), "Id", "BrandName");
            ViewBag.OId = new SelectList(_item.GetAllOrder().ToList(), "Id", "");

            return View();
        }

        [HttpPost]
        public IActionResult AddOrEditItem(int? id, Item itm)
        {
            long result = 0;
            int Status;
            string Value;
            if (id > 0)
            {
                result = _item.UpdateItem(itm);
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
                ViewBag.PId = new SelectList(_item.GetAllProducts().ToList(), "Id", "ProductName");
                ViewBag.SId = new SelectList(_item.GetAllSupplier().ToList(), "Id", "SupplierName");
                ViewBag.BId = new SelectList(_item.GetAllBrand().ToList(), "Id", "BrandName");
                ViewBag.OId = new SelectList(_item.GetAllOrder().ToList(), "Id", "");
                itm.createdAt = DateTime.Now;
                itm.updatedAt = DateTime.Now;

                result = _item.AddItem(itm);
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
        public IActionResult DetailItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var itm = _item.GetItemByID(id.GetValueOrDefault());
            if (itm == null)

                return NotFound();

            return View(itm);
        }

        [HttpPost]
        public IActionResult DeleteItem(int id, Item itm)
        {
            if (_item.DeleteItem(id) > 0)
            {
                return RedirectToAction("Index");
            }
            return View(itm);
        }

        [HttpPost]
        //Data Table, Searching, sorting, Paging, Total Count,Filtering
        public JsonResult GetAllItem()
        {
            var request = new DTReq();
            request.draw = Convert.ToInt32(Request.Form["draw"]);
            request.StartRowIndex = Convert.ToInt32(Request.Form["start"]);
            request.SortExpression = Request.Form["order[0][dir]"];
            request.PageSize = Convert.ToInt32(Request.Form["length"]);
            request.SearchText = Request.Form["search[value]"];
            request.productId = Convert.ToInt32(Request.Form["product"]);
            request.BrandId = Convert.ToInt32(Request.Form["brand"]);
            request.supplierId = Convert.ToInt32(Request.Form["supplier"]);
            request.orderId = Convert.ToInt32(Request.Form["order"]);


            var pro = _item.GetAllItemDT(request).Result;
            return Json(pro);
        }
    }
}
