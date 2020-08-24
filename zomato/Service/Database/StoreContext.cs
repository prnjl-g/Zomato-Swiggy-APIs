using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Service.Database;
namespace Service.Database
{

  public class StoreContext : IdentityDbContext<ApplicationUser>
  {
    public StoreContext(){}
    public StoreContext(DbContextOptions<StoreContext> options)
      : base(options)
    { }
     
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=BiraApiData;User Id=SA; Password=YourStrong@Passw0rd;");
    }
    public DbSet<Issue> IssueList{get; set;}
    public DbSet<Project> ProjectDetails{get; set;} 
    public DbSet<Comment> CommentsOnIssues{get; set;}
    public DbSet<Sprint> Sprints{get; set;}
    public DbSet<Label> Labels{get; set;}
    public DbSet<TimeLogging> TimeLoggings{get; set;}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       base.OnModelCreating(modelBuilder);
       modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = Role.ProjectManager, NormalizedName = Role.ProjectManager.ToUpper() });
    }
  }
}

public static class ApplicationDbInitializer
{
    public static void SeedUsers(UserManager<ApplicationUser> userManager)
    {
        if (userManager.FindByEmailAsync("projectmanager@gamil.com").Result==null)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = "projectManager",
                Email = "projectmanager@gamil.com"
            };

            IdentityResult result = userManager.CreateAsync(user, "projectManager@admin01").Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, Role.ProjectManager).Wait();
            }
        }       
    }   
}