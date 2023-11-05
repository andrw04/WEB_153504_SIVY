using Castle.Core.Configuration;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using WEB_153504_SIVY.API.Data;
using WEB_153504_SIVY.API.Services.CarModelService;
using WEB_153504_SIVY.Domain.Entities;

namespace WEB_153504_SIVY.Test
{
    public class CarModelServiceTests : IDisposable
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<ApplicationDbContext> _contextOptions;

        #region CtorAndDispose
        public CarModelServiceTests()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(_connection)
                .Options;

            using var context = new ApplicationDbContext(_contextOptions);

            #region AddData

            var carBodies = new List<CarBodyType>
            {
                new CarBodyType() { Name = "Sedan", NormalizedName = "sedan" },
                new CarBodyType() { Name = "Hatchback", NormalizedName = "hatchback" },
                new CarBodyType() { Name = "Station wagon", NormalizedName = "station-wagon" },
                new CarBodyType() { Name = "Pickup truck", NormalizedName = "pickup-truck"},
                new CarBodyType() { Name = "Van", NormalizedName = "van" },
                new CarBodyType() { Name = "Cabriolet", NormalizedName = "cabriolet" },
                new CarBodyType() { Name = "Off-road car", NormalizedName = "off-road-car" },
            };

            context.AddRange(
                new CarModel()
                {
                    Name = "Bwm 3 Series",
                    Price = 50000,
                    Description = "The BMW 3 Series is a line of compact executive cars manufactured by the German automaker BMW since May 1975.",
                    CarBody = carBodies[0],
                    Image = $"/Images/Bmw3-series.png",
                },
                new CarModel()
                {
                    Name = "Mercedes-Benz C-class",
                    Price = 45000,
                    Description = "The Mercedes-Benz C-Class is a series of compact executive cars produced by Mercedes-Benz Group AG.",
                    CarBody = carBodies[0],
                    Image = $"/Images/Mercedes-BenzC-class.png",
                },
                new CarModel()
                {
                    Name = "Audi A6",
                    Price = 52000,
                    Description = "The Audi A6 is an executive car made by the German automaker Audi.",
                    CarBody = carBodies[0],
                    Image = $"/Images/AudiA6.png",
                },
                new CarModel()
                {
                    Name = "Audi A4",
                    Price = 30000,
                    Description = "The Audi A4 is a line of luxury compact executive cars produced since 1994 by the German car manufacturer Audi, a subsidiary of the Volkswagen Group.",
                    CarBody = carBodies[0],
                    Image = $"/Images/AudiA4.png",
                },
                new CarModel()
                {
                    Name = "RAM 1500",
                    Price = 67000,
                    Description = "The Ram 1500 can tow up to 12,750 pounds and carry a payload of up to 2320 pounds.",
                    CarBody = carBodies[3],
                    Image = $"/Images/RAM1500.png",
                },
                new CarModel()
                {
                    Name = "Volkswagen Golf",
                    Price = 30000,
                    Description = "Despite a small and underpowered turbo-four engine, it boasts an excellent six-speed manual or eight-speed automatic transmission.",
                    CarBody = carBodies[1],
                    Image = $"/Images/VolkswagenGolf.png"
                },
                new CarModel()
                {
                    Name = "Renault Megane",
                    Price = 15000,
                    Description = "The Renault Megane won't wow you with a sporty drive, but you'll be impressed by how comfortable it is.",
                    CarBody = carBodies[2],
                    Image = $"/Images/RenaultMegane.png"
                },
                new CarModel()
                {
                    Name = "Citroen Jumper",
                    Price = 20000,
                    Description = "A light commercial van jointly developed by Fiat Group and PSA Group (currently Stellantis)",
                    CarBody = carBodies[4],
                    Image = $"/Images/CitroenJumper.png"
                },
                new CarModel()
                {
                    Name = "Bwm 3 Series",
                    Price = 50000,
                    Description = "The BMW 3 Series is a line of compact executive cars manufactured by the German automaker BMW since May 1975.",
                    CarBody = carBodies[0],
                    Image = $"/Images/Bmw3-series.png",
                },
                new CarModel()
                {
                    Name = "Mercedes-Benz C-class",
                    Price = 45000,
                    Description = "The Mercedes-Benz C-Class is a series of compact executive cars produced by Mercedes-Benz Group AG.",
                    CarBody = carBodies[0],
                    Image = $"/Images/Mercedes-BenzC-class.png",
                },
                new CarModel()
                {
                    Name = "Audi A6",
                    Price = 52000,
                    Description = "The Audi A6 is an executive car made by the German automaker Audi.",
                    CarBody = carBodies[0],
                    Image = $"/Images/AudiA6.png",
                },
                new CarModel()
                {
                    Name = "Audi A4",
                    Price = 30000,
                    Description = "The Audi A4 is a line of luxury compact executive cars produced since 1994 by the German car manufacturer Audi, a subsidiary of the Volkswagen Group.",
                    CarBody = carBodies[0],
                    Image = $"/Images/AudiA4.png",
                },
                new CarModel()
                {
                    Name = "RAM 1500",
                    Price = 67000,
                    Description = "The Ram 1500 can tow up to 12,750 pounds and carry a payload of up to 2320 pounds.",
                    CarBody = carBodies[3],
                    Image = $"/Images/RAM1500.png",
                },
                new CarModel()
                {
                    Name = "Volkswagen Golf",
                    Price = 30000,
                    Description = "Despite a small and underpowered turbo-four engine, it boasts an excellent six-speed manual or eight-speed automatic transmission.",
                    CarBody = carBodies[1],
                    Image = $"/Images/VolkswagenGolf.png"
                },
                new CarModel()
                {
                    Name = "Renault Megane",
                    Price = 15000,
                    Description = "The Renault Megane won't wow you with a sporty drive, but you'll be impressed by how comfortable it is.",
                    CarBody = carBodies[2],
                    Image = $"/Images/RenaultMegane.png"
                },
                new CarModel()
                {
                    Name = "Citroen Jumper",
                    Price = 20000,
                    Description = "A light commercial van jointly developed by Fiat Group and PSA Group (currently Stellantis)",
                    CarBody = carBodies[4],
                    Image = $"/Images/CitroenJumper.png"
                },
                new CarModel()
                {
                    Name = "Bwm 3 Series",
                    Price = 50000,
                    Description = "The BMW 3 Series is a line of compact executive cars manufactured by the German automaker BMW since May 1975.",
                    CarBody = carBodies[0],
                    Image = $"/Images/Bmw3-series.png",
                },
                new CarModel()
                {
                    Name = "Mercedes-Benz C-class",
                    Price = 45000,
                    Description = "The Mercedes-Benz C-Class is a series of compact executive cars produced by Mercedes-Benz Group AG.",
                    CarBody = carBodies[0],
                    Image = $"/Images/Mercedes-BenzC-class.png",
                },
                new CarModel()
                {
                    Name = "Audi A6",
                    Price = 52000,
                    Description = "The Audi A6 is an executive car made by the German automaker Audi.",
                    CarBody = carBodies[0],
                    Image = $"/Images/AudiA6.png",
                },
                new CarModel()
                {
                    Name = "Audi A4",
                    Price = 30000,
                    Description = "The Audi A4 is a line of luxury compact executive cars produced since 1994 by the German car manufacturer Audi, a subsidiary of the Volkswagen Group.",
                    CarBody = carBodies[0],
                    Image = $"/Images/AudiA4.png",
                },
                new CarModel()
                {
                    Name = "RAM 1500",
                    Price = 67000,
                    Description = "The Ram 1500 can tow up to 12,750 pounds and carry a payload of up to 2320 pounds.",
                    CarBody = carBodies[3],
                    Image = $"/Images/RAM1500.png",
                },
                new CarModel()
                {
                    Name = "Volkswagen Golf",
                    Price = 30000,
                    Description = "Despite a small and underpowered turbo-four engine, it boasts an excellent six-speed manual or eight-speed automatic transmission.",
                    CarBody = carBodies[1],
                    Image = $"/Images/VolkswagenGolf.png"
                },
                new CarModel()
                {
                    Name = "Renault Megane",
                    Price = 15000,
                    Description = "The Renault Megane won't wow you with a sporty drive, but you'll be impressed by how comfortable it is.",
                    CarBody = carBodies[2],
                    Image = $"/Images/RenaultMegane.png"
                },
                new CarModel()
                {
                    Name = "Citroen Jumper",
                    Price = 20000,
                    Description = "A light commercial van jointly developed by Fiat Group and PSA Group (currently Stellantis)",
                    CarBody = carBodies[4],
                    Image = $"/Images/CitroenJumper.png"
                });

            context.SaveChanges();

            #endregion

        }
        ApplicationDbContext CreateContext() => new ApplicationDbContext(_contextOptions);

        public void Dispose()
        {
            _connection.Dispose();
        }

        #endregion

        [Fact]
        public void GetCarModelListReturnPageNo1Size3()
        {
            // Arange
            var context = CreateContext();
            var service = new CarModelService(context, null, null);

            // Act
            var result = service.GetCarModelListAsync(null).Result;

            // Assert
            Assert.Equal(1, result.Data.CurrentPage);
            Assert.Equal(3, result.Data.Items.Count);
            Assert.Equal(8, result.Data.TotalPages);
        }

        [Fact]
        public void GetCarModelListReturnCorrectPage()
        {
            // Arange
            var context = CreateContext();
            var service = new CarModelService(context, null, null);

            // Act
            var result = service.GetCarModelListAsync(null, 2).Result;

            // Assert
            Assert.Equal(2, result.Data.CurrentPage);
        }

        [Fact]
        public void GetCarModelListReturnCorrectFilteredData()
        {
            // Arange
            var context = CreateContext();
            var service = new CarModelService(context, null, null);

            // Act
            var result = service.GetCarModelListAsync("sedan").Result;

            // Assert
            Assert.True(result.Data.Items.All(item => item.CarBodyId == 1));
        }

        [Fact]
        public void GetCarModelListMaxPageSize()
        {
            // Arange
            var context = CreateContext();
            var service = new CarModelService(context, null, null);

            // Act
            var result = service.GetCarModelListAsync(null, pageSize: 21).Result;

            // Assert
            Assert.Equal(2, result.Data.TotalPages);
        }

        [Fact]
        public void GetCarModelListInvalidPageNoReturnSuccessFalse()
        {
            // Arange
            var context = CreateContext();
            var service = new CarModelService(context, null, null);

            // Act
            var result = service.GetCarModelListAsync(null, 100).Result;

            // Assert
            Assert.False(result.Success);
        }
    }
}
