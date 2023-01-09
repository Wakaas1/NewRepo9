
namespace YSManagmentSystem.Domain.User
{
    public partial class UserRolePartial
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }

        public string Token { get; set; }
        public string IsVerify { get; set; }

    }
    public partial class UserRolePartial
    {
        public int UId { get; set; }
        public int RId { get; set; }

    }
    public partial class UserRolePartial
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
