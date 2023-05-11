using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSyaPatientManagement.DL.Entities;
using eSyaPatientManagement.DL.Repository;
using eSyaPatientManagement.IF;
using eSyaPatientManagement.WebAPI.Extention;
using eSyaPatientManagement.WebAPI.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eSyaPatientManagement.WebAPI
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
          
            services.AddMvc(options =>
            {
               // options.Filters.Add(typeof(HttpAuthAttribute));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

         
            services.AddDbContext<eSyaEnterpriseContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("dbContext")));

            services.AddScoped<IMasterRepository, MasterRepository>();
            services.AddScoped<IOpClinicDetailsRepository, OpClinicDetailsRepository>();
            services.AddScoped<IOpRegistrationBillingRepository, OpRegistrationBillingRepository>();
            services.AddScoped<IPatientInfoRepository, PatientInfoRepository>();
            services.AddScoped<IBillingTransactionRepository, BillingTransactionRepository>();
            services.AddScoped<IReceiptTransactionRepository, ReceiptTransactionRepository>();
            services.AddScoped<IReceiptMasterRepository, ReceiptMasterRepository>();
            services.AddScoped<IServiceRatesRepository, ServiceRatesRepository>();
            services.AddScoped<IOpBillingRepository, OpBillingRepository>();
            services.AddScoped<ISuspendOpBillingRepository, SuspendOpBillingRepository>();
            services.AddScoped<IOpPatientVisitDetailRepository, OpPatientVisitDetailRepository>();
            services.AddScoped<IDoctorDeskRepository, DoctorDeskRepository>();
            services.AddScoped<IClinicalFormsRepository, ClinicalFormsRepository>();
            services.AddScoped<IPatientTypesRepository, PatientTypesRepository>();
            services.AddScoped<ICareCardRatesRepository, CareCardRatesRepository>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
