using FluentMigrator;
using Readarr.Core.Datastore.Migration.Framework;

namespace Readarr.Core.Datastore.Migration
{
    [Migration(180)]
    public class task_duration : ReadarrMigrationBase
    {
        protected override void MainDbUpgrade()
        {
            Alter.Table("ScheduledTasks").AddColumn("LastStartTime").AsDateTime().Nullable();
        }
    }
}
