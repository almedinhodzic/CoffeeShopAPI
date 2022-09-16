using CoffeeShopAPI.Config;
using CoffeeShopAPI.Data;
using CoffeeShopAPI.IRepository;
using CoffeeShopAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CoffeeShopAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Add Connection to the database
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            // Add logging into the project
            builder.Host.UseSerilog(
                (ctx, lc) =>
                    lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

            // Adding AutoMapper
            builder.Services.AddAutoMapper(typeof(MapperConfig));

            // Repository injections
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowAll",
                                  policy =>
                                  {
                                      policy.WithOrigins("*")
                                      .AllowAnyHeader().AllowAnyMethod();
                                  });
            });

            // services.AddResponseCaching();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}