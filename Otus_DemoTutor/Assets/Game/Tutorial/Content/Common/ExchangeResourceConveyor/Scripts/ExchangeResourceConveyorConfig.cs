using Game.GameEngine.GameResources;
using Game.Localization;
using UnityEngine;

namespace Game.Tutorial
{
    public enum ExchangeType
    {
        Put = 0,
        Get = 1,
    }
    
    [CreateAssetMenu(
        fileName = "Tutorial Step «ExchangeResourceConveyor»",
        menuName = "Tutorial/New Tutorial Step «Exchange Resource with Conveyor»"
    )]
    public sealed class ExchangeResourceConveyorConfig : ScriptableObject, IPanelConfig
    {
        [Header("Quest")]
        [SerializeField]
        public ExchangeType exchangeType = ExchangeType.Put;
        
        [SerializeField]
        public ResourceType targetResourceType = ResourceType.WOOD;
    
        [Header("Meta")]
        [TranslationKey]
        [SerializeField]
        public string title = "Exchange Resource with Conveyor";

        [SerializeField]
        public Sprite icon;
        
        string IPanelConfig.Title => title;

        Sprite IPanelConfig.Icon => icon;
    }
}