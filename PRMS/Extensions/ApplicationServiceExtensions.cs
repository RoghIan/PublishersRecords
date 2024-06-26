﻿using API.Data.Repository;
using API.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using PRMS.Data;
using PRMS.Data.Repository;
using PRMS.Helpers;
using PRMS.Interfaces;

namespace PRMS.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IPublihserRepository, PublisherRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IAppointedRepository, AppointedRepository>();
            services.AddAutoMapper(typeof(AutomapperProfiles).Assembly);
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                    o => o
                    .MaxBatchSize(1000));
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(config.GetSection("AzureAd"));

            return services;
        }
    }
}
