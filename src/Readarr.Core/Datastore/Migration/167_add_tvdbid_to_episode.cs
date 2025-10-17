using FluentMigrator;
using Readarr.Core.Datastore.Migration.Framework;

namespace Readarr.Core.Datastore.Migration
{
    [Migration(167)]
    public class add_tvdbid_to_episode : ReadarrMigrationBase
    {
        protected override void MainDbUpgrade()
        {
            Alter.Table("Episodes").AddColumn("TvdbId").AsInt32().Nullable();
        }
    }
}
