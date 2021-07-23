using MaiAnVat.Common;
using MaiAnVat.ServiceFramework;
using MaiAnVat.ServiceFramework.JobAndJobType;
using MaiAnVat.Services;
using MaiAnVat.Services.JobAndJobType;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace MaiAnVat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            RegisterServices(services);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
               {
                   RequireExpirationTime = true,
                   ValidIssuer = MAVClaimTypes.kMAVIssuer,
                   ValidateIssuer = true,
                   ValidateAudience = false,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(MAVClaimTypes.kMAVSecretKey))
               };
           });

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
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
                app.UseHsts();
            }
            app.UseCors(MyAllowSpecificOrigins);
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.Add(new ServiceDescriptor(typeof(IJobService), typeof(JobService), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(IWorkFlowStatusService), typeof(WorkFlowStatusService), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(IListCategoryService), typeof(ListCategoryService), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(IJobTypeService), typeof(JobTypeService), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(ICategoryService), typeof(CategoryService), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(IRegistrationJobService), typeof(RegistrationJobService), ServiceLifetime.Transient));
        }
    }
}
