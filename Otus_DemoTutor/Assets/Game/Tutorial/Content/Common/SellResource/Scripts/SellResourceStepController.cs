using Game.Gameplay.Player;
using Game.Tutorial.Gameplay;
using Game.Tutorial.UI;
using GameSystem;
using UnityEngine;

namespace Game.Tutorial
{
    [AddComponentMenu("Tutorial/Step «Sell Resource»")]
    public sealed class SellResourceStepController : TutorialStepController
    {
        private PointerManager pointerManager;

        private NavigationManager navigationManager;

        private ScreenTransform screenTransform;
    
        private readonly SellResourceInspector actionInspector = new();

        [SerializeField]
        private SellResourceConfig config;

        [SerializeField]
        private TutorialStepPanelShower actionPanel = new();

        [SerializeField]
        private Transform pointerTransform;

        public override void ConstructGame(GameContext context)
        {
            this.pointerManager = context.GetService<PointerManager>();
            this.navigationManager = context.GetService<NavigationManager>();
            this.screenTransform = context.GetService<ScreenTransform>();
            
            var sellInteractor = context.GetService<VendorInteractor>();
            this.actionInspector.Construct(sellInteractor, this.config);
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