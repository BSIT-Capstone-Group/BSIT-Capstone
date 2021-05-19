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
        public TMP_Dropdown qualityDropdown;
        public Toggle leftAutoRotationToggle;
        public Toggle rightAutoRotationToggle;

        public Slider masterVolumeSlider;
        public Slider musicVolumeSlider;
        public Slider soundVolumeSlider;

        public TMP_Text masterVolumeText;
        public TMP_Text musicVolumeText;
        public TMP_Text soundVolumeText;

        private IEnumerator Start() {
            yield return this.setUpLanguageDropdown();

            this.setUpQualityDropdown();
            this.setUpAutoRotationToggles();
            this.setUpVolumeSliders();
            
        }

        public IEnumerator setUpLanguageDropdown() {
            LocalizationSettings.SelectedLocaleChanged += (l) => {
                int i = LocalizationSettings.AvailableLocales.Locales.IndexOf(l);
                this.languageDropdown.value = i;

            };

            // Wait for the localization system to initialize
            yield return LocalizationSettings.InitializationOperation;

            // Generate list of available Locales
            var options = new List<TMP_Dropdown.OptionData>();
            int selected = 0;

            for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; i++)
            {
                var locale = LocalizationSettings.AvailableLocales.Locales[i];
                if (LocalizationSettings.SelectedLocale == locale)
                    selected = i;
                options.Add(new TMP_Dropdown.OptionData(locale.LocaleName));
            }
            this.languageDropdown.options = options;

            this.languageDropdown.value = selected;
            this.languageDropdown.onValueChanged.AddListener(GameModule.PreferencesController.setLocale);

        }

        public void setUpQualityDropdown() {
            string[] qualityLevelNames = GameModule.PreferencesController.getQualityLevels();
            var options = new List<TMP_Dropdown.OptionData>();
            int selected = 0;

            for (int i = 0; i < qualityLevelNames.Length; i++) {
                string qualityLevelName = qualityLevelNames[i];
                if (GameModule.PreferencesController.getQualityLevel() == i) selected = i;

                options.Add(new TMP_Dropdown.OptionData(qualityLevelName));
            }
            this.qualityDropdown.options = options;

            this.qualityDropdown.value = selected;
            this.qualityDropdown.onValueChanged.AddListener(GameModule.PreferencesController.setQualityLevel);

        }

        public void setUpAutoRotationToggles() {
            int ar = GameModule.PreferencesController.getLandscapeAutoRotation();
            this.leftAutoRotationToggle.isOn = true;
            this.rightAutoRotationToggle.isOn = true;

            if(ar == 0) {
                this.rightAutoRotationToggle.isOn = false;

            } else if(ar == 1) {
                this.leftAutoRotationToggle.isOn = false;

            }

            this.leftAutoRotationToggle.onValueChanged.AddListener(this.setLeftAutoRotation);
            this.rightAutoRotationToggle.onValueChanged.AddListener(this.setRightAutoRotation);

        }

        public void setUpVolumeSliders() {
            this.masterVolumeSlider.onValueChanged.AddListener(
                GameModule.PreferencesController.setMasterVolume
            );
            this.musicVolumeSlider.onValueChanged.AddListener(
                GameModule.PreferencesController.setMusicVolume
            );
            this.soundVolumeSlider.onValueChanged.AddListener(
                GameModule.PreferencesController.setSoundVolume
            );

            this.masterVolumeSlider.onValueChanged.AddListener(
                (v) => { this.masterVolumeText.SetText((v * 100.0f).ToString("N0") + "%"); }
                // (v) => { this.masterVolumeText.SetText(v.ToString()); }
            );
            this.musicVolumeSlider.onValueChanged.AddListener(
                (v) => { this.musicVolumeText.SetText((v * 100.0f).ToString("N0") + "%"); }
            );
            this.soundVolumeSlider.onValueChanged.AddListener(
                (v) => { this.soundVolumeText.SetText((v * 100.0f).ToString("N0") + "%"); }
            );

            this.masterVolumeSlider.value = GameModule.AudioController.getMasterVolume();
            this.musicVolumeSlider.value = GameModule.AudioController.getMusicVolume();
            this.soundVolumeSlider.value = GameModule.AudioController.getSoundVolume();

        }

        public void setLeftAutoRotation(bool value) {
            if(!value && !this.rightAutoRotationToggle.isOn) this.leftAutoRotationToggle.isOn = true;

            // this.rightAutoRotationToggle.isOn = false;
            GameModule.PreferencesController.setLandscapeAutoRotation(
                this.leftAutoRotationToggle.isOn, this.rightAutoRotationToggle.isOn
            );

            print(this.leftAutoRotationToggle.isOn + " " + this.rightAutoRotationToggle.isOn);

        }

        public void setRightAutoRotation(bool value) {
            if(!value && !this.leftAutoRotationToggle.isOn) this.rightAutoRotationToggle.isOn = true;

            // this.leftAutoRotationToggle.isOn = false;
            GameModule.PreferencesController.setLandscapeAutoRotation(
                this.leftAutoRotationToggle.isOn, this.rightAutoRotationToggle.isOn
            );

            // print(this.leftAutoRotationToggle.isOn + " " + this.rightAutoRotationToggle.isOn);

        }

    }

}
