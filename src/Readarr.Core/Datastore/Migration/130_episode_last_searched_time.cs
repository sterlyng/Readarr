using FluentMigrator;
using Readarr.Core.Datastore.Migration.Framework;

namespace Readarr.Core.Datastore.Migration
{
    [Migration(130)]
    public class episode_last_searched_time : ReadarrMigrationBase
    {
        protected override void MainDbUpgrade()
        {
            Alter.Table("Episodes").AddColumn("LastSearchTime").AsDateTime().Nullable();
        }
    }
}
