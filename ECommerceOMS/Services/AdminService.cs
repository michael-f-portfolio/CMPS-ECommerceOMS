using ECommerceOMS.Models.Identity;
using ECommerceOMS.Repositories;

namespace ECommerceOMS.Services
{
    public class AdminService
    {
        private readonly UserRepository _userRepository;

        public AdminService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> ChangeRoleAsync(string actingUserEmail, string targetEmail, RoleType newRole)
        {
            var actingUser = await _userRepository.FindByEmailAsync(actingUserEmail);
            if (actingUser == null) return false;

            var actingRole = await _userRepository.GetRoleAsync(actingUser);
            if (actingRole == null)
                return false;

            var targetUser = await _userRepository.FindByEmailAsync(targetEmail);
            if (targetUser == null)
                return false;

            var targetUserCurrentRole = await _userRepository.GetRoleAsync(targetUser);

            // Has SuperAdmin Role - Can modify any role
            if (actingRole == RoleType.SuperAdmin.ToName())
            {
                if (targetUserCurrentRole != null)
                    await _userRepository.RemoveFromRoleAsync(targetUser, targetUserCurrentRole);

                return await _userRepository.AddToRoleAsync(targetUser, newRole.ToName());
            }

            // Has Admin Role - Can modify Buyer and Seller roles only
            if (actingRole == RoleType.Admin.ToName())
            {
                // Admin cannot edit fellow Admins or SuperAdmins
                if (targetUserCurrentRole == RoleType.SuperAdmin.ToName() ||
                    targetUserCurrentRole == RoleType.Admin.ToName())
                    return false;

                // Admin cannot promote to Admin or SuperAdmin
                if (newRole == RoleType.Admin || 
                    newRole == RoleType.SuperAdmin)
                    return false;

                // Admin can promote Buyers/demote Sellers
                if (targetUserCurrentRole == RoleType.Seller.ToName() ||
                    targetUserCurrentRole == RoleType.Buyer.ToName())
                {
                    await _userRepository.RemoveFromRoleAsync(targetUser, targetUserCurrentRole);
                    return await _userRepository.AddToRoleAsync(targetUser, newRole.ToName());
                }
            }

            // Has Seller/Buyer Role - Cannot modify any roles
            return false;
        }

        public async Task<List<ApplicationUser>> GetAdminsAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();

            return users.Where(u =>
                _userRepository.GetRoleAsync(u).Result.Contains(RoleType.Admin.ToName()) ||
                _userRepository.GetRoleAsync(u).Result.Contains(RoleType.SuperAdmin.ToName())
                ).ToList();
                
        }

        // Lockout method, would need a SuperDuperAdmin to lockout SuperAdmin, beyond scope
        public async Task<bool> ToggleLockAsync(string actingUserEmail, string targetEmail)
        {
            var actingUser = await _userRepository.FindByEmailAsync(actingUserEmail);
            if (actingUser == null) 
                return false;

            var actingRole = (await _userRepository.GetRoleAsync(actingUser));
            if (actingRole == null)
                return false;

            var targetUser = await _userRepository.FindByEmailAsync(targetEmail);
            if (targetUser == null) 
                return false;

            var targetRole = (await _userRepository.GetRoleAsync(targetUser));

            // SuperAdmins cannot be locked out
            if (targetRole == null || 
                targetRole == RoleType.SuperAdmin.ToName())
                return false;

            // Don't lock yourself
            if (actingUserEmail == targetEmail)
                return false;

            // SuperAdmin - Can lockout Buyer, Seller, and Admin
            if (actingRole == RoleType.SuperAdmin.ToName())
            {
                if (targetUser.isLocked)
                {
                    await _userRepository.SetLockoutEndDateAsync(targetUser, null);
                    await _userRepository.ResetAccessFailedCountAsync(targetUser);
                }
                else
                {
                    await _userRepository.SetLockoutEndDateAsync(targetUser, DateTimeOffset.UtcNow.AddYears(100));
                }
                return true;
            }

            // Admin - Can lockout Buyers and Seller
            if (actingRole == RoleType.Admin.ToName())
            {
                // Admins cannot lockout other Admins
                if (targetRole == RoleType.Admin.ToName())
                    return false;

                if (targetUser.isLocked)
                {
                    await _userRepository.SetLockoutEndDateAsync(targetUser, null);
                    await _userRepository.ResetAccessFailedCountAsync(targetUser);
                }
                else
                {
                    await _userRepository.SetLockoutEndDateAsync(targetUser, DateTimeOffset.UtcNow.AddYears(100));
                }

                return true;
            }

            return false;
        }
    }
}
