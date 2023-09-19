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
        //private IComponent_ConveyorZone zoneChanges;
        private ConveyorVisitInteractor.Zone zone;
        private ResourceStorage resourceStorage;

        public void Construct(ConveyorVisitInteractor conveyorVisitInteractor, ExchangeResourceConveyorConfig config,
            ResourceStorage storage)
        {
            this.conveyorVisitInteractor = conveyorVisitInteractor;
            this.config = config;
            this.resourceStorage = storage;
            
            if (this.config.exchangeType == ExchangeType.Put)
                zone = this.conveyorVisitInteractor.InputZone;
            else
                zone = this.conveyorVisitInteractor.OutputZone;
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
            
            //Debug.Log($"targetResourceType={this.config.targetResourceType} resourceTypeZone={resourceTypeZone}");
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