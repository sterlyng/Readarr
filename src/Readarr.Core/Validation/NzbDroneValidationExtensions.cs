using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace Readarr.Core.Validation
{
    public static class ReadarrValidationExtensions
    {
        public static ReadarrValidationResult Filter(this ReadarrValidationResult result, params string[] fields)
        {
            var failures = result.Failures.Where(v => fields.Contains(v.PropertyName)).ToArray();

            return new ReadarrValidationResult(failures);
        }

        public static void ThrowOnError(this ReadarrValidationResult result)
        {
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }

        public static bool HasErrors(this List<ValidationFailure> list)
        {
            return list.Any(item => item is not ReadarrValidationFailure { IsWarning: true });
        }
    }
}
