using PSClassLib.Interface;
using PSClassLib.Model;
using PSClassLib.Repo;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace PinkScouts
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            
            // Scoped gives every request its own repo
            // When a page asks for IRepo<Activity>, give it an ActivityRepo (new one per request)
            builder.Services.AddScoped<IRepo<Activity>, ActivityRepo>();




            //
            // for setting up claims based authentication with cookies, we need to add the following code to the Program.cs file:
            //
            builder.Services.Configure<CookiePolicyOptions>(options => {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request. options.CheckConsentNeeded = context => true; 
                options.MinimumSameSitePolicy = SameSiteMode.None;

            });

            // Where should failed logins be moved to? 
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(cookieOptions => {
                cookieOptions.LoginPath = "/Index";

            });

            // Where and who do we send here?
            builder.Services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeFolder("/Profile");
                options.Conventions.AuthorizeFolder("/Profile_leader");
                
            });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
