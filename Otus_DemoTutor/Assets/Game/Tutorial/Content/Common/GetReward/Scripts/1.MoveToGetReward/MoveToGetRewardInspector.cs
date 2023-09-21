using System;
using Game.GameEngine;
using Game.Gameplay.Player;

namespace Game.Tutorial
{
    public sealed class MoveToGetRewardInspector
    {
        private WorldPlaceVisitInteractor worldPlaceVisitor;

        private GetRewardConfig config;

        private Action callback;

        public void Construct(WorldPlaceVisitInteractor worldPlaceVisitor, GetRewardConfig config)
        {
            this.worldPlaceVisitor = worldPlaceVisitor;
            this.config = config;
        }

        public void Inspect(Action callback)
        {
            this.callback = callback;
            this.worldPlaceVisitor.OnVisitStarted += this.OnPlaceVisited;
        }
        
        private void OnPlaceVisited(WorldPlaceType placeType)
        {
            if (placeType == this.config.worldPlaceType)
            {
                this.CompleteQuest();
            }
        }

        private void CompleteQuest()
        {
            this.worldPlaceVisitor.OnVisitStarted -= this.OnPlaceVisited;
            this.callback?.Invoke();
        }
    }
}