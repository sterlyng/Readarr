using Readarr.Core.Validation;

namespace Readarr.Core.ThingiProvider
{
    public class NullConfig : IProviderConfig
    {
        public static readonly NullConfig Instance = new NullConfig();

        public ReadarrValidationResult Validate()
        {
            return new ReadarrValidationResult();
        }
    }
}
