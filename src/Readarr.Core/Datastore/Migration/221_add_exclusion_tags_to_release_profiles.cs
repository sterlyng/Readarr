using FluentMigrator;
using Readarr.Core.Datastore.Migration.Framework;

namespace Readarr.Core.Datastore.Migration
{
    [Migration(221)]
    public class add_exclusion_tags_to_release_profiles : ReadarrMigrationBase
    {
        protected override void MainDbUpgrade()
        {
            Alter.Table("ReleaseProfiles").AddColumn("ExcludedTags").AsString().NotNullable().WithDefaultValue("[]");
        }
    }
}
