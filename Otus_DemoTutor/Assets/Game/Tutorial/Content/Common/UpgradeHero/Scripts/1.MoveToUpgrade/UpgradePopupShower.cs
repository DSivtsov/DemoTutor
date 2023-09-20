using System;
using Windows;
using Game.Tutorial.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Tutorial
{
    [Serializable]
    public sealed class UpgradePopupShower
    {
        private PopupManager popupManager;
        
        [SerializeField]
        private AssetReference popupPrefab;

        private UpgradeHeroConfig upgradeConfig;

        public void Construct(PopupManager popupManager, UpgradeHeroConfig upgradeConfig)
        {
            this.upgradeConfig = upgradeConfig;
            this.popupManager = popupManager;
        }
        
        public async void ShowPopup()
        {
            var handle = this.popupPrefab.LoadAssetAsync<GameObject>();
            await handle.Task;
            GameObject handleResult = handle.Result;
            
            var popupController = handleResult.GetComponentInChildren<UpgradePopupController>();
            //field config SerializedField,ReadOnly
            popupController.InitConfig(this.upgradeConfig);
            
            var popup = handleResult.GetComponent<MonoWindow>();
            this.popupManager.Show(popup, args: null);   
        }
    }
}