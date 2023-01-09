using Dapper;
using System.Data;
using YSManagmentSystem.DAL.Data;
using YSManagmentSystem.Domain.User;

namespace YSManagmentSystem.BLL.UserRole
{
    public class RoleServices : IRoleServices
    {
        private readonly IDapperRepo _dapperRepo;
        public RoleServices(IDapperRepo dapperRepo)
        {
            _dapperRepo = dapperRepo;

        }

        public int AddRole(AppRole model)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@RoleId", -1, dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@RoleName", model.RoleName);
            return _dapperRepo.CreateRoleReturnInt("AddRole", param);
        }

        public int AddUserRole(int userId, int roleId)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@UId", userId);
            param.Add("@RId", roleId);
            return _dapperRepo.CreateUserReturnFKInt("AddUserRole", param);
        }
        public List<RoleEdit> GetAllRole(int uId)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@userid", uId);
            return _dapperRepo.ReturnList<RoleEdit>("dbo.GetAllRole", param).ToList();

        }

        public void RemoveRole(int userId, int roleId)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@UId", userId);
            param.Add("@RId", roleId);
            _dapperRepo.CreateUserReturnFKInt("DeleteRole", param);
        }
        public IEnumerable<UserDetail> GetAllUsers(UserDetail model)
        {
            List<UserDetail> user = new List<UserDetail>();
            user = _dapperRepo.ReturnList<UserDetail>("GetUserByRole").ToList();
            return (user);
        }

        public int AddUserBrand(int userId, int brandId)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@UId", userId);
            param.Add("@BId", brandId);
            return  _dapperRepo.CreateUserReturnFKInt("AddUserBrand", param);
        }
        public int RemoveBrand(int userId, int brandId)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@UId", userId);
            param.Add("@BId", brandId);
            return _dapperRepo.CreateUserReturnFKInt("RemoveUserBrand", param);
        }
        public List<BrandsEdit> GetAllUserBrand(int uId)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@userid", uId);
            return _dapperRepo.ReturnList<BrandsEdit>("dbo.GetAllUserBrand", param).ToList();

        }
    }
}
