using eTickets.Data;
using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eTickets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //DbContext Configuration
            builder.Services.AddDbContext<AppDbContext>(options => options
            .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnictionString")));
            //Services Configration
            //Dependincy injection
            builder.Services.AddScoped<IActorsService, ActorsService>();
            //Producers Service
            builder.Services.AddScoped<IProducersService, ProducersService>();
            //Cinema Service
            builder.Services.AddScoped<ICinemasService, CinemasService>();
            //Movies Services
            builder.Services.AddScoped<IMoviesService, MoviesService>();
            //Orders Services
            builder.Services.AddScoped<IOrdersService, OrdersService>();
            //Configration for shopping cart
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));
            //Authentication and authoriztion
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddMemoryCache();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });

            builder.Services.AddSession();
            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();


            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            //seed database
            AppDbIntializer.Seed(app);
            AppDbIntializer.SeedUsersAndRolesAsync(app).Wait();
            app.Run();

        }
    }
}