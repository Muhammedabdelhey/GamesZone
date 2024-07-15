namespace GameZone.Services
{
    public interface IPlatfromsService
    {
        Task<IEnumerable<SelectListItem>> GetPlatformsForDropDownAsync();
    }
}
