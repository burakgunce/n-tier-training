using Microsoft.EntityFrameworkCore;
using MusicMarket.Core;
using MusicMarket.Core.Services;
using MusicMarket.Data;
using MusicMarket.Services;

namespace MusicMarket.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            /*
             * singelton : uygulama ýlk ayaga kalktýgý zaman servýsýn tek býr ýnstance ý olusturulur. memory de tutulur her cagrýldýgýnda memory deký ýnstance gonderýlýr
             * 
             * scoped : her request ýcýn býr ýnstance yaratýlýr
             * 
             * transient : her servýs cagrýldýgýnda yený býr ýnstance olusturulur
             */

            builder.Services.AddDbContext<MusicMarketDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr"), x => x.MigrationsAssembly("MusicMarket.Data"));
            });

            builder.Services.AddTransient<IMusicService, MusicService>();
            builder.Services.AddTransient<IArtistService, ArtistService>();

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