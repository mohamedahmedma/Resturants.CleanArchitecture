using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using Restaurant.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.Authorization
{
    public class RestaurantUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
    {
        private readonly UserManager<User> _userManager;
        private readonly IOptions<IdentityOptions> _options;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RestaurantUserClaimsPrincipalFactory(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _options = options;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var id = await GenerateClaimsAsync(user);
            if(user.Nationality != null)
            {
                id.AddClaim(new Claim(AppClaimTypes.Nationality , user.Nationality));
            }
            if(user.DateOfBirth != null)
            {
                id.AddClaim(new Claim(AppClaimTypes.DateOfBirth, user.DateOfBirth.Value.ToString("yyyy-MM-dd")));
            }

            return new ClaimsPrincipal(id);
        }
    }
}
