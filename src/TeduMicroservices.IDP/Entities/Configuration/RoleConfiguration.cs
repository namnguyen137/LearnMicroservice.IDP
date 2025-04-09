﻿using LearnMicroservice.IDP.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnMicroservice.IDP.Entities.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Name = SystemConstants.Roles.Administrator,
                NormalizedName = SystemConstants.Roles.Administrator.ToUpper(),
                Id = "b6105f01-18f5-433c-91e0-dbd80d27e7f4"
            },
            new IdentityRole
            {
                Name = SystemConstants.Roles.Customer,
                NormalizedName = SystemConstants.Roles.Customer.ToUpper(),
                Id = "b4365573-ff95-4015-8dd0-adf0650354a2"
            }
        );
    }
}
