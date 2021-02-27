namespace FS.Api
{
    using System;
    using System.Text;
    using Domain.Core.Interfaces.Services;
    using Domain.Core.Services;
    using FS.Data.Repositories;
    using FS.Domain.Core.Interfaces;
    using MediatR;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using Domain.Model;
    using Domain.Model.Validators;
    using FluentValidation;
    using FluentValidation.AspNetCore;
    using Hangfire;
    using Hangfire.SqlServer;
    using Newtonsoft.Json.Converters;

    public class Startup
    {
        public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
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

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Ex: Bearer {token here}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                        },
                        new string[] { }
                    }
                });
            });

            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("DatabaseConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));
            
            services.AddHangfireServer();

            services.AddMvc()
                .AddFluentValidation();

            services.AddMediatR(typeof(Startup));

            services.AddTransient<IValidator<User>, UserValidator>();
            services.AddTransient<IValidator<Account>, AccountValidator>();
            services.AddTransient<IValidator<Movement>, MovementValidator>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IMovementRepository, MovementRepository>();
            services.AddTransient<IBalanceRepository, BalanceRepository>();
            services.AddTransient<IInstallmentMovementRepository, InstallmentMovementRepository>();
            services.AddTransient<IInvestmentRepository, InvestmentRepository>();
            services.AddTransient<IInvestmentBalanceRepository, InvestmentBalanceRepository>();

            services.AddTransient<IUserAccountService, CreateUserService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IInvestmentService, InvestmentService>();

            services.AddControllers()
                .AddNewtonsoftJson((options => 
                    options.SerializerSettings.Converters.Add(new StringEnumConverter())));
            services.AddSwaggerGenNewtonsoftSupport();
            
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

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseCustomExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });
        }
    }
}