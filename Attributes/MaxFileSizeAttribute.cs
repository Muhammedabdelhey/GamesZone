using GameZone.Settings;

namespace GameZone.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > ImageSettings.MaxSizeInMBs * 1024 * 1024)
                {
                    return new ValidationResult($"Maximum Allowed Size is {ImageSettings.MaxSizeInMBs} MBs");
                }
            }
            return ValidationResult.Success;
        }
    }
}
