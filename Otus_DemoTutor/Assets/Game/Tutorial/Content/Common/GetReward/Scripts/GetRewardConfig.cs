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
        public MissionConfig missionConfig;
        
        public UpgradeConfig upgradeConfig;
        
        [SerializeField]
        public WorldPlaceType worldPlaceType =  WorldPlaceType.TAVERN;
        
        [SerializeField]
        public int targetLevel = 3;
    
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