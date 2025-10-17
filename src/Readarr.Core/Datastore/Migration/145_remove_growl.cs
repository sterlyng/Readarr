using FluentMigrator;
using Readarr.Core.Datastore.Migration.Framework;

namespace Readarr.Core.Datastore.Migration
{
    [Migration(145)]
    public class remove_growl : ReadarrMigrationBase
    {
        protected override void MainDbUpgrade()
        {
            Delete.FromTable("Notifications").Row(new { Implementation = "Growl" });
        }
    }
}
