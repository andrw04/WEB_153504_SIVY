using WEB_153504_SIVY.Domain.Entities;
using WEB_153504_SIVY.Domain.Models;

namespace WEB_153504_SIVY.Services.SparePartService
{
    public interface ISparePartService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}
