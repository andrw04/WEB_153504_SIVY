using System.Data.SqlTypes;
using System.Net.Http.Json;
using System.Text;
using WEB_153504_SIVY.Domain.Entities;
using WEB_153504_SIVY.Domain.Models;

namespace WEB_153504_SIVY.BlazorWasm.Services
{
    public class DataService : IDataService
    {
        public List<CarBodyType> BodyTypes { get; set; }
        public List<CarModel> CarModels { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }

        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public DataService(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public Task GetBodyTypeByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task GetBodyTypeListAsync()
        {
            var url = "CarBodyTypes";

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<ResponseData<List<CarBodyType>>>();
                BodyTypes = data.Data;
                TotalPages = 1;
                CurrentPage = 1;
            }
        }

        public async Task GetCarModelByIdAsync(int id)
        {
            var url = $"CarModels/{id}";

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<ResponseData<CarModel>>();
                CarModels = new List<CarModel>() { data.Data };
                TotalPages = 1;
                CurrentPage = 1;
            }
        }

        public async Task GetCarModelListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var url = new StringBuilder("CarModels/");
            if (categoryNormalizedName != null)
            {
                url.Append($"{categoryNormalizedName}/");
            }

            if (pageNo > 1)
            {
                url.Append($"page{pageNo}");
            }

            var response = await _httpClient.GetAsync(url.ToString());
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<ResponseData<CarModelListModel<CarModel>>>();
                CarModels = data.Data.Items;
                TotalPages = data.Data.TotalPages;
                CurrentPage = data.Data.CurrentPage;
            }
        }
    }
}
