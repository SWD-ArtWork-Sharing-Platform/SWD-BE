
using AutoMapper;
using Market.Data;
using Market.Models;
using Market.Services.IServices;
using Market.Services;
using Market.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Management.Util;
using Market.Extension;
using Market.Repository.IRepository;
using Market.Repository;

namespace Market
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

            builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            builder.Services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();




            builder.Services.AddScoped<IImageProcessingService, ImageProcessingService>();
            builder.Services.AddScoped<IArtworkServices, ArtworkServices>();
            builder.Services.AddScoped<IOrderService, OrderServices>();
            builder.Services.AddScoped<IPackageServices, PackageServices>();
            builder.Services.AddScoped<IVnPayService, VnPayService>();
            builder.Services.AddScoped<IWishListServices, WishListServices>();
            builder.Services.AddScoped<IBankAccountService, BankAccountService>();

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
                builder.WithOrigins("http://localhost:3000", "http://localhost:3001", "http://localhost:3002", "http://localhost:3003", "https://artvistamarketapi.azurewebsites.net/", "https://artvista-website.vercel.app/")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials(); // Add this line to allow credentials

                // Other configurations...
            }));

            //builder.Services.AddAuthorization(options =>
            //{

            //    options.AddPolicy("ARTWORKMANAGEMENT", policy =>
            //    {
            //        policy.RequireRole(SD.ADMIN);
            //        policy.RequireRole(SD.MODERATOR);
            //        policy.RequireRole(SD.CREATOR);
            //    });

            //    options.AddPolicy("ORGANIZATION", policy =>
            //    {
            //        policy.RequireRole(SD.ADMIN);
            //        policy.RequireRole(SD.MODERATOR);
            //    });

            //    options.AddPolicy("CUSTOMER_USER", policy =>
            //    {
            //        policy.RequireRole(SD.CUSTOMER);
            //        policy.RequireRole(SD.CREATOR);
            //    });
            //});

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                if (!app.Environment.IsDevelopment())
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MARKET API");
                    c.RoutePrefix = string.Empty;
                }
            });

           
            app.UseHttpsRedirection();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

