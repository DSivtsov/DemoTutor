using System;
using Entities;
using Game.GameEngine.GameResources;
using Game.Gameplay;
using Game.Gameplay.Player;

namespace Game.Tutorial
{
    public sealed class ExchangeResourceConveyorInspector
    {
        private ExchangeResourceConveyorConfig config;

        private ConveyorVisitInteractor conveyorVisitInteractor;

        private Action callback;
        private ConveyorVisitInteractor.Zone zone;

        public void Construct(ConveyorVisitInteractor conveyorVisitInteractor, ExchangeResourceConveyorConfig config,
            ResourceStorage storage)
        {
            this.conveyorVisitInteractor = conveyorVisitInteractor;
            this.config = config;
            
            zone = this.config.exchangeType == ExchangeType.Put ? this.conveyorVisitInteractor.InputZone : this.conveyorVisitInteractor.OutputZone;
        }

        public void Inspect(Action callback)
        {
            this.callback = callback;
            this.zone.OnEntered += ZoneOnOnEntered;
        }

        private void ZoneOnOnEntered(IEntity entity)
        {
            ResourceType resourceTypeZone = this.config.exchangeType == ExchangeType.Put
                ? entity.Get<IComponent_LoadZone>().ResourceType : entity.Get<IComponent_UnloadZone>().ResourceType;
            
            if (this.config.targetResourceType == resourceTypeZone)
            {
                this.CompleteQuest();
            }
        }

        private void CompleteQuest()
        {
            this.zone.OnEntered -= ZoneOnOnEntered;
            this.callback?.Invoke();
        }
    }
}