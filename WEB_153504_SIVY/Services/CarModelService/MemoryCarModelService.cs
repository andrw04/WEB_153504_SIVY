using WEB_153504_SIVY.Domain.Entities;
using WEB_153504_SIVY.Domain.Models;
using WEB_153504_SIVY.Services.CarBrandService;

namespace WEB_153504_SIVY.Services.CarModelService
{
    public class MemoryCarModelService : ICarModelService
    {
        private List<CarModel> carModels = null;
        private List<CarBrand> carBrands = null;

        public MemoryCarModelService(ICarBrandService carBrandService)
        {
            carBrands = carBrandService.GetCarBrandListAsync().Result.Data;
        }

        public void SetupData()
        {
            CarBrand? GetCarBrand(string name) => carBrands.Find(cb => cb.NormalizedName.Equals(name));

            carModels = new List<CarModel>()
            {
                new CarModel()
                {
                    Id = 1,
                    Name = "",
                    Price = 50000,
                    Description = "",
                    CarBrand = GetCarBrand("bmw")
                },
                new CarModel()
                {
                    Id = 2,
                    Name = "",
                    Price = 55000,
                    Description = "",
                    CarBrand = GetCarBrand("mercedes-benz")
                },
                new CarModel()
                { 
                    Id = 3,
                    Name = "",
                    Price = 60000,
                    Description = "",
                    CarBrand = GetCarBrand("audi") },
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

        public Task<ResponseData<List<CarModel>>> GetCarModelListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var result = new ResponseData<List<CarModel>>();
            result.Data = carModels;

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
