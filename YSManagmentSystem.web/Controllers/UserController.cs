using Microsoft.AspNetCore.Mvc;
using YSManagmentSystem.BLL.User;
using YSManagmentSystem.Domain.Product;
using YSManagmentSystem.Domain.User;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.web.Controllers
{
    public class UserController : Controller
    {
        
        private readonly IUserServices _user;
        //private readonly IRoleServices _role;
        private readonly IWebHostEnvironment _webHostEnvironment;


        private class ICustomICustomClaimsCookieSignInHelper { }
        public UserController(IUserServices user, IWebHostEnvironment webHostEnvironment /*IRoleServices role*/)
        {
            _user = user;
            _webHostEnvironment = webHostEnvironment;
            //_role = role;
        }

        //[Authorize(Roles = "Admin,User,Editor")]
        public IActionResult Index(UserDetail uD)
        {
            var user = _user.GetAllUsers(uD);
            return View(user);
        }
        [HttpGet]
        public IActionResult GetUserByID(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var user = _user.GetUserByID(id.GetValueOrDefault());
            if (user == null)

                return NotFound();

            return View(user);
        }
        public IActionResult Profile()
        {
            var email = HttpContext.Response.HttpContext.User.Identity.Name;
            var user = _user.GetUserByEmail(email);

            return View();
        }

        // Change Password after login 
        //[HttpGet]
        //[Authorize]
        //public IActionResult ChangePassword()
        //{
        //    return View();
        //}
        //[HttpPost]

        //public IActionResult ChangePassword(ChangePassword dto)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        var email = HttpContext.Response.HttpContext.User.Identity.Name;
        //        var user = _user.GetUserByEmail(email);
        //        if (!_user.VerifyPasswordHash(user.Password, dto.OldPassword))
        //        {
        //            return BadRequest("Your password does not macth.");
        //        }
        //        if (dto.NewPassword == dto.ConfirmPassword)
        //        {
        //            var hash = _user.CreatePasswordHash(dto.NewPassword);
        //            _user.UpdatePassword(email, hash);
        //        }
        //        else
        //        {
        //            return BadRequest("Password does not match.");
        //        }
        //        return RedirectToAction("Login");
        //    }
        //    else
        //    {
        //        return BadRequest("OOP! Wrong Credentials");
        //    }
        //}


        ////Image Upload

        //[HttpGet]
        //public IActionResult ImageUpload()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult ImageUpload(ImageModel dto)
        //{
        //    string email = HttpContext.Response.HttpContext.User.FindClaim("id").ToString();
        //    string[] ne = email.Split(':');
        //    int e = Convert.ToInt32(ne[1]);
        //    string userimg = _user.GetUserByID(e).Image;
        //    DateTime date = DateTime.UtcNow;

        //    if (dto.ImageFile != null)
        //    {
        //        string path = Path.Combine(_webHostEnvironment.WebRootPath, "User/Img");

        //        if (!string.Equals(userimg, "~/User/Img/User.png"))
        //        {
        //            string oldImagePath = Path.Combine(path, userimg);
        //            if (System.IO.File.Exists(oldImagePath))
        //            {
        //                System.IO.File.Delete(oldImagePath);
        //            }
        //        }
        //        long tick = date.Ticks;
        //        //string guid = Guid.NewGuid().ToString();
        //        string imageName = tick + "_" + dto.ImageFile.FileName;
        //        string filePath = Path.Combine(path, imageName);
        //        //FileStream fs = new FileStream(filePath, FileMode.Create);
        //        //dto.ImageFile.CopyToAsync(fs);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            dto.ImageFile.CopyTo(fileStream);
        //            fileStream.Close();
        //        }

        //        dto.ImageName = imageName;
        //    }


        //    _user.UpadateUserImage(dto.ImageName, e);
        //    return View();

        //}


        ////public IActionResult ImageUpload(List<IFormFile> postedFiles)
        ////{

        ////    string path = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
        ////    if (!Directory.Exists(path))
        ////    {
        ////        Directory.CreateDirectory(path);
        ////    }

        ////    List<string> uploadedFiles = new List<string>();
        ////    foreach (IFormFile postedFile in postedFiles)
        ////    {
        ////        string fileName = Path.GetFileName(postedFile.FileName);
        ////        string imagePath = Path.Combine(path, fileName);
        ////        using (FileStream stream = new FileStream(imagePath, FileMode.Create))
        ////        {
        ////            postedFile.CopyTo(stream);
        ////            uploadedFiles.Add(fileName);
        ////            ViewBag.Message += string.Format("<b>{0}</b> Images.<br />", fileName);
        ////        }
        ////        var email = HttpContext.Response.HttpContext.User.Identity.Name;
        ////        var userId = _user.GetUserByEmail(email).id;
        ////        var pathToDB = @"~\wwwroot\Images\" + fileName;
        ////        var user = new Users
        ////        {
        ////            id = userId,
        ////            Image = pathToDB

        ////        };
        ////        _user.UpadateUserImage(user);


        ////        TempData["Success"] = "The product has been added!";
        ////    }
        ////        return RedirectToAction("Index", "Employee");

        ////}
        ////[HttpGet]
        ////public IActionResult ImageUpload()
        ////{
        ////    return View();
        ////}
        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public IActionResult ImageUpload([Bind("ImageId,Title,ImageName")] ImageModel imageModel)
        ////{
        ////    if (ModelState.IsValid)
        ////    {
        ////        //Save image to wwwroot/image
        ////        string wwwRootPath = _webHostEnvironment.WebRootPath;
        ////        string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
        ////        string extension = Path.GetExtension(imageModel.ImageFile.FileName);
        ////        imageModel.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
        ////        string path = Path.Combine(wwwRootPath, fileName);
        ////        using (var fileStream = new FileStream(path, FileMode.Create))
        ////        {
        ////            imageModel.ImageFile.CopyTo(fileStream);
        ////        }
        ////        var email = HttpContext.Response.HttpContext.User.Identity.Name;
        ////        var userId = _user.GetUserByEmail(email).id;
        ////        var pathToDB = @"~\wwwroot\Images\" + fileName;

        ////        _user.UpadateUserImage(pathToDB,userId);
        ////        //var deleteImg = (User.Identity as ClaimsIdentity).Claims.Where(c => c.Type == "Image").FirstOrDefault();
        ////        //if (deleteImg != null)
        ////        //{
        ////        //    string oldimg = Request.Path(deleteImg.Value.ToString());
        ////        //    if (System.IO.File.Exists(oldimg))
        ////        //    {
        ////        //        System.IO.File.Delete(oldimg);
        ////        //    }
        ////        //}
        ////        ViewBag.Message = "profile picture updated successfully.";

        ////    }
        ////    return RedirectToAction("Index", "Employee");
        ////}

        ////public IActionResult DeleteImage(int? id)
        ////{
        ////    return View();
        ////}

        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public IActionResult DeleteConfirmed(int id)
        ////{
        ////    var imageModel =  _user.GetUserByID(id).Image;

        ////    //delete image from wwwroot/image
        ////    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Image", imageModel.);
        ////    if (System.IO.File.Exists(imagePath))
        ////        System.IO.File.Delete(imagePath);

        ////    _user.DeleteUserImage(imageModel);
        ////    return RedirectToAction("Index","Employee");
        ////}
        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync();
        //    return LocalRedirect("~/");
        //}

        public IActionResult ProfileView()
        {

            return View();
        }

        // User Image Delete

        [HttpGet]
        public IActionResult DeleteUserImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = _user.GetUserByID(id.GetValueOrDefault());
            if (user == null)

                return NotFound();

            return View(user);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteUserImage(ImageModel dto)
        //{
        //    var email = HttpContext.Response.HttpContext.User.Identity.Name;
        //    var userimg = _user.GetUserByEmail(email).Image;

        //    if (dto.ImageFile != null)
        //    {
        //        string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "User/Img");

        //        if (!string.Equals(userimg, "User.png"))
        //        {
        //            string oldImagePath = Path.Combine(uploadsDir, userimg);
        //            if (System.IO.File.Exists(oldImagePath))
        //            {
        //                System.IO.File.Delete(oldImagePath);
        //            }
        //        }

        //    }
        //    return RedirectToAction("ProfileView");
        //}
        // Update User

        [HttpGet]
        public IActionResult UpdateUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = _user.GetUserByID(id.GetValueOrDefault());
            if (user == null)

                return NotFound();

            return View(user);
        }

        [HttpPost]

        public IActionResult UpdateUser(int id, [Bind("id,Name,Image,")] AppUsers user)
        {
            long result = 0;
            int Status;
            string Value;

            if (ModelState.IsValid)
            {
                result = _user.UpdateUser(user);
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


        //Delete user
        [HttpGet]
        public IActionResult DeleteUser()
        {
            return View();

        }

        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            if (_user.DeleteUser(id) > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        //Data Table, Searching, sorting, Paging, Total Count,Filtering
        public JsonResult GetAllUser()
        {
            var request = new DTReq();
            request.draw = Convert.ToInt32(Request.Form["draw"]);
            request.StartRowIndex = Convert.ToInt32(Request.Form["start"]);
            request.SortExpression = Request.Form["order[0][dir]"];
            request.PageSize = Convert.ToInt32(Request.Form["length"]);
            request.SearchText = Request.Form["search[value]"];
            


            var pro = _user.GetAllUserDT(request).Result;
            return Json(pro);
        }


    }
}
