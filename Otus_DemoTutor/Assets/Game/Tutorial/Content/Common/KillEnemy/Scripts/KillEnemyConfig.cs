using Game.Localization;
using UnityEngine;

namespace Game.Tutorial
{
    [CreateAssetMenu(
        fileName = "Config «Kill Enemy»",
        menuName = "Tutorial/New Config «Kill Enemy»"
    )]
    public sealed class KillEnemyConfig : ScriptableObject, IPanelConfig
    {
        [Header("Meta")]
        [TranslationKey]
        [SerializeField]
        public string title = "KILL ENEMY";

        [SerializeField]
        public Sprite icon;

        string IPanelConfig.Title => title;

        Sprite IPanelConfig.Icon => icon;
    }
}