

namespace YSManagmentSystem.Domain.User
{
    public class UserDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string roles_list { get; set; }
    }
    public class UserBrandDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string brand_list { get; set; }
    }
}
