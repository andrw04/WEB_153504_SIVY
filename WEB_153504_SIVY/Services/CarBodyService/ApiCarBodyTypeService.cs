using System.Text;
using System.Text.Json;
using WEB_153504_SIVY.Domain.Entities;
using WEB_153504_SIVY.Domain.Models;
using WEB_153504_SIVY.Services.CarModelService;

namespace WEB_153504_SIVY.Services.CarBodyService
{
    public class ApiCarBodyTypeService : ICarBodyTypeService
    {
        private readonly HttpClient _httpClient;
        private ILogger<ApiCarBodyTypeService> _logger;
        JsonSerializerOptions _serializerOptions;
        public ApiCarBodyTypeService(HttpClient httpClient, ILogger<ApiCarBodyTypeService> logger)
        {
            _httpClient = httpClient;
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _logger = logger;
        }
        public async Task<ResponseData<List<CarBodyType>>> GetCarBodyTypeListAsync()
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}CarBodyTypes/");
            var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response
                        .Content
                        .ReadFromJsonAsync<ResponseData<List<CarBodyType>>>(_serializerOptions);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"-----> Ошибка: {ex.Message}");
                    return new ResponseData<List<CarBodyType>>
                    {
                        Success = false,
                        ErrorMessage = $"Ошибка: {ex.Message}"
                    };
                }
            }

            return new ResponseData<List<CarBodyType>>
            {
                Success = false,
                ErrorMessage = $"-----> Данные не получены от сервера. Error: {response.StatusCode.ToString()}"
            };

        }
    }
}
