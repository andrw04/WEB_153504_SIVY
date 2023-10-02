using WEB_153504_SIVY.Domain.Entities;
using WEB_153504_SIVY.Domain.Models;
using WEB_153504_SIVY.Services.CarBodyService;

namespace WEB_153504_SIVY.Services.CarModelService
{
    public class MemoryCarModelService : ICarModelService
    {
        private List<CarModel> carModels = null;
        private List<CarBodyType> carBrands = null;
        private IConfiguration config;

        public MemoryCarModelService(IConfiguration config, ICarBodyTypeService carBodyTypeService)
        {
            this.config = config;
            carBrands = carBodyTypeService.GetCarBodyTypeListAsync().Result.Data;

            SetupData();
        }

        public void SetupData()
        {
            CarBodyType? GetCarBodyType(string name) => carBrands.Find(cb => cb.NormalizedName.Equals(name));

            carModels = new List<CarModel>()
            {
                new CarModel()
                {
                    Id = 1,
                    Name = "Bwm 3 Series",
                    Price = 50000,
                    Description = "The BMW 3 Series is a line of compact executive cars manufactured by the German automaker BMW since May 1975.",
                    CarBody = GetCarBodyType("sedan"),
                    Image = "Images/Bmw3-series.png",
                },
                new CarModel()
                {
                    Id = 2,
                    Name = "Mercedes-Benz C-class",
                    Price = 45000,
                    Description = "The Mercedes-Benz C-Class is a series of compact executive cars produced by Mercedes-Benz Group AG.",
                    CarBody = GetCarBodyType("sedan"),
                    Image = "Images/Mercedes-BenzC-class.png",
                },
                new CarModel()
                {
                    Id = 3,
                    Name = "Audi A6",
                    Price = 52000,
                    Description = "The Audi A6 is an executive car made by the German automaker Audi.",
                    CarBody = GetCarBodyType("sedan"),
                    Image = "Images/AudiA6.png",
                },
                new CarModel()
                {
                    Id = 4,
                    Name = "Audi A4",
                    Price = 30000,
                    Description = "The Audi A4 is a line of luxury compact executive cars produced since 1994 by the German car manufacturer Audi, a subsidiary of the Volkswagen Group.",
                    CarBody = GetCarBodyType("sedan"),
                    Image = "Images/AudiA4.png",
                },
                new CarModel()
                {
                    Id = 5,
                    Name = "RAM 1500",
                    Price = 67000,
                    Description = "The Ram 1500 can tow up to 12,750 pounds and carry a payload of up to 2320 pounds.",
                    CarBody = GetCarBodyType("pickup-truck"),
                    Image = "Images/RAM1500.png",
                },
                new CarModel()
                {
                    Id = 6,
                    Name = "Volkswagen Golf",
                    Price = 30000,
                    Description = "Despite a small and underpowered turbo-four engine, it boasts an excellent six-speed manual or eight-speed automatic transmission.",
                    CarBody = GetCarBodyType("hatchback"),
                    Image = "Images/VolkswagenGolf.png"
                },
                new CarModel()
                {
                    Id = 7,
                    Name = "Renault Megane",
                    Price = 15000,
                    Description = "The Renault Megane won't wow you with a sporty drive, but you'll be impressed by how comfortable it is.",
                    CarBody = GetCarBodyType("station-wagon"),
                    Image = "Images/RenaultMegane.png"
                },
                new CarModel()
                {
                    Id = 8,
                    Name = "Citroen Jumper",
                    Price = 20000,
                    Description = "A light commercial van jointly developed by Fiat Group and PSA Group (currently Stellantis)",
                    CarBody = GetCarBodyType("van"),
                    Image = "Images/CitroenJumper.png"
                }
            };
        }

        public Task<ResponseData<CarModel>> CreateCarModelAsync(CarModel carModel, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCarModelAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<CarModelListModel<CarModel>>> GetCarModelListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            int pageSize = (int)config.GetValue(typeof(int), "ItemsPerPage");

            var itemsList = carModels
                .Where(x => categoryNormalizedName == null ||
                x.CarBody.NormalizedName.Equals(categoryNormalizedName));
            int totalPageCount = (itemsList.Count() + pageSize - 1) / pageSize;

            var result = new ResponseData<CarModelListModel<CarModel>>()
            {
                Data = new CarModelListModel<CarModel>()
                {
                    Items = itemsList.Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .ToList()
                },
            };


            result.Data.CurrentPage = pageNo;
            result.Data.TotalPages = totalPageCount;

            return Task.FromResult(result);
        }

        public Task<ResponseData<List<CarModel>>> GetCarModelByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCarModelAsync(int id, CarModel carModel, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}
