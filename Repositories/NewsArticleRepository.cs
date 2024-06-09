using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Databases;

namespace Repositories
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        private readonly FunewsManagementDbContext _dbContext;

        public NewsArticleRepository(FunewsManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<NewsArticle>> GetNews()
        {
            try
            {
                var news = await _dbContext.NewsArticles.Include(x => x.CreatedBy)
                    .Include(x => x.Tags)
                    .Include(x => x.Category)
                    .Where(x => x.NewsStatus == true)
                    .ToListAsync();
                return news;
            }
            catch
            {
                throw;
            }
        }

        public async Task<NewsArticle?> GetNewsById(string newsId)
        {
            try
            {
                var news = await _dbContext.NewsArticles.Include(x => x.CreatedBy)
                    .Include(x => x.Tags)
                    .Include(x => x.Category)
                    .FirstOrDefaultAsync(x => x.NewsArticleId == newsId);
                return news;
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<NewsArticle> GetNewsQueryable()
        {
            try
            {
                var news = _dbContext.NewsArticles.Include(x => x.CreatedBy)
                    .Include(x => x.Tags)
                    .Include(x => x.Category)
                    .Where(x => x.NewsStatus == true)
                    .AsQueryable();
                return news;
            }
            catch
            {
                throw;
            }
        }
    }
}
