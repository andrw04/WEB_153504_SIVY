using Microsoft.EntityFrameworkCore;
using WEB_153504_SIVY.API.Data;
using WEB_153504_SIVY.Domain.Entities;
using WEB_153504_SIVY.Domain.Models;

namespace WEB_153504_SIVY.API.Services.CarBodyService
{
    public class CarBodyService : ICarBodyService
    {

        private ApplicationDbContext _context;

        public CarBodyService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseData<List<CarBodyType>>> GetCarBodyTypeListAsync()
        {
            return new ResponseData<List<CarBodyType>>
            {
                Data = await _context.CarBodyTypes.ToListAsync(),
            };
        }
    }
}
