using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using UniversityMVC.Bots;
using UniversityMVC.Models;

namespace UniversityMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

    /*        builder.Services.AddHttpClient();
            builder.Services.AddSingleton<ConversationState>();
            builder.Services.AddTransient<IBot, EchoBot>();
            builder.Services.AddBot<EchoBot>(options =>
            {
                var conversationState = builder.Services.BuildServiceProvider().GetService<ConversationState>();
                options.State.Add(conversationState);
            });
*/

            builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiUrls"));
            // Add services to the container.
            builder.Services.AddControllersWithViews();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
          
            app.Run();
        }
    }
}
