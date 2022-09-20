using CoffeeShopAPI.Config;
using CoffeeShopAPI.Contracts;
using CoffeeShopAPI.Data;
using CoffeeShopAPI.IRepository;
using CoffeeShopAPI.Middleware;
using CoffeeShopAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace CoffeeShopAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Add Connection to the database
            var connectionString = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // Add logging into the project
            builder.Host.UseSerilog(
                (ctx, lc) =>
                    lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

            //var key = builder.Configuration["SecretKey"];

            // Adding AutoMapper
            builder.Services.AddAutoMapper(typeof(MapperConfig));

            // Identity provider

            builder.Services.AddIdentityCore<Employee>()
                .AddTokenProvider<DataProtectorTokenProvider<Employee>>("CoffeeShop")
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            // Repository injections
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IAuthManager, AuthManager>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowAll",
                                  policy =>
                                  {
                                      policy.WithOrigins("*")
                                      .AllowAnyHeader().AllowAnyMethod();
                                  });
            });

            // Add Jwt
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding
                        .UTF8
                        .GetBytes(builder.Configuration["JwtSettings:Key"]))
                };
            });

            // services.AddResponseCaching();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Adding Versioning
            builder.Services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0); // 1.0, or use constant ApiVersion.Default, its 1.0
                                                                  //options.ApiVersionReader = new MediaTypeApiVersionReader("version");
                                                                  // To get value for version through accept header, default is query string
                                                                  //options.ApiVersionReader = new HeaderApiVersionReader("Version"); // custom header name
                /*                options.ApiVersionReader = ApiVersionReader.Combine(
                                    new MediaTypeApiVersionReader("version"),
                                    new HeaderApiVersionReader("X-Version")
                                    );*/ // To combine both
                                         //options.ReportApiVersions = true; // To show in response headers which version are supported by an API
                                         // Its even possible to specifify controller inhere and say which version it supports and which actions supports which version
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Use middleware for exceptions
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}