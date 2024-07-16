using GameZone.Settings;

namespace GameZone.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value is not IFormFile file)
            {
                return new ValidationResult("Invalid file type. Please upload a valid file.");
            }
            string extension = Path.GetExtension(file.FileName);
            if (!ImageSettings.AllowedExtensions.Split(',').Contains(extension, StringComparer.OrdinalIgnoreCase))
            {
                return new ValidationResult($"Only the following extensions are allowed: {ImageSettings.AllowedExtensions}");
            }
            return ValidationResult.Success;
        }
    }
}