using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc;
using YSManagmentSystem.BLL.OrderService;
using YSManagmentSystem.BLL.Products;
using YSManagmentSystem.Domain.Order;
using YSManagmentSystem.web.Models.DataTable;


namespace YSManagmentSystem.web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderServices _order;
        private readonly ICustomerServices _customer;
        private readonly IProductServices _product;
        private readonly IDriverServices _driver;
        private readonly ISalePersonsServices _saleperson;

        public OrderController(IOrderServices order,ICustomerServices customer,IProductServices product,IDriverServices driver,ISalePersonsServices saleperson)
        {
            _order = order;
            _customer = customer;
            _product = product;
            _driver = driver;
            _saleperson = saleperson;
        }

        public IActionResult Index(DTReq req)
        {
            var loc = _order.GetAllOrderDT(req);
            return View(loc);
        }
        [HttpGet]
        public IActionResult CreateOrder(int id)
        {
            ViewBag.DI = new SelectList(_driver.GetAllDrivers().ToList(), "Id", "Name");
            ViewBag.CId = new SelectList(_customer.GetAllCustomer().ToList(), "Id", "CustomerName");
            ViewBag.VD = new SelectList(_driver.GetAllVehicles().ToList(), "Id", "RegistrationNumber");
            ViewBag.SP = new SelectList(_saleperson.GetAllSalePersons().ToList(), "Id", "SalePersonName");
            ViewBag.Area = new SelectList(_saleperson.GetAllAreas().ToList(), "Id", "AreaName");
            if (id == 0)
            {
                //int i = 0000;
                //string num = i++.ToString();

                Random generator = new Random();
                string number = generator.Next(1, 100000).ToString("D5");
                //var no = _order.GetOrderByID(i);

                var order = new tbl_Order();
                    order.OrderDate = DateTime.Now;
                    order.OrderNumber = number;
                    order.CustomerId = 0;
                    order.Status = false;

                    int r = _order.CreateNewOrder(id,number);
                    var list = _order.GetOrderByItem(r);
                    ViewBag.id = r;
                    TempData["Oid"] = r;
                
                    return View(list);

                
                }
            else
                {
                    
                    TempData["OId"] = id;
                    var list = _order.GetOrderByItem(id);
                    var tot = list.Sum(x => x.Total);
                    ViewBag.tot = tot;
                    return View(list);
                }
            }
        

        [HttpPost]
        public IActionResult AddCustomer(int CId)
        {
            int li = (int)TempData["OId"];
            ViewBag.CId = new SelectList(_customer.GetAllCustomer().ToList(), "Id", "CustomerName");
            _order.AddCustomer(li,CId);
            int id = li;
            return Redirect(Url.Action("CreateOrder", "Order", new { id }, "https"));
        }

        [HttpGet]
        public IActionResult AddOrderItem(int id)
        {
            ViewBag.PId = new SelectList(_product.GetAllProducts().ToList(), "Id", "ProductName");
            ViewBag.PC = new SelectList(_product.GetAllProducts().ToList(), "Id", "ProductCode");

            return View();
        }
        [HttpPost]
        public IActionResult AddOrderItem(int PId , int D , int Q)
        {
            int li = (int)TempData["Oid"];
            ViewBag.PId = new SelectList(_product.GetAllProducts().ToList(), "Id", "ProductName");
            ViewBag.PC = new SelectList(_product.GetAllProducts().ToList(), "Id", "ProductCode");
            //ViewBag.OId = new SelectList(_order.GetAllOrders().ToList(), "Id");
            var product = _product.GetProductByID(PId);
            var ordi = new OrderItem();
            ordi.OrderId = li;
            ordi.ProductId = PId;
            ordi.Quantity = Q;
            ordi.Cost = product.Price;
            ordi.Discount = (ordi.Cost * D)/100;
            ordi.Total = ordi.Quantity * (ordi.Cost - ordi.Discount);
            

            var i = _order.AddOrderItem(ordi);
            var id = _order.GetOrderItemByID(i).OrderId;

            TempData["OID"] = li;

            return Redirect(Url.Action("CreateOrder", "Order", new { id }, "https")); 
        }
        [HttpGet]
        public IActionResult OrderItem(int id,int  CId, int DId)
        {         
            var ord = _order.GetOrderByID(id);
            var cus = _customer.GetCustomerByID(ord.CustomerId);
            ViewBag.CName = cus.CustomerName;
            ViewBag.Con = cus.ContactNumber;
            ViewBag.Address = cus.CustomerAddress;
            ViewBag.Date = ord.OrderDate;
            ViewBag.ONum = ord.OrderNumber;
            var dri = _driver.GetDriverByID(ord.DriverId);
            var veh = _driver.GetVehicleByID(dri.VehicleId);
            var area = _saleperson.GetAreaByID(ord.AreaId);
            var sale = _saleperson.GetSalePersonByID(ord.SalePersonId);
            ViewBag.SName = sale.SalePersonName;
            ViewBag.AName = area.AreaName;
            ViewBag.DName = dri.Name;
            ViewBag.VReg = veh.RegistrationNumber;
            //var d = _order.GetOrderItemByID(id).OrderId;
            //var di = _order.GetOrderItemByID(d);
            //ViewBag.dis = di.Discount;
            var list = _order.GetOrderByItem(id);
            var tot = list.Sum(x => x.Total);
            _order.AddTotal(id, tot);
            
            ViewBag.tot = tot;
            return View(list);
        }

        [HttpPost]
        public IActionResult Checkout( int CId, int DId, int SId, int AId)
        {
            int li = (int)TempData["OID"];

            int id = li;
            var list = _order.GetOrderByItem(id);
            foreach(var item in list)
            {
                
                int OIId = item.Id;
                var items = _order.GetOrderItemByID(OIId);
                var pro = _product.GetProductByID(items.ProductId);
                var qu = pro.Quantity - items.Quantity;
                _product.UpdateProductQuantity(items.ProductId, qu);
            }
            _order.Checkout(id, CId, DId, SId, AId);
            return Redirect(Url.Action("OrderItem", "Order", new { id }, "https")); 
        }


        [HttpPost]
        public JsonResult GetAllOrder()
        {
            var request = new DTReq();
            request.draw = Convert.ToInt32(Request.Form["draw"]);
            request.StartRowIndex = Convert.ToInt32(Request.Form["start"]);
            request.SortExpression = Request.Form["order[0][dir]"];
            request.PageSize = Convert.ToInt32(Request.Form["length"]);
            request.SearchText = Request.Form["search[value]"];

            var loc = _order.GetAllOrderDT(request).Result;
            return Json(loc);
        }

        //Add Quantity
        public IActionResult Add(int id)
        {
            var item = _order.GetOrderItemByID(id);
            var pro = _product.GetProductByID(item.ProductId);
          
            var newqu = item.Quantity + 1;
      
            int Id = item.OrderId ;
            var Total = newqu *  pro.Price;
            _order.UpdateQuantity(id,newqu,Total);
            return Redirect(Url.Action("CreateOrder", "Order", new { Id }, "https")); 
        }

        //Decrease Quantity
        public IActionResult Dec(int id)
        {
            var item = _order.GetOrderItemByID(id);
            var pro = _product.GetProductByID(item.ProductId);
            int Id = item.OrderId;
            var q = item.Quantity;
            if (q == 0)
            {
                _order.DeleteOrder(id);
            }
          
            var newqu = item.Quantity - 1;

            var Total = newqu * pro.Price;
            _order.UpdateQuantity(id, newqu, Total);
            return Redirect(Url.Action("CreateOrder", "Order", new { Id }, "https")); 

        }
        //Remove Item When Quantity = 0 
        public IActionResult RemoveItem(int id)
        {
            var item = _order.GetOrderItemByID(id);
            var pro = _product.GetProductByID(item.ProductId);
            var newqu = item.Quantity + pro.Quantity;
            _product.UpdateProductQuantity(item.ProductId, newqu);
            int Id = item.OrderId;
            _order.DeleteOrder(id);

            return Redirect(Url.Action("CreateOrder", "Order", new { Id }, "https"));
        }

            //Clear Cart 
        public IActionResult Clear(int id)
        {
            _order.DeleteOrderItemList(id);
            return Redirect(Url.Action("CreateOrder", "Order", new { id }, "https"));
        }
    }
}
