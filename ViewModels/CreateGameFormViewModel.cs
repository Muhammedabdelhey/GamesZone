
namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel : GameViewModel
    {
        [AllowedExtensions, MaxFileSize]
        public IFormFile Cover { get; set; } = default!;

    }
}
