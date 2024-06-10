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
    }
}
