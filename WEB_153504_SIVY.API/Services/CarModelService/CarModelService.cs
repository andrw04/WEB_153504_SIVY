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
        private IHttpContextAccessor _httpContextAccessor;
        private IWebHostEnvironment _environment;

        public CarModelService(ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment environment)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _environment = environment;
        }

        public Task<ResponseData<CarModel>> CreateCarModelAsync(CarModel carModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseData<CarModel>> GetCarModelByIdAsync(int id)
        {
            return new ResponseData<CarModel> { Data = await _context.CarModels.FindAsync(id) };
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

        public async Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
        {
            var responseData = new ResponseData<string>();
            var carModel = await _context.CarModels.FindAsync(id);
            if (carModel == null)
            {
                responseData.Success = false;
                responseData.Data = "No item found";
                return responseData;
            }
            var host = "https://" + _httpContextAccessor.HttpContext.Request.Host;

            var imageFolder = Path.Combine(_environment.WebRootPath, "Images");

            if (formFile != null)
            {
                if (!string.IsNullOrEmpty(carModel.Image))
                {
                    var prevImage = Path.GetFileName(carModel.Image);
                    var prevPath = Path.Combine(imageFolder, prevImage);

                    if (File.Exists(prevPath))
                    {
                        File.Delete(prevPath);
                    }
                }
                // Создать имя файла
                var ext = Path.GetExtension(formFile.FileName);
                var fName = Path.ChangeExtension(Path.GetRandomFileName(), ext);
                // сохранить файл

                using (FileStream stream = new FileStream(Path.Combine(imageFolder, fName), FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }

                // Указать имя файла в объекте
                carModel.Image = $"{host}/Images/{fName}";
                await _context.SaveChangesAsync();
            }

            responseData.Data = carModel.Image;
            return responseData;
        }

        public Task UpdateCarModelAsync(int id, CarModel carModel)
        {
            throw new NotImplementedException();
        }
    }
}
