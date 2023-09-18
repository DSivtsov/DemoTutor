using System;
using Game.App;
using Game.Localization;
using Game.Tutorial.Gameplay;
using Game.Tutorial.UI;
using UnityEngine;

namespace Game.Tutorial
{
    [Serializable]
    public sealed class TutorialStepPanelShower : InfoPanelShower
    {
        private IPanelConfig iPanelConfig;

        public void Construct(IPanelConfig iPanelConfig)
        {
            this.iPanelConfig = iPanelConfig;
        }

        protected override void OnShow()
        {
            var title = LocalizationManager.GetCurrentText(this.iPanelConfig.Title);
            this.view.SetTitle(title);
            this.view.SetIcon(this.iPanelConfig.Icon);

            LanguageManager.OnLanguageChanged += this.OnLanguageChanged;
        }

        protected override void OnHide()
        {
            LanguageManager.OnLanguageChanged -= this.OnLanguageChanged;
        }

        private void OnLanguageChanged(SystemLanguage language)
        {
            var title = LocalizationManager.GetText(this.iPanelConfig.Title, language);
            this.view.SetTitle(title);
        }
    }

    public interface IPanelConfig
    {
        string Title { get; }
        Sprite Icon { get; }
    }
}