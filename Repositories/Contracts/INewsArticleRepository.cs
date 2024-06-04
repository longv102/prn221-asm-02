namespace Repositories.Contracts
{
    public interface INewsArticleRepository
    {
        Task<IEnumerable<NewsArticle>> GetNews();

        Task<NewsArticle?> GetNewsById(string newsId);
    }
}
