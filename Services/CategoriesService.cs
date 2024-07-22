
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDbContext _context;
        public CategoriesService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SelectListItem>> GetCategoriesForDropDownAsync()
        {
            return await _context.Categories
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .OrderBy(c => c.Text)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                 .OrderBy(c => c.Name)
                 .AsNoTracking()
                 .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return null;
            }
            return category;
        }
        public async Task CreateAsync(Category model)
        {
            Category category = new()
            {
                Name = model.Name,
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category?> UpdateAsync(Category model)
        {
            var category = _context.Categories.Find(model.Id);
            if (category == null)
            {
                return null;
            }
            category.Name = model.Name;
            await _context.SaveChangesAsync();
            return category;
        }

    }
}
