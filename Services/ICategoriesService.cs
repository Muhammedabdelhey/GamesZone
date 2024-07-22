namespace GameZone.Services
{
    public interface ICategoriesService
    {
        public Task<IEnumerable<SelectListItem>> GetCategoriesForDropDownAsync();
        public Task<IEnumerable<Category>> GetAllAsync();
        public Task<Category?> GetByIdAsync(int id);
        public Task CreateAsync(Category model);
        public Task<Category?> UpdateAsync(Category model);


    }
}
