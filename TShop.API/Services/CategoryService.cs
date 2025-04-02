using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TShop.API.Data;
using TShop.API.Models;

namespace TShop.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Category Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public bool Edit(int id, Category updatedCategory)
        {
            var category = _context.Categories.AsNoTracking().FirstOrDefault(c => c.Id == id);
            if (category == null) return false;

            updatedCategory.Id = category.Id;
            _context.Categories.Update(updatedCategory);
            _context.SaveChanges();
            return true;
        }

        public Category? Get(Expression<Func<Category, bool>> expression)
        {
            return _context.Categories.FirstOrDefault(expression);
        }

        public IEnumerable<Category> GetAll()
        {
            return [.. _context.Categories];
        }

        public bool Remove(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return true;
        }
    }
}
