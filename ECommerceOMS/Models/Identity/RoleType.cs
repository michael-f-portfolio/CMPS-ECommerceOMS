namespace ECommerceOMS.Models.Identity
{
    public enum RoleType
    {
        SuperAdmin,
        Admin,
        Seller,
        Buyer
    }

    public static class RoleExtensions
    {
        public static string ToName(this RoleType role)
        {
            return role.ToString();
        }
    }
}
