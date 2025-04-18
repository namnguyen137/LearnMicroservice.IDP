﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduMicroservices.IDP.Infrastructure.Domains;

namespace TeduMicroservices.IDP.Infrastructure.Entities;

public class Permission : EntityBase<long>
{
    public Permission()
    {
    }
    public Permission(string function, string command, string roleId)
    {
        Function = function;
        Command = command;
        RoleId = roleId;
    }
    public Permission(long id, string function, string command, string roleId) : this(function, command, roleId)
    {
        Id = id;
    }

    [Key]
    [MaxLength(50)]
    [Column(TypeName = "varchar(50)")]
    public string Function { get; set; }
    [Key]
    [MaxLength(50)]
    [Column(TypeName = "varchar(50)")]
    public string Command { get; set; }
    [ForeignKey("RoleId")]
    public string RoleId { get; set; }
    public virtual IdentityRole Role { get; set; }
}
