using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ST10303347_PROG6212P2F.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file && file.Length > _maxFileSize)
            {
                return new ValidationResult(ErrorMessage ?? $"File size must not exceed {_maxFileSize / 1024 / 1024} MB.");
            }
            return ValidationResult.Success;
        }
    }
}
