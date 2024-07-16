using GameZone.Settings;

namespace GameZone.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not IFormFile file)
            {
                return new ValidationResult("Invalid file type. Please upload a valid file.");
            }
            if (file.Length > ImageSettings.MaxSizeInMBs * 1024 * 1024)
            {
                return new ValidationResult($"Maximum Allowed Size is {ImageSettings.MaxSizeInMBs} MBs");
            }
            return ValidationResult.Success;
        }
    }
}
