using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Db.Entities;

namespace ToDoApp.DB;

public class AppDbContext : IdentityDbContext<UserEntity, RoleEntity, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<SendEmailRequestEntity> SendEmailRequests { get; set; }
    public DbSet<TodoEntity> Todos { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //builder.Entity<UserEntity>().ToTable("Users");
        //builder.Entity<RoleEntity>().ToTable("Roles");
        //builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
        //builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
        //builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
        //builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
        //builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
    }
}