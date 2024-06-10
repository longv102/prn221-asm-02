using BO.Enums;

namespace Repositories.Contracts
{
    public interface INewsArticleRepository
    {
        Task<IEnumerable<NewsArticle>> GetNews();

        Task<NewsArticle?> GetNewsById(string newsId);

        IQueryable<NewsArticle> GetNewsQueryable();

        Task<NewsOperationResult> AddNews(NewsArticle newsArticle);

        Task<NewsOperationResult> UpdateNews(NewsArticle newsArticle);

        Task<NewsOperationResult> DeleteNews(string newsId);

        Task<IEnumerable<NewsArticle>> GetNewsByStaffEmail(string email);
    }
}
