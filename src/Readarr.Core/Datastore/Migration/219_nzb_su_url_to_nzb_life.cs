using FluentMigrator;
using Readarr.Core.Datastore.Migration.Framework;

namespace Readarr.Core.Datastore.Migration
{
    [Migration(219)]
    public class nzb_su_url_to_nzb_life : ReadarrMigrationBase
    {
        protected override void MainDbUpgrade()
        {
            Execute.Sql("UPDATE \"Indexers\" SET \"Settings\" = replace(\"Settings\", '//api.nzb.su', '//api.nzb.life')" +
                        "WHERE \"Implementation\" = 'Newznab'" +
                        "AND \"Settings\" LIKE '%//api.nzb.su%'");
        }
    }
}
