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
             * singelton : uygulama �lk ayaga kalkt�g� zaman serv�s�n tek b�r �nstance � olusturulur. memory de tutulur her cagr�ld�g�nda memory dek� �nstance gonder�l�r
             * 
             * scoped : her request �c�n b�r �nstance yarat�l�r
             * 
             * transient : her serv�s cagr�ld�g�nda yen� b�r �nstance olusturulur
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