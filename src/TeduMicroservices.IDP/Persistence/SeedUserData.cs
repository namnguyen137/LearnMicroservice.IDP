using Duende.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TeduMicroservices.IDP.Infrastructure.Common;
using TeduMicroservices.IDP.Infrastructure.Entities;
using TeduMicroservices.IDP.Persistence;

namespace LearnMicroservice.IDP.Persistence;

public static class SeedUserData
{
    public static async Task EnsureSeedDataAsync(string connectionString)
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddDbContext<TeduIdentityContext>(opt =>
        opt.UseSqlServer(connectionString));

        services.AddIdentity<User, IdentityRole>(opt =>
        {
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequireDigit = false;
            opt.Password.RequiredLength = 6;
            opt.Password.RequireUppercase = false;
            opt.Password.RequireLowercase = false;
            //opt.User.RequireUniqueEmail = true;
            //opt.Lockout.AllowedForNewUsers = true;
            //opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //opt.Lockout.MaxFailedAccessAttempts = 3;
        })
        .AddEntityFrameworkStores<TeduIdentityContext>()
        //.AddUserStore<TeduUserStore>()
        .AddDefaultTokenProviders();

        using (var serviceProvider = services.BuildServiceProvider())
        {
            using (var scope = serviceProvider
                .GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                await CreateAdminUserAsync(scope, "Alice", "Smith", "Alice Smith's address",
                    Guid.NewGuid().ToString(), "alice123",
                    SystemConstants.Roles.Administrator, "alicesmith@example.com");
            }
        }
    }

    //private static async Task SeedAdminPermissions(IServiceScope scope)
    //{
    //    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    //    var role = await roleManager.FindByNameAsync(SystemConstants.Roles.Administrator);
    //    if (role is not null)
    //    {
    //        await using var teduContext = scope.ServiceProvider
    //            .GetRequiredService<TeduIdentityContext>();
    //        var adminPermissions = await teduContext.Permissions.Where(x => x.RoleId.Equals(role.Id)).ToListAsync();
    //        if (!adminPermissions.Any())
    //        {
    //            var permissions = PermissionHelper.GetAllPermissions();
    //            foreach (var permission in permissions)
    //            {
    //                var permissionEntity = new Permission(permission.Function, permission.Command, role.Id);
    //                teduContext.Permissions.Add(permissionEntity);
    //            }
    //            await teduContext.SaveChangesAsync();
    //        }
    //    }
    //}
    private static async Task CreateAdminUserAsync(IServiceScope scope, string firstName, string lastName,
        string address, string id, string password, string role, string email)
    {
        var userManagement = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var user = await userManagement.FindByNameAsync(email);
        if (user == null)
        {
            user = new User
            {
                UserName = email,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                EmailConfirmed = true,
                Id = id,
            };
            var result = await userManagement.CreateAsync(user, password);
            CheckResult(result);

            var addToRoleResult = await userManagement.AddToRoleAsync(user, role);
            CheckResult(addToRoleResult);

            result = userManagement.AddClaimsAsync(user, new Claim[]
            {
                new(SystemConstants.Claims.UserName, user.UserName),
                new(SystemConstants.Claims.FirstName, user.FirstName),
                new(SystemConstants.Claims.LastName, user.LastName),
                new(SystemConstants.Claims.Roles, role),
                new(JwtClaimTypes.Address, user.Address),
                new(JwtClaimTypes.Email, user.Email),
                new(ClaimTypes.NameIdentifier, user.Id),
            }).Result;
            CheckResult(result);
        }
    }
    private static void CheckResult(IdentityResult result)
    {
        if (!result.Succeeded)
        {
            throw new Exception(result.Errors.First().Description);
        }
    }

}
