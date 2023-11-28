using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http.Headers;
using System.Data.SqlTypes;
using System.Net.Http.Json;
using System.Text;
using WEB_153504_SIVY.Domain.Entities;
using WEB_153504_SIVY.Domain.Models;

namespace WEB_153504_SIVY.BlazorWasm.Services
{
    public class DataService : IDataService
    {
        public List<CarBodyType> CarBodyTypes { get; set; }
        public List<CarModel> CarModels { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }

        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IAccessTokenProvider _tokenProvider;

        public event Action DataLoaded;

        public DataService(HttpClient httpClient, IConfiguration configuration, IAccessTokenProvider tokenProvider)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _tokenProvider = tokenProvider;
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
                CarBodyTypes = data.Data;
            }
            DataLoaded.Invoke();
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
            DataLoaded.Invoke();
        }

        public async Task GetCarModelListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            try
            {
                var tokenRequest = await _tokenProvider.RequestAccessToken();
                if (tokenRequest.TryGetToken(out var token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Value);
                }

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
                DataLoaded.Invoke();
            }
            catch
            {
                throw;
            }
        }
    }
}
