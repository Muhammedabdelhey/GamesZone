namespace GameZone.Services
{
    public interface IGamesService
    {
        Task CreateAsync(CreateGameFormViewModel model);
    }
}
