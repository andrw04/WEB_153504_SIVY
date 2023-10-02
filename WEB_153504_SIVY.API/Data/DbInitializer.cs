using Microsoft.EntityFrameworkCore;
using WEB_153504_SIVY.Domain.Entities;

namespace WEB_153504_SIVY.API.Data
{
    public class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureDeleted();
            context.Database.Migrate();

            await Task.Run(() =>
            {
                context.CarBodyTypes.AddRange(new List<CarBodyType>()
                {
                    new CarBodyType() { Name = "Sedan", NormalizedName = "sedan" },
                    new CarBodyType() { Name = "Hatchback", NormalizedName = "hatchback" },
                    new CarBodyType() { Name = "Station wagon", NormalizedName = "station-wagon" },
                    new CarBodyType() { Name = "Pickup truck", NormalizedName = "pickup-truck"},
                    new CarBodyType() { Name = "Van", NormalizedName = "van" },
                    new CarBodyType() { Name = "Cabriolet", NormalizedName = "cabriolet" },
                    new CarBodyType() { Name = "Off-road car", NormalizedName = "off-road-car" },
                });

                context.SaveChanges();

                CarBodyType? GetCarBodyType(string name) => context.CarBodyTypes.First(car => car.NormalizedName == name);
                string url = app.Configuration.GetValue<string>("applicationUrl");

                context.CarModels.AddRange(new List<CarModel>()
                {
                    new CarModel()
                    {
                        Name = "Bwm 3 Series",
                        Price = 50000,
                        Description = "The BMW 3 Series is a line of compact executive cars manufactured by the German automaker BMW since May 1975.",
                        CarBody = GetCarBodyType("sedan"),
                        Image = $"{url}/Images/Bmw3-series.png",
                    },
                    new CarModel()
                    {
                        Name = "Mercedes-Benz C-class",
                        Price = 45000,
                        Description = "The Mercedes-Benz C-Class is a series of compact executive cars produced by Mercedes-Benz Group AG.",
                        CarBody = GetCarBodyType("sedan"),
                        Image = $"{url}/Images/Mercedes-BenzC-class.png",
                    },
                    new CarModel()
                    {
                        Name = "Audi A6",
                        Price = 52000,
                        Description = "The Audi A6 is an executive car made by the German automaker Audi.",
                        CarBody = GetCarBodyType("sedan"),
                        Image = $"{url}/Images/AudiA6.png",
                    },
                    new CarModel()
                    {
                        Name = "Audi A4",
                        Price = 30000,
                        Description = "The Audi A4 is a line of luxury compact executive cars produced since 1994 by the German car manufacturer Audi, a subsidiary of the Volkswagen Group.",
                        CarBody = GetCarBodyType("sedan"),
                        Image = $"{url}/Images/AudiA4.png",
                    },
                    new CarModel()
                    {
                        Name = "RAM 1500",
                        Price = 67000,
                        Description = "The Ram 1500 can tow up to 12,750 pounds and carry a payload of up to 2320 pounds.",
                        CarBody = GetCarBodyType("pickup-truck"),
                        Image = $"{url}/Images/RAM1500.png",
                    },
                    new CarModel()
                    {
                        Name = "Volkswagen Golf",
                        Price = 30000,
                        Description = "Despite a small and underpowered turbo-four engine, it boasts an excellent six-speed manual or eight-speed automatic transmission.",
                        CarBody = GetCarBodyType("hatchback"),
                        Image = $"{url}/Images/VolkswagenGolf.png"
                    },
                    new CarModel()
                    {
                        Name = "Renault Megane",
                        Price = 15000,
                        Description = "The Renault Megane won't wow you with a sporty drive, but you'll be impressed by how comfortable it is.",
                        CarBody = GetCarBodyType("station-wagon"),
                        Image = $"{url}/Images/RenaultMegane.png"
                    },
                    new CarModel()
                    {
                        Name = "Citroen Jumper",
                        Price = 20000,
                        Description = "A light commercial van jointly developed by Fiat Group and PSA Group (currently Stellantis)",
                        CarBody = GetCarBodyType("van"),
                        Image = $"{url}/Images/CitroenJumper.png"
                    }
                });
                context.SaveChanges();

            });
        }
    }
}
