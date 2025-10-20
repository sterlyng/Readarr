using Readarr.Core.Update;
using Readarr.Http.REST;

namespace Readarr.Api.V1.Settings;

public class UpdateSettingsResource : RestResource
{
    public string Branch { get; set; }
    public bool UpdateAutomatically { get; set; }
    public UpdateMechanism UpdateMechanism { get; set; }
    public string UpdateScriptPath { get; set; }
}
