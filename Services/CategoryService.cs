using AutoMapper;
using BO.Dtos;
using BO.Enums;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services.Interfaces;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public Task<CategoryOperationResult> CreateCategory(CategoryDto category)
        {
            var request = _mapper.Map<Category>(category);
            var result = _categoryRepository.AddCategory(request);
            return result;
        }

        public Task<CategoryOperationResult> DeleteCategory(short id)
        {
            var result = _categoryRepository.DeleteCategory(id);
            return result;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            var categories = await _categoryRepository.GetCategories()
                .ToListAsync();
            var response = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return response;
        }

        public async Task<CategoryDto> GetCategory(short id)
        {
            var category = await _categoryRepository.GetCategory(id);
            var response = _mapper.Map<CategoryDto>(category);
            return response;
        }

        public Task<CategoryOperationResult> UpdateCategory(CategoryDto category)
        {
            var request = _mapper.Map<Category>(category);
            var result = _categoryRepository.UpdateCategory(request);
            return result;
        }
    }
}
