using Game.GameEngine.GameResources;
using Game.Localization;
using UnityEngine;

namespace Game.Tutorial
{
    [CreateAssetMenu(
        fileName = "Tutorial Step «Get Resource From Conveyor»",
        menuName = "Tutorial/New Tutorial Step «Get Resource From  Conveyor»"
    )]
    public sealed class GetResourceFromConveyorConfig : ScriptableObject, IPanelConfig
    {
        [Header("Quest")]
        [SerializeField]
        public ResourceType targetResourceType = ResourceType.WOOD;
    
        [Header("Meta")]
        [TranslationKey]
        [SerializeField]
        public string title = "Get Resource From Conveyor";

        [SerializeField]
        public Sprite icon;
        
        string IPanelConfig.Title => title;

        Sprite IPanelConfig.Icon => icon;
    }
}