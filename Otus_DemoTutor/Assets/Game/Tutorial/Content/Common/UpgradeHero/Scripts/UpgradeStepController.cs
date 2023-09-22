using Game.Gameplay.Player;
using Game.Tutorial.Gameplay;
using Game.Tutorial.UI;
using GameSystem;
using UnityEngine;

namespace Game.Tutorial
{
    [AddComponentMenu("Tutorial/Step «Upgrade»")]
    public sealed class UpgradeStepController : TutorialStepController, IGameInitElement
    {
        private PointerManager pointerManager;

        private NavigationManager navigationManager;

        private ScreenTransform screenTransform;
        
        private WorldPlacePopupShower worldPlacePopupShower;

        private readonly MoveToUpgradeInspector actionInspector = new();

        [SerializeField]
        private UpgradeHeroConfig config;
        
        [SerializeField]
        private TutorialStepPanelShower actionTutorialPanel;

        [SerializeField]
        private Transform pointerTransform;

        [SerializeField]
        private UpgradePopupShower popupShower;
        
        public override void ConstructGame(GameContext context)
        {
            this.pointerManager = context.GetService<PointerManager>();
            this.navigationManager = context.GetService<NavigationManager>();
            this.screenTransform = context.GetService<ScreenTransform>();
            this.worldPlacePopupShower = context.GetService<WorldPlacePopupShower>();

            var worldPlaceVisitor = context.GetService<WorldPlaceVisitInteractor>();
            this.actionInspector.Construct(worldPlaceVisitor, this.config);
            this.actionTutorialPanel.Construct(this.config);

            var popupManager = context.GetService<PopupManager>();
            this.popupShower.Construct(popupManager, config);

            base.ConstructGame(context);
        }

        void IGameInitElement.InitGame()
        {
            if (!this.IsStepFinished() && this.worldPlacePopupShower != null)
            {
                //Убираем базовый триггер
                this.worldPlacePopupShower.SetEnable(false);
            }
        }

        protected override void OnStart()
        {
            base.OnStart();
            
            //Подписываемся на подход к месту:
            this.actionInspector.Inspect(this.OnPlaceVisited);

            //Показываем указатель:
            var targetPosition = this.pointerTransform.position;
            this.pointerManager.ShowPointer(targetPosition, this.pointerTransform.rotation);
            this.navigationManager.StartLookAt(targetPosition);

            //Показываем квест в UI:
            this.actionTutorialPanel.Show(this.screenTransform.Value);
        }

        private void OnPlaceVisited()
        {
            //Убираем указатель
            this.pointerManager.HidePointer();
            this.navigationManager.Stop();

            //Убираем квест из UI:
            this.actionTutorialPanel.Hide();

            //Показываем попап:
            this.popupShower.ShowPopup();
        }

        protected override void OnStop()
        {
            base.OnStop();
            
            //Возвращаем базовый триггер:
            this.worldPlacePopupShower.SetEnable(true);
        }
    }
}