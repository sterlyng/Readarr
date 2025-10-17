namespace Readarr.Core.Validation
{
    public class ReadarrValidationState
    {
        public static ReadarrValidationState Warning = new ReadarrValidationState { IsWarning = true };

        public bool IsWarning { get; set; }
    }
}
