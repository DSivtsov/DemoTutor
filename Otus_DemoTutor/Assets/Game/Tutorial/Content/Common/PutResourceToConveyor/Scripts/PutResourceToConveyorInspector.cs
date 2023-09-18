using System;
using Game.Gameplay.Player;

namespace Game.Tutorial
{
    public sealed class PutResourceToConveyorInspector
    {
        private PutResourceToConveyorConfig config;

        private VendorInteractor sellInteractor;

        private Action callback;

        public void Construct(VendorInteractor sellInteractor, PutResourceToConveyorConfig config)
        {
            this.sellInteractor = sellInteractor;
            this.config = config;
        }

        public void Inspect(Action callback)
        {
            this.callback = callback;
            this.sellInteractor.OnResourcesSold += this.OnResourcesSold;
        }

        private void OnResourcesSold(VendorSellResult result)
        {
            if (result.resourceType == this.config.targetResourceType)
            {
                this.CompleteQuest();
            }
        }

        private void CompleteQuest()
        {
            this.sellInteractor.OnResourcesSold -= this.OnResourcesSold;
            this.callback?.Invoke();
        }
    }
}