using Company.Muhanad.BLL;
using Company.Muhanad.BLL.Interfaces;
using Company.Muhanad.BLL.Repositories;
using Company.Muhanad.DAL.Data.Contexts;
using Company.Muhanad.DAL.Models;
using Company.Muhanad.PL.Mapping;
using Company.Muhanad.PL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company.Muhanad.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //builder.Services.AddScoped<AppDBContext>();
            builder.Services.AddDbContext<AppDBContext>(options => 
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
                 


            }
            );

            builder.Services.AddScoped<IDepartment, DepartmentRepository>();
            /*builder.Services.AddScoped<IEmployee, EmployeeRepository>();*/

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IScopedService,ScopedService>();
            builder.Services.AddTransient<ITransientService,TransientService>();
            builder.Services.AddSingleton<ISingletonService,SingletonService>();
            builder.Services.AddAutoMapper(typeof(EmployeeProfile));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<AppDBContext>()
                            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(configure =>
            {
                configure.LoginPath = "/Account/SignIn";
               
            });

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

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
