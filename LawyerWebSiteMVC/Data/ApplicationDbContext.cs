using Microsoft.EntityFrameworkCore;

namespace LawyerWebSiteMVC.Data;

    public class ApplicationDbContext : DbContext
    {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options){}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<ArticlePhoto> ArticlePhotos { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Letter> Letters { get; set; }
    public DbSet<Slider> Sliders { get; set; }
    }
