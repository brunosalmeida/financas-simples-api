using System;
using System.Text;
using FS.Data.Repositories;
using FS.Domain.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FS.Api
{
    using Domain.Model;
    using Domain.Model.Validators;
    using FluentValidation;
    using FluentValidation.AspNetCore;

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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Finanças Simples",
                    Description = "Finanças Simples API",
                    Contact = new OpenApiContact
                    {
                        Name = "Bruno Almeida",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/brunosalmeida"),
                    }
                });
            });
            
            services.AddMvc()
                .AddFluentValidation();
            
            services.AddAuthentication
                    (JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey
                            (Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });
            
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<DatabaseContext>(opt => 
                    opt.UseNpgsql(Configuration.GetConnectionString("DatabaseConnection")));

            services.AddMediatR(typeof(Startup));

            services.AddTransient<IValidator<User>, UserValidator>();
            services.AddTransient<IValidator<Account>, AccountValidator>();
            services.AddTransient<IValidator<Expense>, ExpenseValidator>();
                
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            
            services.AddSingleton<IConfiguration>(Configuration);
            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Finanças Simples API");
            });

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}