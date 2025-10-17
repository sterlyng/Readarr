using FluentMigrator;
using Readarr.Core.Datastore.Migration.Framework;

namespace Readarr.Core.Datastore.Migration
{
    [Migration(185)]
    public class add_episode_runtime : ReadarrMigrationBase
    {
        protected override void MainDbUpgrade()
        {
            Alter.Table("Episodes").AddColumn("Runtime").AsInt32().WithDefaultValue(0);
        }
    }
}
