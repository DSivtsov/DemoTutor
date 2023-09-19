using System;
using Entities;
using Game.GameEngine.GameResources;
using Game.Gameplay;
using Game.Gameplay.Player;
using UnityEngine;

namespace Game.Tutorial
{
    public sealed class PutResourceToConveyorInspector
    {
        private PutResourceToConveyorConfig config;

        private ConveyorVisitInteractor conveyorVisitInteractor;

        private Action callback;
        private IComponent_LoadZone loadZone;

        public void Construct(ConveyorVisitInteractor conveyorVisitInteractor, PutResourceToConveyorConfig config)
        {
            this.conveyorVisitInteractor = conveyorVisitInteractor;
            this.config = config;
        }

        public void Inspect(Action callback)
        {
            this.callback = callback;
            this.conveyorVisitInteractor.InputZone.OnEntered += InputZoneOnOnEntered;
        }

        private void InputZoneOnOnEntered(IEntity entity)
        {
            this.loadZone = entity?.Get<IComponent_LoadZone>();
            if (loadZone != null)
            {
                //Debug.Log($"[PutResourceToConveyorInspector]: InputZoneOnOnEntered() entity.Get<IComponent_LoadZone>()[{loadZone}]");
                this.loadZone.OnAmountChanged += ComponentLoadZoneOnOnAmountChanged;
            }
            else
                throw new NotImplementedException("[PutResourceToConveyorInspector]: Can't get loadzone from conveyor");
        }

        private void ComponentLoadZoneOnOnAmountChanged(int amount)
        {
            if (amount > 0 && loadZone.ResourceType == ResourceType.WOOD)
            {
                // Debug.Log($"[PutResourceToConveyorInspector]: ComponentLoadZoneOnOnAmountChanged() amount={amount}" +
                //           $" ResourceType is WOOD");
                this.CompleteQuest();
            }
        }

        private void CompleteQuest()
        {
            this.loadZone.OnAmountChanged -= ComponentLoadZoneOnOnAmountChanged;
            this.conveyorVisitInteractor.InputZone.OnEntered -= InputZoneOnOnEntered;
            this.callback?.Invoke();
        }
    }
}