using Readarr.Core.Annotations;

namespace Readarr.Core.Indexers;

public enum SeasonPackSeedGoal
{
    [FieldOption(Label = "IndexerSettingsSeasonPackSeedGoalUseStandardGoals")]
    UseStandardSeedGoal = 0,
    [FieldOption(Label = "IndexerSettingsSeasonPackSeedGoalUseSeasonPackGoals")]
    UseSeasonPackSeedGoal = 1
}
