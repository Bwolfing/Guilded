﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Selama_SPA.Common;
using Selama_SPA.Data;
using Selama_SPA.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Selama_SPA.Data.DAL;
using Selama_SPA.Data.DAL.Home;
using Selama_SPA.Data.Models.Home;
using BattleNetApi.Apis.Interfaces;
using Selama_SPA.Options;
using Microsoft.IdentityModel.Tokens;
using Selama_SPA.Services;

namespace Selama_SPA.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddSelama(this IServiceCollection services, IConfigurationRoot Configuration)
        {
            services.AddSelamaDb(Configuration);
            services.AddSelamaDAL(Configuration);
            services.AddSelamaOptions(Configuration);

            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        private static void AddSelamaDb(this IServiceCollection services, IConfigurationRoot Configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                if (Globals.OSX)
                {
                    options.UseSqlite(Configuration.GetConnectionString("DefaultConnection:OSX"));
                }
                else
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection:Windows"));
                }
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }

        private static void AddSelamaDAL(this IServiceCollection services, IConfigurationRoot Configuration)
        {
            services.AddTransient<IReadOnlyRepository<GuildActivity>, GuildActivityRepo>();
            services.AddSingleton<IBattleNetApi>(implementationInstance:
                new BattleNetApi.Apis.BattleNetApi(Configuration["OAuthProviders:BattleNetClientId"])
            );
            services.AddTransient<IGuildActivityReadOnlyDataContext, GuildActivityReadOnlyDataContext>();
        }
    
        private const string SECRET_KEY = "SampleNotSoSecretKey";
        private static readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SECRET_KEY));

        private static void AddSelamaOptions(this IServiceCollection services, IConfigurationRoot Configuration)
        {
            services.Configure<JwtOptions>(options =>
            {
                var optionSettings = Configuration.GetSection("JwtOptions");
                options.Issuer = optionSettings["Issuer"];
                if (Globals.OSX)
                {
                    options.Audience = optionSettings["Audience:OSX"];
                }
                else
                {
                    options.Audience = optionSettings["Audience:Windows"];
                }
            });
        }
    }
}