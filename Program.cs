using RP_task.AppCode.Interface;
using RP_task.AppCode.MiddleLayer;

namespace ShopingCart
{
    public class Program
    {
        public class Stattup
        {
            public void configureServices(IServiceCollection services)
            {
                services.AddTransient<IHR, MHR>();
            }

        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();          
            builder.Services.AddMvc();
            builder.Services.AddScoped<IHR, MHR>();
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

            app.UseRouting();

            app.UseAuthorization();
            
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=Category}/{id?}");

            app.Run();
        }
    }
}
