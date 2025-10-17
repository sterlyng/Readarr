using FluentMigrator;
using Readarr.Core.Datastore.Migration.Framework;

namespace Readarr.Core.Datastore.Migration
{
    [Migration(223)]
    public class add_source_to_blocklist : ReadarrMigrationBase
    {
        protected override void MainDbUpgrade()
        {
            Alter.Table("Blocklist").AddColumn("Source").AsString().Nullable();
        }
    }
}
