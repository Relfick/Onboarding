using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Onboarding.Models;

public sealed class ApplicationContext: DbContext
{
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Article> Articles { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Department> Departments { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(CategoryConfigure);
        modelBuilder.Entity<Article>(ArticleConfigure);
        modelBuilder.Entity<Employee>(EmployeeConfigure);
        modelBuilder.Entity<Role>(RoleConfigure);
        modelBuilder.Entity<Department>(DepartmentConfigure);
        modelBuilder.Entity<City>(CityConfigure);
    }

    private void CategoryConfigure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(category => category.Id).HasColumnName("id");
        builder.Property(category => category.Name).HasColumnName("name");
    }
    
    private void ArticleConfigure(EntityTypeBuilder<Article> builder)
    {
        builder.Property(article => article.Id).HasColumnName("id");
        builder.Property(article => article.Title).HasColumnName("title");
        builder.Property(article => article.Text).HasColumnName("text");
        builder.Property(article => article.CategoryId).HasColumnName("category_id");
        builder
            .HasOne<Category>()
            .WithMany()
            .HasForeignKey(a => a.CategoryId);
    }
    
    private void RoleConfigure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(role => role.Id).HasColumnName("id");
        builder.Property(role => role.Name).HasColumnName("name");
    }
    
    private void CityConfigure(EntityTypeBuilder<City> builder)
    {
        builder.Property(city => city.Id).HasColumnName("id");
        builder.Property(city => city.Name).HasColumnName("name");
    }
    
    private void DepartmentConfigure(EntityTypeBuilder<Department> builder)
    {
        builder.Property(department=> department.Id).HasColumnName("id");
        builder.Property(department => department.Name).HasColumnName("name");
    }

    private void EmployeeConfigure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(employee => employee.Id).HasColumnName("id");
        builder.Property(employee => employee.Name).HasColumnName("name");
        builder.Property(employee => employee.CityId).HasColumnName("city_id");
        builder.Property(employee => employee.DepartmentId).HasColumnName("department_id");
        builder.Property(employee => employee.RoleId).HasColumnName("role_id");
        builder.Property(employee => employee.PhoneNumber).HasColumnName("phone_number");
        builder.Property(employee => employee.TgUsername).HasColumnName("tg_username");
        builder.Property(employee => employee.TgUserId).HasColumnName("tg_user_id");
    }
}