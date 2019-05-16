using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Proje.Business.Logic;
using Proje.DataAccess.Context;
using Proje.Entity.Model;
using Proje.Interface;

namespace Proje.AspNetCoreWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var connection = @"Server=(localdb)\mssqllocaldb;Database=Proje;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<ProjeContext>(options => options.UseSqlServer(connection));
            services.AddScoped(p => new ProjeContext(p.GetService<DbContextOptions<ProjeContext>>()));
            services.AddScoped<IGenericService<Advert>,AdvertLogic>();
            services.AddScoped<IGenericService<AdvertStatus>, AdvertStatusLogic>();
            services.AddScoped<IGenericService<Avatar>, AvatarLogic>();
            services.AddScoped<IGenericService<Category>, CategoryLogic>();
            services.AddScoped<IGenericService<Email>, EmailLogic>();
            services.AddScoped<IGenericService<Message>, MessageLogic>();
            services.AddScoped<IGenericService<OldPassword>, OldPasswordLogic>();
            services.AddScoped<IGenericService<Password>, PasswordLogic>();
            services.AddScoped<IGenericService<Phone>, PhoneLogic>();
            services.AddScoped<IGenericService<Photo>, PhotoLogic>();
            services.AddScoped<IGenericService<RegisterTemp>, RegisterTempLogic>();
            services.AddScoped<IGenericService<Role>, RoleLogic>();
            services.AddScoped<IGenericService<SharedPhoto>, SharedPhotoLogic>();
            services.AddScoped<IGenericService<UniCity>, UniCityLogic>();
            services.AddScoped<IGenericService<Univercity>, UnivercityLogic>();
            services.AddScoped<IGenericService<UserLocation>, UserLocationLogic>();
            services.AddScoped<IGenericService<User>, UserLogic>();
            services.AddScoped<IGenericService<UserPassword>, UserPasswordLogic>();
            services.AddScoped<IGenericService<UserStarAndComment>, UserStarAndCommentLogic>();
            services.AddScoped<IGenericService<UserTime>, UserTimeLogic>();
            services.AddScoped<IGenericService<AdvertSeenUser>, AdvertSeenUserLogic>();
            services.AddScoped<IGenericService<Notification>, NotificationLogic>();
            services.AddScoped<IGenericService<NotificationType>, NotificationTypeLogic>();
            services.AddScoped<IGenericService<FavAdvert>, FavAdvertLogic>();
            services.AddScoped<IGenericService<Complaint>, ComplaintLogic>();
            services.AddScoped<IGenericService<BlockedUser>, BlockedUserLogic>();

            // BloggingContext requires
            // using EFGetStarted.AspNetCore.NewDb.Models;
            // UseSqlServer requires
            // using Microsoft.EntityFrameworkCore;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
