using App.Api.Middlewars;
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

            //Add Essential mvc service and Register FulenValidaation inside this
            services.AddControllers();

            services.AddMemoryCache();

            services.AddScopedDependencies(typeof(AppDbContext).Assembly, typeof(SiteSettingsService).Assembly);

            services.AddMediatR(cfg => cfg.AsScoped(), typeof(ToDoItemPagedQueryHandler).Assembly);

            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
        }

    }

    public partial class Startup
    {
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseErrorHandlingMiddleware();
            }

            app.UseMvc();
        }
    }
}
