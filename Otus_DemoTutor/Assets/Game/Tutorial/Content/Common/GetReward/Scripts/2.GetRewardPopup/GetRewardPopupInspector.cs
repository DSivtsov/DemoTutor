using System;
using Game.Meta;
using UnityEngine;

namespace Game.Tutorial
{
    public sealed class GetRewardPopupInspector
    {
        private MissionsManager missionsManager;
        
        private Action callback;
        private MissionDifficulty targetMissionDifficulty;

        public void Construct(MissionsManager missionsManager, MissionDifficulty targetMissionDifficulty)
        {
            this.missionsManager = missionsManager;
            this.targetMissionDifficulty = targetMissionDifficulty;
        }
        
        public void Inspect(Action callback)
        {
            this.callback = callback;
            this.missionsManager.OnMissionChanged += MissionsManagerOnOnMissionChanged;
        }

        private void MissionsManagerOnOnMissionChanged(Mission mission)
        {
            if (mission.Difficulty != this.targetMissionDifficulty)
            {
                Debug.LogWarning($"mission.Difficulty[{mission.Difficulty }] != [{this.targetMissionDifficulty}]");
            }
            
            this.missionsManager.OnMissionChanged -= MissionsManagerOnOnMissionChanged;
            this.callback?.Invoke();
        }
    }
}