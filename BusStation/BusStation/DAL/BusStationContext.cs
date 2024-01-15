using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusStation.MODEL;

namespace BusStation.DAL
{
    public class BusStationContext : DbContext
    {
        public DbSet<Passenger> Passengers { get; set; } = null;
        public DbSet<Bus> Buses { get; set; } = null;
        public DbSet<Route> Routes { get; set; } = null;
        public DbSet<Run> Runs { get; set; } = null;

        public BusStationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();

            Bus Bus1 = new Bus()
            {
                Model = "ПАЗ-3205",
                RegNum = "в527rus36"
            };
            Bus Bus2 = new Bus()
            {
                Model = "АКСМ-420",
                RegNum = "а861rus36"
            };
            Route Route1 = new Route()
            {
                Name = "ВАИ - Кольцовская",
                StopsNum = 12
            };
            Route Route2 = new Route()
            {
                Name = "ВГУ - Памятник Славы",
                StopsNum = 16
            };
            Run Run1 = new Run()
            {
                Name = "Рейс 1",
                BusId = 1,
                RouteId = 1,
                DepartureDate = new DateTime(2024, 1, 13),
                ArrivalDate = new DateTime(2024, 1, 13)
            };
            Run Run2 = new Run()
            {
                Name = "Рейс 2",
                BusId = 2,
                RouteId = 2,
                DepartureDate = new DateTime(2024, 01, 13),
                ArrivalDate = new DateTime(2024, 01, 13)
            };
            Passenger Passenger1 = new Passenger()
            {
                FullName = "Иван Иванов",
                IdType = "Паспорт",
                IdNum = "2001 365-102",
            };
            Passenger Passenger2 = new Passenger()
            {
                FullName = "Роман Абрамович ",
                IdType = "Паспорт",
                IdNum = "1966 712-105",
            };
            Passenger Passenger3 = new Passenger()
            {
                FullName = "Павел Дуров",
                IdType = "Паспорт",
                IdNum = "1984 700-905",
            };

            Buses.AddRange(Bus1, Bus2);
            Routes.AddRange(Route1, Route2);
            Passengers.AddRange(Passenger1, Passenger2, Passenger3);
            Runs.AddRange(Run1, Run2);

            Passenger1.Runs.AddRange(new List<Run>() { Run1});
            Passenger2.Runs.AddRange(new List<Run>() { Run2 });



            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=BusStationDb;Trusted_Connection=True");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}
