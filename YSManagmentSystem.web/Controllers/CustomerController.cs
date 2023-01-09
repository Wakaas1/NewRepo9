using Microsoft.AspNetCore.Mvc;
using YSManagmentSystem.BLL.OrderService;
using YSManagmentSystem.Domain.Order;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.web.Controllers
{
    public class CustomerController : Controller
    {
            private readonly ICustomerServices _customer;

            public CustomerController(ICustomerServices customer)
            {
            _customer = customer;

            }

            public IActionResult Index(DTReq req)
            {
                var cus = _customer.GetAllCustomerDT(req);
                return View(cus);
            }

        [HttpGet]
        public IActionResult AddOrEditCustomer(int? id)
        {
            if (id > 0)
            {
                var loc = _customer.GetCustomerByID(id.GetValueOrDefault());
                return View(loc);
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddOrEditCustomer(int? id, Customer cus)
        {
            long result = 0;
            int Status;
            string Value;

            if (id > 0)
            {
                result = _customer.UpdateCustomer(cus);
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
                
                result = _customer.AddCustomer(cus);
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
        public IActionResult DetailCustomer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cs = _customer.GetCustomerByID(id.GetValueOrDefault());
            if (cs == null)

                return NotFound();

            return View(cs);
        }


        [HttpPost]
        public IActionResult DeleteCustomer(int id)
        {
            if (_customer.DeleteCustomer(id) > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }


        //Data Table, Searching, sorting, Paging, Total Count,Filtering

        [HttpPost]
            public JsonResult GetAllCustomer()
            {
                var request = new DTReq();
                request.draw = Convert.ToInt32(Request.Form["draw"]);
                request.StartRowIndex = Convert.ToInt32(Request.Form["start"]);
                request.SortExpression = Request.Form["order[0][dir]"];
                request.PageSize = Convert.ToInt32(Request.Form["length"]);
                request.SearchText = Request.Form["search[value]"];



                var cus = _customer.GetAllCustomerDT(request).Result;
                return Json(cus);
            }


    }
}
