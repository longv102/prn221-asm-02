using BO.Enums;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Databases;
using System.ComponentModel.DataAnnotations;

namespace Repositories
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        private readonly FunewsManagementDbContext _dbContext;

        public NewsArticleRepository(FunewsManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<NewsOperationResult> AddNews(NewsArticle newsArticle)
        {
            try
            {
                if (newsArticle is null)
                    return NewsOperationResult.Empty;
                // check duplicate of news
                var isExisted = await _dbContext.NewsArticles.AnyAsync(x => x.NewsArticleId == newsArticle.NewsArticleId);
                if (isExisted) 
                    return NewsOperationResult.Duplicate;

                _dbContext.NewsArticles.Add(newsArticle);
                await _dbContext.SaveChangesAsync();
                return NewsOperationResult.Success;
            }
            catch
            {
                throw;
            }
        }

        public async Task<NewsOperationResult> DeleteNews(string newsId)
        {
            try
            {
                var news = await _dbContext.NewsArticles.FindAsync(newsId);
                if (news is not null)
                {
                    _dbContext.NewsArticles.Remove(news);
                    await _dbContext.SaveChangesAsync();
                    return NewsOperationResult.Success;
                }
                return NewsOperationResult.Empty;
            }
            catch
            {
                throw;
            }
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

        public async Task<IEnumerable<NewsArticle>> GetNewsByStaffEmail(string email)
        {
            try
            {
                var account = await _dbContext.SystemAccounts.FirstOrDefaultAsync(x => x.AccountEmail == email);
                
                var news = await _dbContext.NewsArticles
                    .Include(x => x.Category)
                    .Include(y => y.Tags)
                    .Where(n => n.NewsStatus == true && n.CreatedById == account.AccountId)
                    .ToListAsync();
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

        public async Task<IList<Tag>> GetTags()
        {
            try
            {
                var tags = await _dbContext.Tags.ToListAsync();
                return tags;
            }
            catch
            {
                throw;
            }
        }

        public async Task<NewsOperationResult> UpdateNews(NewsArticle newsArticle)
        {
            try
            {
                if (newsArticle is null)
                    return NewsOperationResult.Empty;

                _dbContext.NewsArticles.Update(newsArticle);
                await _dbContext.SaveChangesAsync();
                return NewsOperationResult.Success;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IList<int>> GetTagOfANews(string newsArticleId)
        {
            try
            {
                var tagValues = await _dbContext.NewsArticles
                    .Where(n => n.NewsArticleId == newsArticleId)
                    .SelectMany(n => n.Tags.Select(t => t.TagId))
                    .ToListAsync();
                return tagValues;
            }
            catch
            {
                throw;
            }
        }

        public async Task<NewsOperationResult> UpdateTagsForNews(string newsArticleId, IList<int> seletedTagIds)
        {
            try
            {
                var news = await _dbContext.NewsArticles
                    .Include(x => x.Tags)
                    .FirstOrDefaultAsync(x => x.NewsArticleId == newsArticleId);

                if (news is null)
                    return NewsOperationResult.Empty;

                // Retrieve the selected tags from the database
                var selectedTags = await _dbContext.Tags
                    .Where(t => seletedTagIds.Contains(t.TagId))
                    .ToListAsync();

                // Clear existing tags
                news.Tags.Clear();
                foreach (var tag in selectedTags)
                {
                    news.Tags.Add(tag);
                }

                await _dbContext.SaveChangesAsync();
                return NewsOperationResult.Success;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IList<NewsArticle>> GetNewsOfAStaffWithDate(short accountId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                var news = await _dbContext.NewsArticles
                    .Include(x => x.Tags)
                    .Include(x => x.CreatedBy)
                    .Where(x => x.CreatedById == accountId && x.CreatedDate >= fromDate && x.CreatedDate <= toDate)
                    .ToListAsync();
                return news;
            }
            catch
            {
                throw;
            }
        }
    }
}
