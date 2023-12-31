using System.Collections.Generic;
using Game.GameEngine;
using Game.GameEngine.InventorySystem;
using GameSystem;
using Windows;
using UnityEngine;

namespace Game.Meta
{
    public sealed class InventoryItemListPresenter : MonoWindow, IGameConstructElement
    {
        [SerializeField]
        private InventoryItemView prefab;

        [SerializeField]
        private Transform container;

        private InventoryService inventoryService;

        private PopupManager popupManager;

        private InventoryItemConsumer consumeManager;

        private readonly Dictionary<InventoryItem, ViewHolder> items = new();

        protected override void OnShow(object args)
        {
            var playerInventory = this.inventoryService.GetInventory();
            playerInventory.OnItemAdded += this.AddItem;
            playerInventory.OnItemRemoved += this.RemoveItem;

            var inventoryItems = playerInventory.GetAllItems();
            for (int i = 0, count = inventoryItems.Length; i < count; i++)
            {
                var inventoryItem = inventoryItems[i];
                this.AddItem(inventoryItem);
            }
        }

        protected override void OnHide()
        {
            var playerInventory = this.inventoryService.GetInventory();
            playerInventory.OnItemAdded -= this.AddItem;
            playerInventory.OnItemRemoved -= this.RemoveItem;

            var inventoryItems = playerInventory.GetAllItems();
            for (int i = 0, count = inventoryItems.Length; i < count; i++)
            {
                var inventoryItem = inventoryItems[i];
                this.RemoveItem(inventoryItem);
            }
        }

        private void AddItem(InventoryItem item)
        {
            if (this.items.ContainsKey(item))
            {
                return;
            }

            var view = Instantiate(this.prefab, this.container);
            var presenter = new InventoryItemViewPresenter(view, item);
            presenter.Construct(this.popupManager, this.consumeManager);

            var viewHolder = new ViewHolder(view, presenter);
            this.items.Add(item, viewHolder);

            presenter.Start();
        }

        private void RemoveItem(InventoryItem item)
        {
            if (!this.items.ContainsKey(item))
            {
                return;
            }

            var viewHolder = this.items[item];
            viewHolder.presenter.Stop();
            Destroy(viewHolder.view.gameObject);
            this.items.Remove(item);
        }

        void IGameConstructElement.ConstructGame(GameContext context)
        {
            this.inventoryService = context.GetService<InventoryService>();
            this.popupManager = context.GetService<PopupManager>();
            this.consumeManager = context.GetService<InventoryItemConsumer>();
        }

        private sealed class ViewHolder
        {
            public readonly InventoryItemView view;
            public readonly InventoryItemViewPresenter presenter;

            public ViewHolder(InventoryItemView view, InventoryItemViewPresenter presenter)
            {
                this.view = view;
                this.presenter = presenter;
            }
        }
    }
}