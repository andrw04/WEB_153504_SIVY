using WEB_153504_SIVY.Domain.Entities;
using WEB_153504_SIVY.Domain.Models;

namespace WEB_153504_SIVY.Services.CarModelService
{
    public interface ICarModelService
    {
        public Task<ResponseData<CarModelListModel<CarModel>>> GetCarModelListAsync
            (string? categoryNormalizedName, int pageNo = 1);
        public Task<ResponseData<CarModel>> GetCarModelByIdAsync(int id);
        public Task UpdateCarModelAsync(int id, CarModel carModel, IFormFile? formFile);
        public Task DeleteCarModelAsync(int id);
        public Task<ResponseData<CarModel>> CreateCarModelAsync(CarModel carModel,
            IFormFile? formFile);
    }
}
