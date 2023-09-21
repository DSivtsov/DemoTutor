using Game.GameEngine;
using Game.Localization;
using Game.Meta;
using UnityEngine;

namespace Game.Tutorial
{
    [CreateAssetMenu(
        fileName = "Config «Get Reward»",
        menuName = "Tutorial/Config «Get Reward»"
    )]
    public sealed class GetRewardConfig : ScriptableObject, IPanelConfig
    {
        [Header("Quest")]
        [SerializeField]
        public MissionConfig targetMission;
        
        [SerializeField]
        public WorldPlaceType worldPlaceType =  WorldPlaceType.TAVERN;
    
        [Header("Meta")]
        [TranslationKey]
        [SerializeField]
        public string title = "GET REWARD";

        [SerializeField]
        public Sprite icon;

        string IPanelConfig.Title => title;

        Sprite IPanelConfig.Icon => icon;
    }
}