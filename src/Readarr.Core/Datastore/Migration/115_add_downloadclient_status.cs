using FluentMigrator;
using Readarr.Core.Datastore.Migration.Framework;

namespace Readarr.Core.Datastore.Migration
{
    [Migration(115)]
    public class add_downloadclient_status : ReadarrMigrationBase
    {
        protected override void MainDbUpgrade()
        {
            Create.TableForModel("DownloadClientStatus")
                  .WithColumn("ProviderId").AsInt32().NotNullable().Unique()
                  .WithColumn("InitialFailure").AsDateTime().Nullable()
                  .WithColumn("MostRecentFailure").AsDateTime().Nullable()
                  .WithColumn("EscalationLevel").AsInt32().NotNullable()
                  .WithColumn("DisabledTill").AsDateTime().Nullable();
        }
    }
}
