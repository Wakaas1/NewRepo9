using YSManagmentSystem.Domain.DataTableUser;
using YSManagmentSystem.Domain.User;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.BLL.User
{
    public interface IUserServices
    {
        int AddUser(AppUsers model);
        string CreatePasswordHash(string password);
        int DeleteUser(int id);
        int DeleteUserImage(AppUsers model);
        AppUsers GetUserByEmail(string model);
        AppUsers GetUserByID(int Id);
        int UpadateUserImage(string image, int id);
        void UpdatePassword(string email, string password);
        void UpdateToken(string email, string token);
        int UpdateUser(AppUsers model);
        void UserIsVerified(string email, bool verify);
        bool VerifyPasswordHash(string dbpassword, string password);
        IEnumerable<UserDetail> GetAllUsers(UserDetail model);
        IEnumerable<UserRolePartial> UserListId(int id);
        Task<DataTableResponse<UserDetailDT>> GetAllUserDT(DTReq request);
        Task<DataTableResponse<UserBrandDetailDT>> GetAllUserBrandDT(DTReq request);
    }
}