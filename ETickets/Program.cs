using ETickets.Data;
using ETickets.IRepositry;
using ETickets.Models;
using ETickets.Repositry;
using ETickets.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace ETickets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            builder.Services.AddDbContext<ApplicationDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IMovieRepositry, MovieRepositry>();
            builder.Services.AddScoped<ICinemaRepositry, CinemaRepositry>();
            builder.Services.AddScoped<ICategoryRepositry, CategoryRepositry>();
            builder.Services.AddScoped<IActorRepositry, ActorRepositry>();
            builder.Services.AddScoped<IActorMovieRepositry, ActorMovieRepositry>();
            builder.Services.AddScoped<ICartItemRepositry, CartItemRepositry>();
            builder.Services.AddScoped<ICartRepositry, CartRepositry>();
            builder.Services.AddScoped<IOrderItemRepositry, OrderItemRepositry>();
            builder.Services.AddScoped<IOrderRepositry, OrderRepositry>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                   .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddTransient<IEmailSender, EmailSender>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
