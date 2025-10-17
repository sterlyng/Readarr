using FluentValidation.Results;

namespace Readarr.Core.Validation
{
    public class ReadarrValidationFailure : ValidationFailure
    {
        public bool IsWarning { get; set; }
        public string DetailedDescription { get; set; }
        public string InfoLink { get; set; }

        public ReadarrValidationFailure(string propertyName, string error)
            : base(propertyName, error)
        {
        }

        public ReadarrValidationFailure(string propertyName, string error, object attemptedValue)
            : base(propertyName, error, attemptedValue)
        {
        }

        public ReadarrValidationFailure(ValidationFailure validationFailure)
            : base(validationFailure.PropertyName, validationFailure.ErrorMessage, validationFailure.AttemptedValue)
        {
            CustomState = validationFailure.CustomState;
            var state = validationFailure.CustomState as ReadarrValidationState;

            IsWarning = state is { IsWarning: true };
        }
    }
}
