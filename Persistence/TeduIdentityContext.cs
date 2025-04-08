using LearnMicroservice.IDP.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LearnMicroservice.IDP.Persistence;

public class TeduIdentityContext : IdentityDbContext<User>
{
    public TeduIdentityContext(DbContextOptions<TeduIdentityContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(TeduIdentityContext).Assembly);
        builder.ApplyIdentityConfiguration();
        //base.OnModelCreating(builder);
    }
}
