using FluentMigrator;
using Readarr.Core.Datastore.Migration.Framework;

namespace Readarr.Core.Datastore.Migration
{
    [Migration(8)]
    public class remove_backlog : ReadarrMigrationBase
    {
        protected override void MainDbUpgrade()
        {
            Delete.Column("BacklogSetting").FromTable("Series");
            Delete.Column("UseSceneName").FromTable("NamingConfig");
        }
    }
}
