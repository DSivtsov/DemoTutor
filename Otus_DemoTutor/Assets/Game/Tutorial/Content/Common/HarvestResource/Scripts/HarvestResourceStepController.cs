using Game.Gameplay.Hero;
using Game.Tutorial.App;
using Game.Tutorial.Gameplay;
using Game.Tutorial.UI;
using GameSystem;
using UnityEngine;

namespace Game.Tutorial
{
    [AddComponentMenu("Tutorial/Step «Harvest Resource»")]
    public sealed class HarvestResourceStepController : TutorialStepController
    {
        private PointerManager pointerManager;
        
        private NavigationManager navigationManager;

        private ScreenTransform screenTransform;
        
        private readonly HarvestResourceInspector inspector = new();

        [SerializeField]
        private HarvestResourceConfig config;

        [SerializeField]
        private HarvestResourcePanelShower panelShower = new();

        [SerializeField]
        private Transform pointerTransform;

        public override void ConstructGame(GameContext context)
        {
            this.pointerManager = context.GetService<PointerManager>();
            this.navigationManager = context.GetService<NavigationManager>();
            this.screenTransform = context.GetService<ScreenTransform>();

            var heroService = context.GetService<IHeroService>();
            this.inspector.Construct(heroService, this.config);
            this.panelShower.Construct(this.config);

            base.ConstructGame(context);
        }

        protected override string DescriptionForTutorialAnalytics => config.title;
        
        protected override void OnStart()
        {
            base.OnStart();
            this.inspector.Inspect(callback: this.NotifyAboutCompleteAndMoveNext);
            var position = this.pointerTransform.position;
            this.pointerManager.ShowPointer(position, this.pointerTransform.rotation);
            this.navigationManager.StartLookAt(position);
            this.panelShower.Show(this.screenTransform.Value);
        }

        protected override void OnStop()
        {
            base.OnStop();
            this.navigationManager.Stop();
            this.panelShower.Hide();
            this.pointerManager.HidePointer();
        }
    }
}