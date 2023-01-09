using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YSManagmentSystem.BLL.User;
using YSManagmentSystem.Domain.User;
using YSManagmentSystem.web.DTO;
using YSManagmentSystem.BLL.Email;
using YSManagmentSystem.BLL.UserRole;

namespace ManagmentSystem.web.Controllers
{
    public class AccountController : Controller
    {

        private readonly IEmailServices _email;
        private readonly IUserServices _user;
        private readonly IRoleServices _role;
        private readonly IWebHostEnvironment _webHostEnvironment;


        private class ICustomICustomClaimsCookieSignInHelper { }
        public AccountController(IUserServices user, IWebHostEnvironment webHostEnvironment, IRoleServices role, IEmailServices email)
        {
            _user = user;
            _webHostEnvironment = webHostEnvironment;
            _role = role;
            _email = email;

        }

        // User Registeration
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        
        public IActionResult Register(RegisterDto dto)
        {
            string date = DateTime.UtcNow.ToString();

            string imageName = "User.png";

            //if (ModelState.IsValid)
            //{
                System.Guid guid = System.Guid.NewGuid();
                var token = guid.ToString();

                DateTime now = DateTime.UtcNow;

                if (dto.Password == dto.confirmPassword)
                {
                    var user = new AppUsers
                    {
                        Name = dto.Name,
                        Email = dto.Email,
                        Password = _user.CreatePasswordHash(dto.Password),
                        Token = token,
                        IsVerify = true,
                        CreatedDate = now,
                        Image = imageName
                    };
                
                 int uid =  _user.AddUser(user);
                int rid = 2;
                _role.AddUserRole(uid, rid);
                    //string lnkHref = "" + Url.Action("ConfirmOTP", "User", new { token }, "https") + "";

                    //_email.SendEmail(dto.Email, lnkHref, dto.Name);

                    //string email = dto.Email;

                    //TempData["Email"] = email;
                    return RedirectToAction("Login");
                }
                else
                {
                    return BadRequest("Please enter correct detail.");
                }
            //}
            
        }

        //User exist same name in db

       [AcceptVerbs("Get", "Post")]
        public IActionResult IsUserAlreadyExists(string email)
        {
            var user = _user.GetUserByEmail(email);
            if (user != null)
            {
                return Json($"Email already exist.");
            }

            else
                return Json(true);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var req = _user.GetUserByEmail(dto.Email);
            int userId = req.Id;
            if (req.IsVerify == true)
            {

                if (_user.VerifyPasswordHash(req.Password, dto.Password))
                {
                    var cl = SignInUser(req, true, userId);

                    var identity = new ClaimsIdentity(cl, "DDLO");
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(10),
                        IsPersistent = true
                    });
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return BadRequest("User not verified");
            }
            return View();
        }

        // Custom Claims 
        private List<Claim> SignInUser(AppUsers currentUser, bool isPersistent, int id)
        {
            //Initialization
            var claims = new List<Claim>();

            try
            {
                //custom claims
                claims.Add(new Claim("Id", currentUser.Id.ToString()));
                claims.Add(new Claim("Name", currentUser.Name.ToString()));
                claims.Add(new Claim("Email", currentUser.Email.ToString()));
                claims.Add(new Claim("Image", currentUser.Image.ToString()));
                //Id Profile Picutue
                var userrole = _user.UserListId(id).ToArray();
                foreach (var item in userrole)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item.RoleName));
                }
                return claims;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Forgot Password

       [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(ForgotPassword dto, string email)
        {
            if (ModelState.IsValid)
            {
                var Email = _user.GetUserByEmail(email);
                if(dto.NewPassword == dto.ConfirmPassword)
                {
                    var pass = _user.CreatePasswordHash(dto.NewPassword);
                    _user.UpdatePassword(email, pass);
                    return RedirectToAction("Login");
                }  
            }
            else
            {
                // If user does not exist or is not confirmed.
                return BadRequest("Password does not match.");
            }
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return LocalRedirect("~/");
        }
    }
}
