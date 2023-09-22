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

        private GetRewardStepController getRewardStepController;

        public void Construct(PopupManager popupManager, GetRewardStepController getRewardStepController)
        {
            this.getRewardStepController = getRewardStepController;
            this.popupManager = popupManager;
        }
        
        public async void ShowPopup()
        {
            var handle = this.popupPrefab.LoadAssetAsync<GameObject>();
            await handle.Task;
            GameObject handleResult = handle.Result;
            
            var popupController = handleResult.GetComponentInChildren<GetRewardPopupController>();
            //set value in SerializedField,ReadOnly field
            popupController.LinkPopupPrefabWithStepController(this.getRewardStepController);
            
            var popup = handleResult.GetComponent<MonoWindow>();
            this.popupManager.Show(popup, args: null);   
        }
    }
}