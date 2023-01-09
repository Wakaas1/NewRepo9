using Dapper;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using YSManagmentSystem.DAL.Data;
using YSManagmentSystem.Domain.DataTableUser;
using YSManagmentSystem.Domain.Product;
using YSManagmentSystem.Domain.User;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.BLL.User
{   
    public class UserServices : IUserServices
    {
        private readonly IDapperRepo _dapperRepo;
        
        public UserServices(IDapperRepo dapperRepo)
        {
            _dapperRepo = dapperRepo;
            
        }

        public int AddUser(AppUsers model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", -1, dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@Name", model.Name);
            param.Add("@Email", model.Email);
            param.Add("@Password", model.Password);
            param.Add("@Image", model.Image);
            param.Add("@Token", model.Token);
            param.Add("@IsVerify", model.IsVerify);
            param.Add("@CreatedDate", model.CreatedDate);
            var result = _dapperRepo.CreateUserReturnInt("dbo.AddUser", param);
            if (result > 0)
            {

            }
            return result;
        }
        public int UpdateUser(AppUsers model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", model.Id);
            param.Add("@Name", model.Name);
            param.Add("@Email", model.Email);
            param.Add("@Password", model.Password);
            param.Add("@Image", model.Image);
            param.Add("@Token", model.Token);
            var result = _dapperRepo.CreateUserReturnInt("dbo.UpdateUser", param);
            if (result > 0)
            {

            }
            return result;
        }
        public AppUsers GetUserByID(int Id)
        {

            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", Id);
            var user = _dapperRepo.ReturnList<AppUsers>("dbo.GetUserByID", param).FirstOrDefault();

            return user;
        }

        public AppUsers GetUserByEmail(string model)
        {

            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Email", model);
            var user = _dapperRepo.ReturnList<AppUsers>("dbo.GetUserByEmail", param).FirstOrDefault();

            return user;
        }

        public int UpadateUserImage(string image, int id)
        {

            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);
            param.Add("@Image", image);
            var result = _dapperRepo.CreateUserReturnInt("dbo.UserUpdateImage", param);
            if (result > 0)
            {

            }

            return result;
        }

        public int DeleteUserImage(AppUsers model)
        {

            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", model.Id);
            param.Add("@Image", model.Image);
            var result = _dapperRepo.CreateUserReturnInt("dbo.DeleteUserImage", param);
            if (result > 0)
            {

            }

            return result;
        }
        public int DeleteUser(int id)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            var user = _dapperRepo.CreateUserReturnInt("dbo.DeleteUser", param);

            return user;
        }
        public string CreatePasswordHash(string password)
        {

            var hmac = new HMACSHA512();

            byte[] passwordSalt = passwordSalt = hmac.Key;
            byte[] passwordHash = passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            string Passalt = Convert.ToBase64String(passwordSalt);
            string Pashash = Convert.ToBase64String(passwordHash);

            var createHash = Pashash + ":" + Passalt;
            return createHash;
        }

        public bool VerifyPasswordHash(string dbpassword, string password)
        {
            string[] passwordarry = dbpassword.Split(':');
            byte[] orignalhash = Convert.FromBase64String(passwordarry[1]);
            using (var hmac = new HMACSHA512(orignalhash))
            {
                var verifyHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                var orignalsalt = Convert.FromBase64String(passwordarry[0]);
                return verifyHash.SequenceEqual(orignalsalt);
            }
        }

        public void UpdatePassword(string email, string password)
        {
            Dapper.DynamicParameters param = new DynamicParameters();


            param.Add("@Email", email);
            param.Add("@Password", password);
            _dapperRepo.Execute("dbo.UpdatePassword", param);

        }

        public void UpdateToken(string email, string token)
        {
            Dapper.DynamicParameters param = new DynamicParameters();

            param.Add("@Email", email);
            param.Add("@Token", token);
            _dapperRepo.Execute("dbo.UpdateToken", param);

        }

        public IEnumerable<UserRolePartial> UserListId(int id)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);
            return _dapperRepo.ReturnList<UserRolePartial>("dbo.GetUserRoleById", param);
        }


        public IEnumerable<UserDetail> GetAllUsers(UserDetail model)
        {
            List<UserDetail> user = new List<UserDetail>();
            user = _dapperRepo.ReturnList<UserDetail>("GetUserByRole").ToList();
            return (user);
        }

        public void UserIsVerified(string email, bool verify)
        {
            Dapper.DynamicParameters param = new DynamicParameters();

            param.Add("@Email", email);
            param.Add("@IsVerify", verify);
            _dapperRepo.Execute("dbo.IsAVirify", param);

        }
        public async Task<DataTableResponse<UserDetailDT>> GetAllUserDT(DTReq request)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("SearchText", request.SearchText, DbType.String);
            param.Add("SortExpression", request.SortExpression, DbType.String);
            param.Add("StartRowIndex", request.StartRowIndex, DbType.Int32);
            param.Add("PageSize", request.PageSize, DbType.Int32);
         

            var pro = _dapperRepo.ReturnListMultiple("GetAllUserDT", param);
            var Response = new DataTableResponse<UserDetailDT>()
            {
                draw = request.draw,
                data = pro.Result.UserRec.ToList(),
                recordsFiltered = pro.Result.TotalRecord,
                recordsTotal = pro.Result.TotalRecord,

            };
            return Response;
        }

        public async Task<DataTableResponse<UserBrandDetailDT>> GetAllUserBrandDT(DTReq request)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("SearchText", request.SearchText, DbType.String);
            param.Add("SortExpression", request.SortExpression, DbType.String);
            param.Add("StartRowIndex", request.StartRowIndex, DbType.Int32);
            param.Add("PageSize", request.PageSize, DbType.Int32);


            var pro = _dapperRepo.ReturnUBListMultiple("GetAllUserBrandDT", param);
            var Response = new DataTableResponse<UserBrandDetailDT>()
            {
                draw = request.draw,
                data = pro.Result.UserbrandRec.ToList(),
                recordsFiltered = pro.Result.TotalRecord,
                recordsTotal = pro.Result.TotalRecord,

            };
            return Response;
        }

        //public async Task<DataTableResponse<UserPartial>> GetAllUserAsync(DataTableRequest request)
        //{
        //    var req = new ListingRequest()
        //    {
        //        PageNo = request.Start,
        //        PageSize = request.Length,
        //        SortColumn = request.Order[0].Column,
        //        SortDirection = request.Order[0].Dir,
        //        SearchValue = request.Search != null ? request.Search.Value.Trim() : ""
        //    };
        //    var users = await _genericRepo.GetUserAsync(req);
        //    return new DataTableResponse<UserPartial>()
        //    {
        //        draw = request.Draw,
        //        recordsTotal = users[0].TotalCount,
        //        recordsFiltered = users[0].FilteredCount,
        //        data = users.ToList(),
        //        error = ""
        //    };
        //}
    }
}



