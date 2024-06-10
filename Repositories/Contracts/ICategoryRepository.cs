using BO.Enums;

namespace Repositories.Contracts
{
    public interface ICategoryRepository
    {
        IQueryable<Category> GetCategories();

        Task<Category?> GetCategory(short id);

        Task<CategoryOperationResult> AddCategory(Category category);

        Task<CategoryOperationResult> UpdateCategory(Category category); 

        Task<CategoryOperationResult> DeleteCategory(short id);
    }
}
