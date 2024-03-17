
using Auth.Models;
using Auth.Repository.IRepository;
using Auth.Services.IService;
using Auth.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using Auth.Service;
using Auth.Repository;
using Auth.Data;


namespace Auth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
            builder.Services.AddControllers();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
          .AddDefaultTokenProviders();
            builder.Services.AddControllers();
            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddHttpClient("PhoneCheck", u => u.BaseAddress = new Uri(builder.Configuration["ValideUser:PhoneAPI"]));
            builder.Services.AddHttpClient("MailCheck", u => u.BaseAddress = new Uri(builder.Configuration["ValideUser:EmailAPI"]));
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddCors(p => p.AddPolicy(MyAllowSpecificOrigins, builder =>
            {
                builder.WithOrigins("http://localhost:3000", "http://localhost:3001", "http://localhost:3002", "http://localhost:3003", "https://artvistaauthapi.azurewebsites.net/", "https://artvista-website.vercel.app/")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials(); // Add this line to allow credentials

                // Other configurations...
            }));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                if (!app.Environment.IsDevelopment())
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AUTH API");
                    c.RoutePrefix = string.Empty;
                }
            });
          
            app.UseHttpsRedirection();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthorization();

            app.MapControllers();
             //ApplyMigration();
            app.Run();


            //void ApplyMigration()
            //{
            //    using (var scope = app.Services.CreateScope())
            //    {
            //        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            //        if (_db.Database.GetPendingMigrations().Count() > 0)
            //        {
            //            _db.Database.Migrate();
            //        }
            //    }
            //}
        }
    }
}