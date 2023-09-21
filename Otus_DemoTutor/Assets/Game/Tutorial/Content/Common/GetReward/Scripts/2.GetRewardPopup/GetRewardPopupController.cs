using Game.Meta;
using GameSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Tutorial
{
    [RequireComponent(typeof(GetRewardsPopupViewChanger))]
    public sealed class GetRewardPopupController : MonoBehaviour, IGameConstructElement
    {
        [SerializeField] private GetRewardPopupListPresenter getRewardPopupListPresenter;
        [SerializeField] private Transform fading;
        [Header("Close")]
        [SerializeField] private Button closeButton;
        [SerializeField] private GameObject closeCursor;
        [Header("Construct Parameters")]
        [SerializeField,ReadOnly] private GetRewardStepController getRewardStepController;
        [ShowInInspector,ReadOnly] private GameObject questCursor;

        private readonly GetRewardPopupInspector questInspector = new();
        private MissionDifficulty targetMissionDifficulty;

        public void LinkPopupPrefabWithStepController(GetRewardStepController getRewardStepController)
        {
            this.getRewardStepController = getRewardStepController;
        }

        void IGameConstructElement.ConstructGame(GameContext context)
        {
            this.targetMissionDifficulty = this.getRewardStepController.Config.targetMission.Difficulty;
            
            var missionsManager = context.GetService<MissionsManager>();
            this.questInspector.Construct(missionsManager, targetMissionDifficulty);

            GetRewardsPopupViewChanger popupViewChanger = this.GetComponent<GetRewardsPopupViewChanger>();
            popupViewChanger.InitMissionPopupViewValues(targetMissionDifficulty);
            
            this.questCursor = popupViewChanger.GetCurrentMissionCursor();
            
            this.getRewardPopupListPresenter.Construct(context, targetMissionDifficulty,
                popupViewChanger.GetCurrentMissionFakePresenter());

            this.InitView();
        }

        private void InitView()
        {
            this.closeCursor.SetActive(false);
            this.closeButton.interactable = false;
            this.questCursor.SetActive(false);
        }

        public void Show()
        {
            //Ждем выполнение квеста прокачки:
            this.questInspector.Inspect(this.OnQuestFinished);
            
            //Включаем курсор на GetReward:
            this.questCursor.SetActive(true);
        }

        private void OnQuestFinished()
        {
            //Выключаем курсор на прокачке:
            this.questCursor.SetActive(false);

            //Включаем курсор на кнопке закрыть:
            this.closeCursor.SetActive(true);

            // //Делаем затемнение на прокачке:
            // this.fading.SetAsLastSibling();
            
            this.fading.gameObject.SetActive(true);

            //Активируем кнопку закрыть:
            this.closeButton.interactable = true;
            this.closeButton.onClick.AddListener(this.OnCloseClicked);
            
            //Завершаем шаг туториала:
            this.getRewardStepController.FinishStep();
        }

        private void OnCloseClicked()
        {
            this.closeButton.onClick.RemoveListener(this.OnCloseClicked);
            
            //Выключаем курсор на кнопке закрыть:
            this.closeCursor.SetActive(false);

            //Переходим к следующему шагу туториала:
            this.getRewardStepController.MoveNextStep();
        }
    }
}