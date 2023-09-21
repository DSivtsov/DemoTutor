using System;
using Game.Gameplay.Player;
using Game.Meta;
using GameSystem;
using UnityEngine;

namespace Game.Tutorial
{
    public sealed class GetRewardPopupListPresenter : MonoBehaviour
    {
        [SerializeField] private MissionItem[] missionItems;

        private MissionsManager missionsManager;
        private MoneyPanelAnimator_AddMoney moneyPanelAnimator;
        private readonly GetRewardsMissionController getRewardsMissionController;

        public void Construct(GameContext context, MissionDifficulty targetMissionDifficulty, MissionPresenter fakeMissionPresenter)
        {
            this.missionsManager = context.GetService<MissionsManager>();
            this.moneyPanelAnimator = context.GetService<MoneyPanelAnimator_AddMoney>();

            ReplaceTargetPresenter(fakeMissionPresenter, targetMissionDifficulty);

            SetMissionPresenters();
        }

        public void Show()
        {
            this.missionsManager.OnMissionChanged += this.OnMissionChanged;

            var missions = this.missionsManager.GetMissions();
            for (int i = 0, count = missions.Length; i < count; i++)
            {
                var mission = missions[i];
                var presenter = this.GetPresenter(mission.Difficulty);
                presenter.Start(mission);
            }
        }

        public void Hide()
        {
            this.missionsManager.OnMissionChanged -= this.OnMissionChanged;

            for (int i = 0, count = this.missionItems.Length; i < count; i++)
            {
                var presenter = this.missionItems[i].presenter;
                presenter.Stop();
            }
        }

        private void ReplaceTargetPresenter(MissionPresenter fakeMissionPresenter, MissionDifficulty targetMissionDifficulty)
        {
            for (int i = 0; i < missionItems.Length; i++)
            {
                var currentMissionItems = missionItems[i];
                if (targetMissionDifficulty == currentMissionItems.difficulty)
                {
                    currentMissionItems.presenter = fakeMissionPresenter;
                    return;
                }
            }

            throw new NotImplementedException(
                "[GetRewardPopupListPresenter]: Can't find data for demanded difficulty [{difficulty}]");
        }

        private void SetMissionPresenters()
        {
            for (int i = 0, count = this.missionItems.Length; i < count; i++)
            {
                var missionItem = this.missionItems[i];
                missionItem.presenter.Construct(this.missionsManager, moneyPanelAnimator);
            }
        }

        private MissionPresenter GetPresenter(MissionDifficulty difficulty)
        {
            for (int i = 0, count = this.missionItems.Length; i < count; i++)
            {
                var missionItem = this.missionItems[i];
                if (missionItem.difficulty == difficulty)
                {
                    return missionItem.presenter;
                }
            }

            throw new Exception($"Mission with difficulty {difficulty} is not found"!);
        }

        private void OnMissionChanged(Mission mission)
        {
            var presenter = this.GetPresenter(mission.Difficulty);
            if (presenter.IsShown)
            {
                presenter.Stop();
            }

            presenter.Start(mission);
        }

        [Serializable]
        private sealed class MissionItem
        {
            [SerializeField] public MissionDifficulty difficulty;
            [SerializeField] public MissionPresenter presenter;
        }
    }
}