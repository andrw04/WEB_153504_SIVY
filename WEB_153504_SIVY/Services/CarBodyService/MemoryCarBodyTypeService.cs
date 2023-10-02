using WEB_153504_SIVY.Domain.Entities;
using WEB_153504_SIVY.Domain.Models;

namespace WEB_153504_SIVY.Services.CarBodyService
{
    public class MemoryCarBodyTypeService : ICarBodyTypeService
    {
        public Task<ResponseData<List<CarBodyType>>> GetCarBodyTypeListAsync()
        {
            var categories = new List<CarBodyType>()
            {
                new CarBodyType() { Id = 1, Name = "Sedan", NormalizedName = "sedan" },
                new CarBodyType() { Id = 2, Name = "Hatchback", NormalizedName = "hatchback" },
                new CarBodyType() { Id = 3, Name = "Station wagon", NormalizedName = "station-wagon" },
                new CarBodyType() { Id = 4, Name = "Pickup truck", NormalizedName = "pickup-truck"},
                new CarBodyType() { Id = 5, Name = "Van", NormalizedName = "van" },
                new CarBodyType() { Id = 6, Name = "Cabriolet", NormalizedName = "cabriolet" },
                new CarBodyType() { Id = 7, Name = "Off-road car", NormalizedName = "off-road-car" },
            };

            var result = new ResponseData<List<CarBodyType>>();
            result.Data = categories;
            
            return Task.FromResult(result);
        }
    }
}
