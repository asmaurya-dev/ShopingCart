using RP_task.AppCode.Interface;
using RP_task.AppCode.MiddleLayer;
using ShopingCart.Models;

namespace ShopingCart
{
    public class Program
    {
        public class Stattup
        {
            public void configureServices(IServiceCollection services)
            {
                services.AddTransient<IHR, MHR>();
                services.AddScoped<EmailService>();
            }

        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();  
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddMvc();
            builder.Services.AddScoped<IHR, MHR>();
            builder.Services.AddSession();

            var app = builder.Build();
            // Add services to the container.
            
      

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession(); 
            app.UseRouting();

            app.UseAuthorization();
            
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=User}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
