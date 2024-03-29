
using AutoMapper;
using Management.AppHub;
using Management.Data;
using Management.Extension;
using Management.Models;
using Management.Repository;
using Management.Repository.IRepository;
using Management.Services;
using Management.Services.IService;
using Management.Util;
using Management.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Management
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ArtworkSharingPlatformContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ArtworkSharingPlatformContext>()
    .AddDefaultTokenProviders();
            // Add services to the container.
            IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
            builder.Services.AddSingleton(mapper);
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<BackendApiAuthenthicationHttpClientHandler>();

            builder.Services.AddScoped<IArtworkRepository, ArtworkRepository>();
            builder.Services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            builder.Services.AddScoped<IInteractionRepository, InteractionRepository>();
            builder.Services.AddScoped<IPackageOfCreatorRepository, PackageOfCreatorRepository>();
            builder.Services.AddScoped<IPackageRepository, PackageRepository>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IReportRepository, ReportRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();



            builder.Services.AddScoped<IArtworkService, ArtworkService>();
            builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
            builder.Services.AddScoped<IInteractionService, InteractionService>();
            builder.Services.AddScoped<IPackageOfCreatorService, PackageOfCreatorService>();
            builder.Services.AddScoped<IPackageService, PackageService>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<IReportService, ReportService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IUserServices, UserServices>();



            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter string as follow : Bearer Generated-JWT-Token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    }, new string[]{}
                    }
                });
            }
           );
            builder.AppAuthentication();
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddCors(p => p.AddPolicy(MyAllowSpecificOrigins, builder =>
            {
                builder.WithOrigins("http://localhost:3000", "http://localhost:3001", "http://localhost:3002", "http://localhost:3003", "https://artvistamanagementapi.azurewebsites.net/", "https://artvista-website.vercel.app/").AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials(); // Add this line to allow credentials

                // Other configurations...
            }));

            builder.Services.AddAuthorization(options => {

                options.AddPolicy("ARTWORKMANAGEMENT", policy =>
                {
                    policy.RequireRole(SD.ADMIN, SD.MODERATOR, SD.CREATOR);
                });

                options.AddPolicy("ORGANIZATION", policy =>
                {
                    policy.RequireRole(SD.ADMIN, SD.MODERATOR);
                });

                options.AddPolicy("CUSTOMER_USER", policy =>
                {
                    policy.RequireRole(SD.CUSTOMER, SD.CREATOR);
                });
            });
            builder.Services.AddSignalR();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                if (!app.Environment.IsDevelopment())
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MANAGEMENT API");
                    c.RoutePrefix = string.Empty;
                }
            });


            app.UseHttpsRedirection();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();

            // Hub
            app.MapHub<UserStatusHub>("/appHub/userStatusHub");

            app.MapControllers();

            app.Run();
        }
    }
}
