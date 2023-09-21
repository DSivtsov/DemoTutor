using System;
using Game.Meta;
using UnityEngine;

namespace Game.Tutorial
{
    public class GetRewardsPopupViewChanger : MonoBehaviour
    {
        [SerializeField] private MissionFakePresenterWithCursor[] fakePresentersWithCursors;

        private MissionFakePresenterWithCursor currentMissionFakePresenterWithCursor;

        public GameObject GetCurrentMissionCursor() => this.currentMissionFakePresenterWithCursor.missionCursor;
        public MissionPresenter GetCurrentMissionFakePresenter() => this.currentMissionFakePresenterWithCursor.fakePresenter;

        public void InitMissionPopupViewValues(MissionDifficulty difficulty)
        {
            for (int i = 0; i < fakePresentersWithCursors.Length; i++)
            {
                currentMissionFakePresenterWithCursor = fakePresentersWithCursors[i];
                if (difficulty == currentMissionFakePresenterWithCursor.difficulty)
                    return;
            }

            throw new NotImplementedException("[GetRewardsPopupChanger]: Can't find data for demanded difficulty [{difficulty}]");
        }

        [Serializable]
        private sealed class MissionFakePresenterWithCursor
        {
            [SerializeField] public MissionDifficulty difficulty;
            [SerializeField] public MissionPresenter fakePresenter;
            [SerializeField] public GameObject missionCursor;
        }
    }
}