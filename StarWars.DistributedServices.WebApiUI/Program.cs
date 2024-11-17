using Microsoft.EntityFrameworkCore;
using StarWars.Infrastructure.Contracts;
using StarWars.Infrastructure.Impl;
using StarWars.Infrastructure.Impl.DbContext;
using StarWars.Library.Contracts;
using StarWars.Library.Impl;

namespace StarWars.DistributedServices.WebApiUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services
               .AddScoped<IPeopleApiRepository, PeopleApiRepository>()
              .AddScoped<IPlanetsDBRepository, PlanetsDBRepository>()
              .AddScoped<IPlanetsApiRepository, PlanetsApiRepository>()
              .AddScoped<IPlanetsService, PlanetsService>()
              .AddScoped<IResidentsService, ResidentsService>();

            builder.Services.AddDbContext<SWDBContext>(options =>
            options.UseSqlServer("Data Source=074BCN2024\\SQLEXPRESS;Initial Catalog=SWDB;User ID=adria;Password=1234;Trust Server Certificate=True"));

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

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
