using BO.Dtos;
using BO.Enums;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategories();

        Task<CategoryDto> GetCategory(short id);

        Task<CategoryOperationResult> CreateCategory(CategoryDto category);

        Task<CategoryOperationResult> UpdateCategory(CategoryDto category);

        Task<CategoryOperationResult> DeleteCategory(short id);
    }
}
