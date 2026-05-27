using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces.Repositories;
using LibraryManagement.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(
            ICategoryRepository categoryRepository,
            ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        // Retrieves all registered categories
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        // Retrieves a category by its identifier
        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        // Retrieves a category by its name
        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _categoryRepository.GetByNameAsync(name);
        }

        // Creates a category after validating required fields and duplicate names
        public async Task<Category> CreateAsync(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
                throw new InvalidOperationException("Category name is required.");

            var isNameUnique = await _categoryRepository.IsNameUniqueAsync(category.Name);
            if (!isNameUnique)
                throw new InvalidOperationException("A category with the same name already exists.");

            _logger.LogInformation("Creating category {CategoryName}", category.Name);

            return await _categoryRepository.AddAsync(category);
        }

        // Updates a category after validating existence and duplicate names
        public async Task UpdateAsync(Category category)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(category.Id);
            if (existingCategory is null)
                throw new InvalidOperationException("Category not found.");

            var isNameUnique = await _categoryRepository.IsNameUniqueAsync(category.Name, category.Id);
            if (!isNameUnique)
                throw new InvalidOperationException("A category with the same name already exists.");

            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;

            await _categoryRepository.UpdateAsync(existingCategory);
        }

        // Deletes a category by its identifier
        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
                throw new InvalidOperationException("Category not found.");

            await _categoryRepository.DeleteAsync(category);
        }
    }
}
