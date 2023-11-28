using WEB_153504_SIVY.Domain.Entities;

namespace WEB_153504_SIVY.BlazorWasm.Services
{
    public interface IDataService
    {
        event Action DataLoaded;
        List<CarBodyType> CarBodyTypes { get; set; }
        List<CarModel> CarModels { get; set; }
        bool Success { get; set; }
        string ErrorMessage { get; set; }
        int TotalPages { get; set; }
        int CurrentPage { get; set; }
        public Task GetCarModelListAsync(string? categoryNormalizedName, int pageNo = 1);
        public Task GetCarModelByIdAsync(int id);
        public Task GetBodyTypeByIdAsync(int id);
        public Task GetBodyTypeListAsync();
    }
}
