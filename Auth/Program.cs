
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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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