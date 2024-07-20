using GameZone.Settings;

namespace GameZone.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            var file = value as IFormFile;
            if (file != null)
            {
                string extension = Path.GetExtension(file.FileName);
                if (!ImageSettings.AllowedExtensions.Split(',').Contains(extension, StringComparer.OrdinalIgnoreCase))
                {
                    return new ValidationResult($"Only the following extensions are allowed: {ImageSettings.AllowedExtensions}");
                }
            }
            return ValidationResult.Success;
        }
    }
}