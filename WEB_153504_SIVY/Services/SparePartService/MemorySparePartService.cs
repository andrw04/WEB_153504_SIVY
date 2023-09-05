using WEB_153504_SIVY.Domain.Entities;
using WEB_153504_SIVY.Domain.Models;

namespace WEB_153504_SIVY.Services.SparePartService
{
    public class MemorySparePartService : ISparePartService
    {
        /// <summary>
        /// Получение списка всех категорий
        /// </summary>
        /// <returns></returns>
        public Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Масла и жидкости", NormalizedName = "oil-and-liquids" },
                new Category { Id = 2, Name = "Фильтры", NormalizedName = "filters" },
                new Category { Id = 3, Name = "Тормозная система", NormalizedName = "brake-system" },
                new Category { Id = 4, Name = "Подвеска", NormalizedName = "suspension" },
                new Category { Id = 5, Name = "Очистка окон", NormalizedName = "window-cleaning" },
                new Category { Id = 6, Name = "Ремни, цепи и натяжители", NormalizedName = "belts-chains-and-tensioners" },
                new Category { Id = 7, Name = "Зажигание", NormalizedName = "ignition" },
            };
            var result = new ResponseData<List<Category>>();
            result.Data = categories;
            return Task.FromResult(result);
        }
    }
}
