using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lab_Lesson09.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class NoSensitiveWordsAttribute : ValidationAttribute
    {
        private readonly string[] _sensitiveWords;

        public NoSensitiveWordsAttribute(params string[] words)
        {
            if (words != null && words.Length > 0)
            {
                _sensitiveWords = words;
            }
            else
            {
                _sensitiveWords = new[] { "die", "admin", "fack", "fuck", "bitch", "hack", "spam" };
            }
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string text && !string.IsNullOrWhiteSpace(text))
            {
                foreach (var word in _sensitiveWords)
                {
                    // Match whole word or substring
                    var pattern = $@"\b{Regex.Escape(word)}\b";
                    if (Regex.IsMatch(text, pattern, RegexOptions.IgnoreCase))
                    {
                        var displayName = validationContext.DisplayName ?? "Trường này";
                        var errorMessage = ErrorMessage ?? $"{displayName} không được chứa từ nhạy cảm (Phát hiện từ cấm: '{word}').";
                        return new ValidationResult(errorMessage);
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}
