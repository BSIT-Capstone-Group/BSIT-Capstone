using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using TMPro;

namespace CoDe_A.Lakbay.Modules.MainMenuModule.UIModule {
    public class UIController : MonoBehaviour {
        // Source: https://docs.unity3d.com/Packages/com.unity.localization@1.0/manual/Scripting.html
        public TMP_Dropdown languageDropdown;

        private IEnumerator Start() {
            // Wait for the localization system to initialize
            yield return LocalizationSettings.InitializationOperation;

            // Generate list of available Locales
            var options = new List<TMP_Dropdown.OptionData>();
            int selected = 0;
            for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; ++i)
            {
                var locale = LocalizationSettings.AvailableLocales.Locales[i];
                if (LocalizationSettings.SelectedLocale == locale)
                    selected = i;
                options.Add(new TMP_Dropdown.OptionData(locale.LocaleName));
            }
            this.languageDropdown.options = options;

            this.languageDropdown.value = selected;
            this.languageDropdown.onValueChanged.AddListener(GameModule.GameController.setLocale);
            
        }

    }

}
