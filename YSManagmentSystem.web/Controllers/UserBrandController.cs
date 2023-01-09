using Microsoft.AspNetCore.Mvc;
using YSManagmentSystem.BLL.User;
using YSManagmentSystem.BLL.UserRole;
using YSManagmentSystem.Domain.User;
using YSManagmentSystem.web.Models.DataTable;
using System.Data;

namespace YSManagmentSystem.web.Controllers
{
    public class UserBrandController : Controller
    {
        private readonly IUserServices _user;
        private readonly IRoleServices _role;
        const string SessionId = "_Id";
        public UserBrandController(IUserServices user, IRoleServices role)
        {
            _user = user;
            _role = role;
        }
        public IActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles = "Admin,Ceo")]
        public IActionResult EditUserBrand(int uId)
        {
            var user = _user.GetUserByID(uId).Email;
            ViewBag.Email = user.ToString();

            HttpContext.Session.SetInt32(SessionId, uId);

            return View(_role.GetAllUserBrand(uId));
        }

        //[Authorize(Roles = "Admin,Ceo")]
        [HttpPost]
        public IActionResult EditUserBrand(List<BrandsEdit> BrandsEdit)
        {
            long result = 0;
            int Status;
            string Value;
            int UId = (int)HttpContext.Session.GetInt32(SessionId);
            var uBrandChk = BrandsEdit.Where(x => x.Checked == true);
            if (ModelState.IsValid)
            {
                foreach (var item in uBrandChk)
                {
                    if (item.Checked == true)
                    {
                        _role.RemoveBrand(UId, item.Id);
                        result =  _role.AddUserBrand(UId, item.Id);
                    }
                };
                var brandUchk = BrandsEdit.Where(x => x.Checked == false);
                foreach (var item in brandUchk)
                {
                    _role.RemoveBrand(UId, item.Id);

                };
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
                Status = 500;
                Value = "There is some error at client side";
            }
            return Json(new { status = Status, value = Value });
        }
        public JsonResult GetAllUserBrand()
        {
            var request = new DTReq();
            request.draw = Convert.ToInt32(Request.Form["draw"]);
            request.StartRowIndex = Convert.ToInt32(Request.Form["start"]);
            request.SortExpression = Request.Form["order[0][dir]"];
            request.PageSize = Convert.ToInt32(Request.Form["length"]);
            request.SearchText = Request.Form["search[value]"];



            var pro = _user.GetAllUserBrandDT(request).Result;
            return Json(pro);
        }
    }
}

