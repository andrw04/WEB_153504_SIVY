using WEB_153504_SIVY.Domain.Entities;
using WEB_153504_SIVY.Domain.Models;

namespace WEB_153504_SIVY.API.Services.CarBodyService
{
    public interface ICarBodyService
    {
        public Task<ResponseData<List<CarBodyType>>> GetCarBodyTypeListAsync();
    }
}
