using System;
using Windows;
using Game.Tutorial.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Tutorial
{
    [Serializable]
    public sealed class GetRewardPopupShower
    {
        private PopupManager popupManager;
        
        [SerializeField]
        private AssetReference popupPrefab;

        private GetRewardConfig getRewardConfig;

        public void Construct(PopupManager popupManager, GetRewardConfig getRewardConfig)
        {
            this.getRewardConfig = getRewardConfig;
            this.popupManager = popupManager;
        }
        
        public async void ShowPopup()
        {
            var handle = this.popupPrefab.LoadAssetAsync<GameObject>();
            await handle.Task;
            GameObject handleResult = handle.Result;
            
            var popupController = handleResult.GetComponentInChildren<GetRewardPopupController>();
            //field config SerializedField,ReadOnly
            popupController.InitConfig(this.getRewardConfig);
            
            var popup = handleResult.GetComponent<MonoWindow>();
            this.popupManager.Show(popup, args: null);   
        }
    }
}