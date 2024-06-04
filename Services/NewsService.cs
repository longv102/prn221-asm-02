using AutoMapper;
using BO.Dtos;
using Repositories.Contracts;
using Services.Interfaces;

namespace Services
{
    public class NewsService : INewsService
    {
        private readonly INewsArticleRepository _newsArticleRepository;
        private readonly IMapper _mapper;

        public NewsService(INewsArticleRepository newsArticleRepository, IMapper mapper)
        {
            _newsArticleRepository = newsArticleRepository;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<NewsArticleDto>> GetNews()
        {
            var news = await _newsArticleRepository.GetNews();
            var mappedNews = _mapper.Map<IEnumerable<NewsArticleDto>>(news);

            return mappedNews;
        }

        public async Task<NewsArticleDto> GetNewsById(string id)
        {
            var news = await _newsArticleRepository.GetNewsById(id);
            var mappedNews = _mapper.Map<NewsArticleDto>(news);
            
            return mappedNews;
        }
    }
}
