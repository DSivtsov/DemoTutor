using System;
using Game.Meta;

namespace Game.Tutorial
{
    public sealed class GetRewardsMissionController
    {
        private MissionsManager missionsManager;

        public void Construct(MissionsManager missionsManager, MissionConfig targetMissionConfig)
        {
            this.missionsManager = missionsManager;
            
            this.StopCurrentMissionTargetType(targetMissionConfig.Difficulty);
            
            Mission mission = this.SetupAndStartTargetMission(targetMissionConfig);

            this.CompleteTargetMission(mission);
        }
        
        private void StopCurrentMissionTargetType(MissionDifficulty targetMissionDifficulty)
        {
            if (this.missionsManager.IsMissionExists(targetMissionDifficulty))
            {
                Mission currentMission = this.missionsManager.GetMission(targetMissionDifficulty);
                if (currentMission.State == MissionState.STARTED)
                {
                    currentMission.Stop();
                }
            }
        }

        private Mission SetupAndStartTargetMission(MissionConfig missionConfig)
        {
            Mission mission = this.missionsManager.SetupMission(missionConfig);
            mission.Start();

            return mission;
        }

        private void CompleteTargetMission(Mission targetMission)
        {
            switch (targetMission)
            {
                case KillEnemyMission killEnemyMission:
                    killEnemyMission.Setup(killEnemyMission.RequiredKills);
                    break;
                case EarnMoneyMission earnMoneyMission:
                    earnMoneyMission.Setup(earnMoneyMission.RequiredMoney);
                    break;
                case CollectResourcesMission collectResourcesMission:
                    collectResourcesMission.Setup(collectResourcesMission.RequiredResources);
                    break;
                default:
                    throw new NotImplementedException($"Not implemented mission type [{targetMission.Id}]");
            }
            
            targetMission.TryComplete();
        }
    }
}