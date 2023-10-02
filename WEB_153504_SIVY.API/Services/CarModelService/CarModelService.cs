using Microsoft.EntityFrameworkCore;
using WEB_153504_SIVY.API.Data;
using WEB_153504_SIVY.Domain.Entities;
using WEB_153504_SIVY.Domain.Models;

namespace WEB_153504_SIVY.API.Services.CarModelService
{
    public class CarModelService : ICarModelService
    {
        private readonly int _maxPageSize = 20;

        private ApplicationDbContext _context;

        public CarModelService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<ResponseData<CarModel>> CreateCarModelAsync(CarModel carModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseData<CarModelListModel<CarModel>>> GetCarModelListAsync(string? normalizedName, int pageNo, int pageSize = 3)
        {
            if (pageSize > _maxPageSize)
                pageSize = _maxPageSize;

            var query = _context.CarModels.AsQueryable();
            var dataList = new CarModelListModel<CarModel>();

            query = query.Where(m => normalizedName == null || m.CarBody.NormalizedName.Equals(normalizedName));

            var count = query.Count();

            if (count == 0)
            {
                return new ResponseData<CarModelListModel<CarModel>>()
                {
                    Data = dataList
                };
            }

            int totalPages = (int)Math.Ceiling(count / (double)pageSize);

            if (pageNo > totalPages)
            {
                return new ResponseData<CarModelListModel<CarModel>>
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = "No such page",
                };
            }

            dataList.Items = await query
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            dataList.CurrentPage = pageNo;
            dataList.TotalPages = totalPages;

            var response = new ResponseData<CarModelListModel<CarModel>>
            {
                Data = dataList,
            };

            return response;
        }

        public Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCarModelAsync(int id, CarModel carModel)
        {
            throw new NotImplementedException();
        }
    }
}
