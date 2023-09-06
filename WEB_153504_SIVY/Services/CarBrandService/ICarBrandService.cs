using WEB_153504_SIVY.Domain.Entities;
using WEB_153504_SIVY.Domain.Models;

namespace WEB_153504_SIVY.Services.CarBrandService
{
    public interface ICarBrandService
    {
        public Task<ResponseData<List<CarBrand>>> GetCarBrandListAsync();
    }
}
