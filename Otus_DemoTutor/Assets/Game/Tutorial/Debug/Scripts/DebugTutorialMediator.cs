#if UNITY_EDITOR
using Game.Tutorial.App;
using JetBrains.Annotations;
using Services;

namespace Game.Tutorial.Development
{
    [UsedImplicitly]
    public sealed class DebugTutorialMediator : TutorialMediator
    {
        protected override void SetupData()
        {
            var debugConfig = DebugTutorialConfig.Instance;
            if (!debugConfig.isDebug)
            {
                base.SetupData();
                return;
            }

            var assetSuppier = ServiceLocator.GetService<TutorialAssetSupplier>();
            var stepList = assetSuppier.LoadStepList();

            var isCompleted = debugConfig.isCompleted;
            var stepIndex = stepList.IndexOf(debugConfig.currentStep);

            this.tutorialManager.Initialize(isCompleted, stepIndex);
        }
    }
}
#endif