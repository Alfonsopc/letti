using Letti.Data;
using Letti.Repositories.Contracts;
using Letti.Repositories.Repositories;
using Letti.Services.Contracts;
using Letti.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Letti.DataService
{
    public class Startup
    {
        private readonly string _corsPolicyName;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _corsPolicyName = "LettiCorsPolicy";
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            ConfigureCorsPolicy(services);
            SetUpDataBase(services);
            ConfigureActualServices(services);
           
        }

        public void ConfigureActualServices(IServiceCollection services)
        {
            services.AddTransient<IContractorRepository, ContractorRepository>();
            services.AddTransient<IContractorRelationshipsRepository, ContractorRelationshipsRepository>();
            services.AddTransient<IContractorScansRepository, ContractorScansRepository>();
            services.AddTransient<IPoiScanningRepository, PoiScanningRepository>();
            services.AddTransient<IContractRepository, ContractRepositoy>();
            services.AddTransient<IContractsService, ContractsService>();
            services.AddTransient<IOrganizationRepository, OrganizationRepository>();
            services.AddTransient<IOrganizationRelationshipsRepository, OrganizationRelationshipsRepository>();
            services.AddTransient<IPersonOfInterestRepository, PersonOfInterestRepository>();
            services.AddTransient<IPersonalRelationshipsRepository, PersonalRelationshipRepository>();
            services.AddTransient<IPoiService, PoiService>();
            
        }
        public virtual void SetUpDataBase(IServiceCollection services)
        {
            //Add EF Core Db Context            
            // services.AddDbContext<LettiContext>(opt => opt.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            //services.AddDbContext<LettiContext>(opt => opt.UseSqlServer(@"Server=localhost\sqlexpress;Database=LettiDb;Trusted_Connection=True;"));
            services.AddDbContext<LettiContext>(opt => opt.UseSqlServer(@"Server=CAMILLEDATA;Database=LettiDb;User ID=lettiuserdev;Password=letti@zbk;"));
            // services.AddDbContext<LettiContext>(opt => opt.UseSqlServer(@"Server=tcp:albaroja.database.windows.net,1433;Initial Catalog=sindicatodigital;Persist Security Info=False;User ID=saadminsnd;Password=snd78yt44#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));

        }
        private void ConfigureCorsPolicy(IServiceCollection services)
        {
            var corsBuilder = new CorsPolicyBuilder();

            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            //corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy(_corsPolicyName, corsBuilder.Build());
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseAuthorization();
            app.UseCors(_corsPolicyName);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
