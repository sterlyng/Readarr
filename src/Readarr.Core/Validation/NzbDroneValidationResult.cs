using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Readarr.Common.Extensions;

namespace Readarr.Core.Validation
{
    public class ReadarrValidationResult : ValidationResult
    {
        public ReadarrValidationResult()
        {
            Failures = new List<ReadarrValidationFailure>();
            Errors = new List<ReadarrValidationFailure>();
            Warnings = new List<ReadarrValidationFailure>();
        }

        public ReadarrValidationResult(ValidationResult validationResult)
            : this(validationResult.Errors)
        {
        }

        public ReadarrValidationResult(IEnumerable<ValidationFailure> failures)
        {
            var errors = new List<ReadarrValidationFailure>();
            var warnings = new List<ReadarrValidationFailure>();

            foreach (var failureBase in failures)
            {
                if (failureBase is not ReadarrValidationFailure failure)
                {
                    failure = new ReadarrValidationFailure(failureBase);
                }

                if (failure.IsWarning)
                {
                    warnings.Add(failure);
                }
                else
                {
                    errors.Add(failure);
                }
            }

            Failures = errors.Concat(warnings).ToList();
            Errors = errors;
            errors.ForEach(base.Errors.Add);
            Warnings = warnings;
        }

        public IList<ReadarrValidationFailure> Failures { get; private set; }
        public new IList<ReadarrValidationFailure> Errors { get; private set; }
        public IList<ReadarrValidationFailure> Warnings { get; private set; }

        public virtual bool HasWarnings => Warnings.Any();

        public override bool IsValid => Errors.Empty();
    }
}
