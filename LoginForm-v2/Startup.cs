using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LoginForm.Data.Repositorys;
using LoginForm.Data;
using LoginForm.Services;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using LoginForm;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LoginForm.Data.Repositorys;

namespace LoginForm
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
            services.AddControllersWithViews();
            services.AddDbContext<Context>(P => P.UseSqlServer("Data Source=(localdb)\\v11.0;Integrated Security=SSPI;Initial Catalog=LoginFormASPNETCore2"));

            services.AddIdentity<IdentityUser, IdentityRole>(P =>
            {
                P.Password.RequiredLength = 5;
                P.Password.RequireLowercase = false;
                P.Password.RequireUppercase = false;
                P.Password.RequireNonAlphanumeric = false;
                P.Password.RequireDigit = false;
            })
                .AddEntityFrameworkStores<Context>()
                .AddDefaultTokenProviders();

            services.AddScoped<IAccountRespository, AccountRepository>();
            services.AddScoped<IAccountsService, AccountsService>();
            services.AddScoped<IUserActivityRepository, UserActivityRepository>();
            services.AddScoped<IUserActivityService, UserActivityService>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();
            services.Configure<CommentConfiguration>(Configuration.GetSection("Comment"));

            services.AddAuthorization(option =>
            {
                option.AddPolicy("UserName", policy => policy.RequireClaim(ClaimTypeStore.UserName));
                option.AddPolicy("Password", policy => policy.RequireClaim(ClaimTypeStore.Password));
                option.AddPolicy("IsAuthunticated", Policy => Policy.RequireAssertion(Context =>
                                 Context.User.Identity.IsAuthenticated));
            });

            services.ConfigureApplicationCookie(option =>
            {
                //option.LoginPath = "~/Views/Home/Index.cshtml";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
