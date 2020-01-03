using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BoardGames.Models
{
    public partial class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<Publisher> Publisher { get; set; }

        protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder, IConfiguration configuration)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(configuration.GetConnectionString("DatabaseConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasIndex(e => e.CategoryId)
                    .HasName("Category___fk");

                entity.HasIndex(e => e.PublisherId)
                    .HasName("Publisher___fk");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Age)
                    .IsRequired()
                    .HasColumnName("age")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AverageGameTime)
                    .HasColumnName("average_game_time")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Complexity)
                    .HasColumnName("complexity")
                    .HasColumnType("int(11)");
                
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.Edited)
                    .HasColumnName("edited")
                    .HasColumnType("datetime");

                entity.Property(e => e.Interaction)
                    .HasColumnName("interaction")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NoOfPlayers)
                    .IsRequired()
                    .HasColumnName("no_of_players")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PublisherId)
                    .HasColumnName("publisher_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Randomness)
                    .HasColumnName("randomness")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Game)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Category___fk");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Game)
                    .HasForeignKey(d => d.PublisherId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Publisher___fk");
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
