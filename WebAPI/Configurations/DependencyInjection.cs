using Adapters.Controllers;
using Adapters.Controllers.Interfaces;
using Adapters.Gateways;
using Adapters.Gateways.Interfaces;
using Adapters.Validators;
using Application.UseCases;
using DataSource.Context;
using DataSource.Repositories;
using DataSource.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.Interfaces;
using Application.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace WebAPI.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraStructure(this IServiceCollection Services, IConfiguration configuration)
        {

            #region conexões
            var mySqlConnectionString = configuration.GetConnectionString("DefaultConnection");
            /* serviços de banco de dados MySql  */
            
            Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(mySqlConnectionString,ServerVersion.AutoDetect(mySqlConnectionString))
               .UseLoggerFactory(
                   LoggerFactory.Create(
                       b => b
                           .AddConsole()
                           .AddFilter(level => level >= LogLevel.Information)))
               .EnableSensitiveDataLogging()
               .EnableDetailedErrors();
            });

            #endregion

            /* ***** serviços de acesso a base ***** */
            Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString)));
            Services.AddScoped<IDataSource, DataSource.DataSource>();


            /* ***** serviços de orquestração ***** */
            Services.AddScoped<IUsuarioController, UsuarioController>();
            Services.AddScoped<IAutenticacaoController, AutenticacaoController>(); 

            /* ***** serviços de acesso a dados ***** */
            Services.AddScoped<IUsuarioGateway, UsuarioGateway>();


            /* ***** serviços de negocio ***** */
            Services.AddScoped<IUsuarioUseCase, UsuarioUseCase>();
            Services.AddScoped<IAutenticacaoUseCase, AutenticacaoUseCase>(); 

            Services.AddScoped<IUsuarioRepository, UsuarioRepository>();          
          
            return Services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddFluentValidationAutoValidation();
            Services.AddValidatorsFromAssemblyContaining<UsuarioRequestValidator>();
            
            return Services;
        }

        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configurar JWT Settings
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

            // Configurar a autenticação e autorização
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Registrar AutenticacaoUseCase
            services.AddScoped<IAutenticacaoUseCase, AutenticacaoUseCase>();

            return services;
        }



    }


}