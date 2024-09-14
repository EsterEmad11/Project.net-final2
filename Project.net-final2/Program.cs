using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.net_final2.Context;
namespace Project.net_final2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddRazorPages();


            // Configure the database context with the connection string from appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("ProjectContextConnection")
                                   ?? throw new InvalidOperationException("Connection string 'ProjectContextConnection' not found.");

            builder.Services.AddDbContext<ProjectContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ProjectContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

#pragma warning disable ASP0014 // Suggest using top level route registrations
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages(); // This line ensures Razor Pages are mapped
            });
#pragma warning restore ASP0014 // Suggest using top level route registrations

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");

            //app.UseEndpoints(endpoint => endpoint.MapRazorPages());
            app.Run();
        }
    }
}
