using FluentMigrator;
using Readarr.Core.Datastore.Migration.Framework;

namespace Readarr.Core.Datastore.Migration
{
    [Migration(211)]
    public class add_custom_colon_replacement_to_naming_config : ReadarrMigrationBase
    {
        protected override void MainDbUpgrade()
        {
            Alter.Table("NamingConfig").AddColumn("CustomColonReplacementFormat").AsString().WithDefaultValue("");
        }
    }
}
