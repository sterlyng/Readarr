using System.Collections.Generic;

namespace Readarr.Api.V1.Blocklist;

public class BlocklistBulkResource
{
    public required List<int> Ids { get; set; }
}
