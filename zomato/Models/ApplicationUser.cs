using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
public class ApplicationUser : IdentityUser 
{
    [NotMapped]
    public string Password {get;set;}
    [NotMapped]
    public List<string> Role {get;set;}
}


    public static class Role
    {
        public const string ProjectManager = "ProjectManager";
        public const string Admin = "Admin";
        public const string User = "User";
    }
