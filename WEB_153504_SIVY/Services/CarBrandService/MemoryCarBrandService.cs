using WEB_153504_SIVY.Domain.Entities;
using WEB_153504_SIVY.Domain.Models;

namespace WEB_153504_SIVY.Services.CarBrandService
{
    public class MemoryCarBrandService : ICarBrandService
    {
        public Task<ResponseData<List<CarBrand>>> GetCarBrandListAsync()
        {
            var categories = new List<CarBrand>()
            {
                new CarBrand() { Id = 1, Name = "BWM", NormalizedName = "bmw" },
                new CarBrand() { Id = 2, Name = "Mercedes-Benz", NormalizedName = "mercedes-benz" },
                new CarBrand() { Id = 3, Name = "Audi", NormalizedName = "audi" },
            };

            var result = new ResponseData<List<CarBrand>>();
            result.Data = categories;
            
            return Task.FromResult(result);
        }
    }
}
