using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Tutorial.App
{
    public sealed class TutorialAssetSupplierWood
    {
        private const string ENGINE_KEY = "WoodTutorialEngine";

        private const string INTERFACE_KEY = "CommonTutorialInterface";

        private const string STEP_CONFIG_PATH = "TutorialStepConfigWood";

        public TutorialList LoadStepList()
        {
            return Resources.Load<TutorialList>(STEP_CONFIG_PATH);
        }

        public async Task<GameObject> LoadTutorialEngine()
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(ENGINE_KEY);
            await handle.Task;
            return handle.Result;
        }

        public async Task<GameObject> LoadTutorialInterface()
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(INTERFACE_KEY);
            await handle.Task;
            return handle.Result;
        }
    }
}