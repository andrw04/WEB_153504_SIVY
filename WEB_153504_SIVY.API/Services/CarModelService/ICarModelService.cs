using WEB_153504_SIVY.Domain.Entities;
using WEB_153504_SIVY.Domain.Models;

namespace WEB_153504_SIVY.API.Services.CarModelService
{
    public interface ICarModelService
    {
        public Task<ResponseData<CarModelListModel<CarModel>>> GetCarModelListAsync(string? normalizedName, int pageNo, int pageSize = 3);

        public Task UpdateCarModelAsync(int id, CarModel carModel);

        public Task<ResponseData<CarModel>> CreateCarModelAsync(CarModel carModel);


        public Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile);
    }
}
