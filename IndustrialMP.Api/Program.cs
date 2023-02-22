using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using IndustrialMP.Api.Data;

namespace IndustrialMP.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ApiContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ApiContext") ?? throw new InvalidOperationException("Connection string 'ApiContext' not found.")));

            // Add services to the container.

            builder.Services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("https://localhost:7298/").AllowAnyHeader().AllowAnyOrigin();
                });

            }                
            );

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}