namespace GameZone.Services
{
    public interface ICategoriesService
    {
        Task<IEnumerable<SelectListItem>> GetCategoriesForDropDownAsync();
    }
}
