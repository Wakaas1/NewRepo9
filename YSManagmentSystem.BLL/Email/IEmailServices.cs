

namespace YSManagmentSystem.BLL.Email
{
    public interface IEmailServices
    {
        //EmailServices(IConfiguration configuration, IWebHostEnvironment webHostEnvironment);
        void SendEmail(string email, string token, string name);
    }
}