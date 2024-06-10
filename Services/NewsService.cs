using AutoMapper;
using BO.Dtos;
using BO.Enums;
using Microsoft.EntityFrameworkCore;
using Repositories;
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

        public Task<NewsOperationResult> CreateNews(NewsArticleDto request)
        {
            var news = new NewsArticle
            {
                NewsArticleId = request.NewsArticleId,
                NewsTitle = request.NewsTitle,
                NewsContent = request.NewsContent,
                CategoryId = request.CategoryId,
                CreatedById = request.CreatedById,
                CreatedDate = request.CreatedDate,
                NewsStatus = request.NewsStatus,
            };
            var result = _newsArticleRepository.AddNews(news);
            return result;
        }

        public async Task<NewsOperationResult> DeleteNews(string id)
        {
            var result = await _newsArticleRepository.DeleteNews(id);
            return result;
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

        public async Task<IEnumerable<NewsArticleDto>> GetNewsByStaffEmail(string staffEmail)
        {
            var news = await _newsArticleRepository.GetNewsByStaffEmail(staffEmail);
            var response = _mapper.Map<IEnumerable<NewsArticleDto>>(news);
            return response;
        }

        public async Task<IEnumerable<NewsArticleDto>> GetNewsV2()
        {
            var news = await _newsArticleRepository.GetNewsQueryable()
                .ToListAsync();
            var response = _mapper.Map<IEnumerable<NewsArticleDto>>(news);
            return response;
        }

        public async Task<IList<int>> GetTagOfANewsArticle(string newsId)
        {
            var tagValues = await _newsArticleRepository.GetTagOfANews(newsId);
            return tagValues;
        }

        public async Task<IList<TagDto>> GetTags()
        {
            var tags = await _newsArticleRepository.GetTags();
            var response = _mapper.Map<IList<TagDto>>(tags);
            return response;
        }

        public async Task<IList<NewsArticleDto>> MakeNewsReport(short accountId, DateTime fromDate, DateTime toDate)
        {
            var news = await _newsArticleRepository.GetNewsOfAStaffWithDate(accountId, fromDate, toDate);
            var response = _mapper.Map<IList<NewsArticleDto>>(news);
            return response;
        }

        public async Task<NewsOperationResult> UpdateNews(NewsArticleDto request)
        {
            var news = new NewsArticle
            {
                NewsArticleId = request.NewsArticleId,
                NewsTitle = request.NewsTitle,
                CreatedDate = request.CreatedDate,
                NewsContent = request.NewsContent,
                CategoryId = request.CategoryId,
                CreatedById = request.CreatedById,
                ModifiedDate = DateTime.Now,
                NewsStatus = request.NewsStatus,
            };
            var result = await _newsArticleRepository.UpdateNews(news);
            return result;
        }

        public async Task<NewsOperationResult> UpdateTags(string newsId, IList<int> selectedValues)
        {
            var result = await _newsArticleRepository.UpdateTagsForNews(newsId, selectedValues);
            return result;
        }
    }
}
