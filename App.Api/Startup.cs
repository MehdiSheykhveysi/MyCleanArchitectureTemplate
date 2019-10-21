using App.Api.Utilities;
using App.ApplicationService.Shaared;
using App.ApplicationService.ToDoItems.UseCases.Queries;
using App.Infrastructure.Shared;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Api
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            settingsService = configuration.GetSection("SiteSettings").Get<SiteSettingsService>();
        }

        public IConfiguration Configuration { get; }
        private readonly SiteSettingsService settingsService;
        //public delegate IRepository<TEntity, TKey> ServiceResolver<TEntity, TKey>(string key) where TKey : IEquatable<TKey> where TEntity : Entity<TKey>;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SiteSettingsService>(Configuration.GetSection("SiteSettings"));
            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlServer(settingsService.DefaultConnection);
                //Disable Client Site Evaluation 
                options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
            });
            services.AddControllers();
            services.AddScopedDependencies(typeof(AppDbContext).Assembly);
            //services.AddScoped(typeof(EFCoreRepository<,>));
            //services.AddScoped<IRepository<,>> (Provider => new RepositoryCasheProxy<,>(Provider.GetService < EFCoreRepository <,> ()));
            // services.AddScoped(typeof(IRepository<,>), typeof(RepositoryCasheProxy<,>));

            services.AddMediatR(cfg => cfg.AsScoped(), typeof(ToDoItemQueryHandler).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

    }

    public partial class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
