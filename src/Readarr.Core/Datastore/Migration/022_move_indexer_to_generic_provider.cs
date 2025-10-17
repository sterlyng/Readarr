using FluentMigrator;
using Readarr.Core.Datastore.Migration.Framework;

namespace Readarr.Core.Datastore.Migration
{
    [Migration(22)]
    public class move_indexer_to_generic_provider : ReadarrMigrationBase
    {
        protected override void MainDbUpgrade()
        {
            Alter.Table("Indexers").AddColumn("ConfigContract").AsString().Nullable();
        }
    }
}
