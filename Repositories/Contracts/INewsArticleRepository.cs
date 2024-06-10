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

        Task<IList<Tag>> GetTags();

        Task<IList<int>> GetTagOfANews(string newsArticleId);

        Task<NewsOperationResult> UpdateTagsForNews(string newsArticleId, IList<int> seletedTagIds);

        Task<IList<NewsArticle>> GetNewsOfAStaffWithDate(short accountId, DateTime fromDate, DateTime toDate);
    }
}
