using WEB_153504_SIVY.Domain.Entities;
using WEB_153504_SIVY.Domain.Models;

namespace WEB_153504_SIVY.Services.CarBodyService
{
    public interface ICarBodyTypeService
    {
        public Task<ResponseData<List<CarBodyType>>> GetCarBodyTypeListAsync();
    }
}
