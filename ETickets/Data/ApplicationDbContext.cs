using ETickets.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Metadata;
using ETickets.ViewModels;
using ETickets.ViewModels.Category;
using ETickets.ViewModels.Cart;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;

namespace ETickets.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet <Cinema>Cinemas { get; set; }
        public DbSet<Movie> Movies { get; set; }

        public DbSet<Actor>Actors { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order  > Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

            // Retrieve the connection string
            string? connectionString = builder.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //PKs

            modelBuilder.Entity<ActorMovie>().ToTable("ActorMovies")
               .HasKey(e => new { e.MoviesId, e.ActorsId });

            modelBuilder.Entity<Cart>().HasKey(e => e.Id);

            modelBuilder.Entity<CartItem>().ToTable("CartItems")
           .HasKey(e => new { e.MovieId, e.CartId });

            modelBuilder.Entity<Category>().HasKey(e => e.Id);

            modelBuilder.Entity<Movie>().HasKey(e => e.Id);
            modelBuilder.Entity<Actor>().HasKey(e => e.Id);

            modelBuilder.Entity<Cinema>().HasKey(e => e.Id);
            
            modelBuilder.Entity<OrderItem>().HasKey(e =>new { e.MovieId, e.OrderId });


            //Relationships

            modelBuilder.Entity<Category>().
                HasMany(e => e.Movies)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryId)
                .IsRequired();

            modelBuilder.Entity<Actor>()
               .HasMany(a => a.Movies)
               .WithMany(m => m.Actors).
               UsingEntity<ActorMovie>();

            modelBuilder.Entity<Cinema>()
                .HasMany(e => e.Movies)
                .WithOne(e => e.Cinema)
                .HasForeignKey(e => e.CinemaId);
           
            modelBuilder.Entity<Cart>()
                .HasMany(e => e.Movies)
                .WithMany(e => e.Carts).
                UsingEntity<CartItem>();

            modelBuilder.Entity<Cart>()
                .HasOne(e => e.User)
                .WithMany(e => e.Carts).
                HasForeignKey(e=>e.UserId);




            //props

            //Categories

            modelBuilder.Entity<Category>().Property(e => e.Name).IsRequired();

            //Cinema
            modelBuilder.Entity<Cinema>().Property(e=>e.Name).IsRequired();
            
            modelBuilder.Entity<Cinema>().Property(e=>e.Description).IsRequired(false);
            
            modelBuilder.Entity<Cinema>().Property(e=>e.CinemaLogo).IsRequired(true);

            modelBuilder.Entity<Cinema>().Property(e=>e.Address).IsRequired(true);


            //Movie

            modelBuilder.Entity<Movie>().Property(e=>e.StartDate).IsRequired(true);
            modelBuilder.Entity<Movie>().Property(e=>e.EndDate).IsRequired(true);

            modelBuilder.Entity<Movie>().Property(e=>e.Price).IsRequired(true);

            modelBuilder.Entity<Movie>().Property(e=>e.Description).IsRequired(true);
            modelBuilder.Entity<Movie>().Property(e=>e.TrailerUrl).IsRequired(false);
            modelBuilder.Entity<Movie>().Property(e=>e.ImgUrl).IsRequired(true);


            modelBuilder.Entity<Movie>()
                        .Property(m => m.MovieStatus)
                        .HasComputedColumnSql("CASE " +
                                              "WHEN SYSDATETIME() > EndDate THEN 2 " + // Expired
                                              "WHEN SYSDATETIME() >= StartDate THEN 1 " + // Available
                                              "ELSE 0 " + // Upcoming
                                              "END");

        }

        



    }

    }

