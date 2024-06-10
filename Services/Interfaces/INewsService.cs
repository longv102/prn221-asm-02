using BO.Dtos;
using BO.Enums;

namespace Services.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<NewsArticleDto>> GetNews();

        Task<IEnumerable<NewsArticleDto>> GetNewsV2();

        Task<NewsArticleDto> GetNewsById(string id);

        Task<NewsOperationResult> CreateNews(NewsArticleDto request);

        Task<NewsOperationResult> UpdateNews(NewsArticleDto request);

        Task<NewsOperationResult> DeleteNews(string id);

        Task<IEnumerable<NewsArticleDto>> GetNewsByStaffEmail(string staffEmail);
        
        Task<IList<TagDto>> GetTags();

        Task<IList<int>> GetTagOfANewsArticle(string newsId);

        Task<NewsOperationResult> UpdateTags(string newsId, IList<int> selectedValues);

        Task<IList<NewsArticleDto>> MakeNewsReport(short accountId, DateTime fromDate, DateTime toDate);
    }
}
