using Microsoft.EntityFrameworkCore;

namespace LawyerWebSiteMVC.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options){}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var admin = new AppUser()
		{
			Id = 1,
            IsDeleted = false,
            CreatedDate = DateTime.Now,
			FullName = "Admin",
			Email = "admin@admin.com",
			Password = "Admin123",
			Role = "Admin"
		};
		modelBuilder.Entity<AppUser>().HasData(admin);
		base.OnModelCreating(modelBuilder);
    }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<ArticlePhoto> ArticlePhotos { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Letter> Letters { get; set; }
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }

}
