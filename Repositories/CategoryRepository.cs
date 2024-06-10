using BO.Enums;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Databases;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FunewsManagementDbContext _dbContext;

        public CategoryRepository(FunewsManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Category> GetCategories()
        {
            try
            {
                return _dbContext.Categories.Where(x => true)
                    .AsQueryable();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Category?> GetCategory(short id)
        {
            try
            {
                var category = await _dbContext.Categories.FindAsync(id);
                return category;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CategoryOperationResult> AddCategory(Category category)
        {
            try
            {
                if (category is null)
                    return CategoryOperationResult.Empty;
                
                _dbContext.Categories.Add(category);
                await _dbContext.SaveChangesAsync();
                return CategoryOperationResult.Success;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CategoryOperationResult> DeleteCategory(short id)
        {
            try
            {
                var category = await GetCategory(id);
                if (category is not null)
                {
                    // check category is existed in news
                    var isExisted = await _dbContext.NewsArticles.AnyAsync(x => x.CategoryId == category.CategoryId);
                    if (isExisted)
                        return CategoryOperationResult.Used;
                    
                    _dbContext.Categories.Remove(category);
                    await _dbContext.SaveChangesAsync();
                    return CategoryOperationResult.Success;
                }
                return CategoryOperationResult.Empty;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CategoryOperationResult> UpdateCategory(Category category)
        {
            try
            {
                if (category is null)
                    return CategoryOperationResult.Empty;

                _dbContext.Categories.Update(category);
                await _dbContext.SaveChangesAsync();
                return CategoryOperationResult.Success;
            }
            catch
            {
                throw;
            }
        }
    }
}
