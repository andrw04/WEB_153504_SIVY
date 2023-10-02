using System.Text;
using System.Text.Json;
using WEB_153504_SIVY.Domain.Entities;
using WEB_153504_SIVY.Domain.Models;

namespace WEB_153504_SIVY.Services.CarModelService
{
    public class ApiCarModelService : ICarModelService
    {
        private readonly HttpClient _httpClient;
        private ILogger<ApiCarModelService> _logger;
        private string _pageSize;
        JsonSerializerOptions _serializerOptions;

        public ApiCarModelService(HttpClient httpClient, IConfiguration configuration, ILogger<ApiCarModelService> logger)
        {
            _httpClient = httpClient;
            _pageSize = configuration.GetSection("ItemsPerPage").Value;
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _logger = logger;
        }

        public Task<ResponseData<CarModel>> CreateCarModelAsync(CarModel carModel, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCarModelAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<List<CarModel>>> GetCarModelByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseData<CarModelListModel<CarModel>>> GetCarModelListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}CarModels/");

            if (categoryNormalizedName != null)
            {
                urlString.Append($"{categoryNormalizedName}/");
            }

            if (pageNo > 1)
            {
                urlString.Append($"page{pageNo}");
            }

            if (!_pageSize.Equals(3))
            {
                urlString.Append(QueryString.Create("pageSize", _pageSize));
            }

            var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response
                        .Content
                        .ReadFromJsonAsync<ResponseData<CarModelListModel<CarModel>>>(_serializerOptions);
                }
                catch(JsonException e)
                {
                    _logger.LogError($"-----> Ошибка: {e.Message}");
                    return new ResponseData<CarModelListModel<CarModel>>
                    {
                        Success = false,
                        ErrorMessage = $"Ошибка: {e.Message}"
                    };
                }
            }

            _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
            return new ResponseData<CarModelListModel<CarModel>>
            {
                Success = false,
                ErrorMessage = $"-----> Данные не получены от сервера. Error: {response.StatusCode.ToString()}"
            };
        }

        public Task UpdateCarModelAsync(int id, CarModel carModel, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}
