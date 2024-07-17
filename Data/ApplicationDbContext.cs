using GameZone.Models;

namespace GameZone.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Platform> Platforms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
            [
                new Category { Id = 1, Name="Sports"},
                new Category { Id = 2, Name="Action"},
                new Category { Id = 3, Name="Adventure"},
                new Category { Id = 4, Name="Fighting"},

            ]);

            modelBuilder.Entity<Platform>().HasData([
                new Platform{Id =1, Name="PLayStation", Icone="bi bi-playstation"},
                new Platform{Id =2, Name="XBox", Icone="bi bi-xbox"},
                new Platform{Id =3, Name="PC", Icone="bi bi-pc-display"}


                ]);
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Platforms)
                .WithMany(g => g.Games)
                .UsingEntity<GamePlatform>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
