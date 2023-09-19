using Game.Gameplay.Player;
using Game.Tutorial.App;
using Game.Tutorial.Gameplay;
using Game.Tutorial.UI;
using GameSystem;
using UnityEngine;

namespace Game.Tutorial
{
    [AddComponentMenu("Tutorial/Step «Exchange Resource with Conveyor»")]
    public sealed class ExchangeResourceConveyorStepController : TutorialStepController
    {
        private PointerManager pointerManager;

        private NavigationManager navigationManager;

        private ScreenTransform screenTransform;
    
        private readonly ExchangeResourceConveyorInspector actionInspector = new();

        [SerializeField]
        private ExchangeResourceConveyorConfig config;

        [SerializeField]
        private TutorialStepPanelShower actionPanel = new();

        [SerializeField]
        private Transform pointerTransform;

        public override void ConstructGame(GameContext context)
        {
            this.pointerManager = context.GetService<PointerManager>();
            this.navigationManager = context.GetService<NavigationManager>();
            this.screenTransform = context.GetService<ScreenTransform>();
            ConveyorVisitInteractor conveyorVisitInteractor = context.GetService<ConveyorVisitInteractor>();
            ResourceStorage resourceStorage = context.GetService<ResourceStorage>();
            this.actionInspector.Construct(conveyorVisitInteractor, this.config, resourceStorage);
            this.actionPanel.Construct(this.config);
            
            base.ConstructGame(context);
        }

        protected override void OnStart()
        {
            base.OnStart();
            this.actionInspector.Inspect(this.NotifyAboutCompleteAndMoveNext);
            var targetPosition = this.pointerTransform.position;
            this.pointerManager.ShowPointer(targetPosition, this.pointerTransform.rotation);
            this.navigationManager.StartLookAt(targetPosition);
            this.actionPanel.Show(this.screenTransform.Value);
        }

        protected override void OnStop()
        {
            base.OnStop();
            this.navigationManager.Stop();
            this.pointerManager.HidePointer();
            this.actionPanel.Hide();
        }
    }
}