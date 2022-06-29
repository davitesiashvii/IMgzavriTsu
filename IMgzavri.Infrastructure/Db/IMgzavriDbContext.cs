using IMgzavri.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Infrastructure.Db
{
    public class IMgzavriDbContext : DbContext
    {

        public IMgzavriDbContext(DbContextOptions<IMgzavriDbContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlServer(Configuration.GetConnectionString("IMgzavriDbContext"));
        //}

        public DbSet<Users> Users { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<CarMarck> CarMarcks { get; set; }

        public DbSet<CarModel> CarModels { get; set; }

        public DbSet<CarImage> CarImages { get; set; }

        public DbSet<Statement> Statements { get; set; }

        public DbSet<IMgzavri.Domain.Models.File> Files { get; set; }

        public DbSet<City> Cities { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //User
            modelBuilder.Entity<RefreshToken>()
                .HasOne(x=>x.User)
                .WithMany(x=> x.RefreshTokens)
                .HasForeignKey(x=>x.UserId);

            modelBuilder.Entity<Car>()
                .HasOne(x => x.User)
                .WithMany(x => x.Cars)
                .HasForeignKey(x => x.UserId); 

            //car
            modelBuilder.Entity<CarImage>()
                .HasOne(x => x.Car)
                .WithMany(x => x.CarImages)
                .HasForeignKey(x => x.CarId);

            modelBuilder.Entity<Statement>()
                .HasOne(x => x.CreateUser)
                .WithMany(x => x.Statements)
                .HasForeignKey(x => x.CreateUserId);

                 

        }
    }
}
