using FluentMigrator;
using Readarr.Core.Datastore.Migration.Framework;

namespace Readarr.Core.Datastore.Migration
{
    [Migration(87)]
    public class remove_eztv : ReadarrMigrationBase
    {
        protected override void MainDbUpgrade()
        {
            Delete.FromTable("Indexers").Row(new { Implementation = "Eztv" });
        }
    }
}
