using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WEB_153504_SIVY.Domain.Entities;
using WEB_153504_SIVY.Domain.Models;

namespace WEB_153504_SIVY.Services.CarModelService
{
    public class ApiCarModelService : ICarModelService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpContext _httpContext;
        private ILogger<ApiCarModelService> _logger;
        private string _pageSize;
        JsonSerializerOptions _serializerOptions;

        public ApiCarModelService(HttpClient httpClient,
            IConfiguration configuration,
            ILogger<ApiCarModelService> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _pageSize = configuration.GetSection("ItemsPerPage").Value;
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _logger = logger;
            _httpContext = httpContextAccessor.HttpContext;
        }


        public async Task<ResponseData<CarModel>> CreateCarModelAsync(CarModel carModel, IFormFile? formFile)
        {
            // api/CarModels
            var urlString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}CarModels/");

            var content = JsonContent.Create(carModel);

            await SetToken();
            var response = await _httpClient.PostAsync(new Uri(urlString.ToString()), content);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var responseCarModel = await response.Content.ReadFromJsonAsync<CarModel>();
                    if (formFile != null)
                    {
                        await SaveImageAsync(responseCarModel.Id, formFile);
                    }
                    return new ResponseData<CarModel> {
                            Data = responseCarModel,
                        };
                }
                catch (JsonException e)
                {
                    _logger.LogError($"-----> Ошибка: {e.Message}");
                    return new ResponseData<CarModel>
                    {
                        Success = false,
                        ErrorMessage = $"Ошибка: {e.Message}"
                    };
                }
            }

            _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
            return new ResponseData<CarModel>
            {
                Success = false,
                ErrorMessage = $"-----> Данные не получены от сервера. Error: {response.StatusCode.ToString()}"
            };
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

            await SetToken();
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

        public async Task UpdateCarModelAsync(int id, CarModel carModel, IFormFile? formFile)
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}CarModels/{id}");

            var content = JsonContent.Create(carModel);
            await SetToken();
            var response = _httpClient.PutAsync(urlString.ToString(), content);

            if (response.IsCompletedSuccessfully)
            {
                await SaveImageAsync(id, formFile);
            }
            else
            {
                _logger.LogError($"-----> Error: {response.Result.StatusCode.ToString()}");
            }
        }

        private async Task SaveImageAsync(int id, IFormFile image)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}CarModels/{id}")
            };

            var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(image.OpenReadStream());

            content.Add(streamContent, "formFile", image.FileName);
            request.Content = content;

            await SetToken();
            await _httpClient.SendAsync(request);
        }

        private async Task SetToken()
        {
            var token = await _httpContext.GetTokenAsync("access_token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }
    }
}
