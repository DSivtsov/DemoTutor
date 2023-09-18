using Game.GameEngine.GameResources;
using Game.Localization;
using UnityEngine;

namespace Game.Tutorial
{
    [CreateAssetMenu(
        fileName = "Tutorial Step «Put Resource To Conveyor»",
        menuName = "Tutorial/New Tutorial Step «Put Resource To Conveyor»"
    )]
    public sealed class PutResourceToConveyorConfig : ScriptableObject, IPanelConfig
    {
        [Header("Quest")]
        [SerializeField]
        public ResourceType targetResourceType = ResourceType.WOOD;
    
        [Header("Meta")]
        [TranslationKey]
        [SerializeField]
        public string title = "Put Resource To Conveyor";

        [SerializeField]
        public Sprite icon;
        
        string IPanelConfig.Title => title;

        Sprite IPanelConfig.Icon => icon;
    }
}