using System.Collections;
using System.Collections.Generic;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using UnityEngine;
using System.Linq;
using SharpConfig;

namespace CoDe_A.Lakbay.Modules.GameModule {
    [System.Serializable]
    public class Preferences {
        [System.Serializable]
        public class Accessibility {
            public string localeName = "English";

            public Accessibility() : this("English") {

            }

            public Accessibility(string localeName) {
                this.localeName = localeName;

            }

        }

        public Accessibility accessibility = new Accessibility();

    }

    public class PreferencesController : MonoBehaviour {
        public static readonly string DEFAULT_FILENAME = "preferences.ini";
        private static string _DEFAULT_PATH = "";
        public static string DEFAULT_PATH { get => _DEFAULT_PATH; }

        // public static Preferences preferences = new Preferences();
        public static Configuration preferences = new Configuration();

        private IEnumerator Start() {
            yield return LocalizationSettings.InitializationOperation;

            PreferencesController._DEFAULT_PATH = Application.persistentDataPath + $"/{DEFAULT_FILENAME}";
            PreferencesController.setDefault();

            print(QualitySettings.names[0]);
            
            try {
                print($"[PREFERENCES] Finding `{DEFAULT_FILENAME}`...");
                PreferencesController.load();
                print($"[PREFERENCES] Loaded `{DEFAULT_FILENAME}`.");
                
            } catch {
                print($"[PREFERENCES] Failed to find `{DEFAULT_FILENAME}`.");
                print($"[PREFERENCES] Creating `{DEFAULT_FILENAME}`.");
                PreferencesController.save();
                print($"[PREFERENCES] Created `{DEFAULT_FILENAME}`.");

            }

        }

        public static void setDefault() {
            PreferencesController.preferences["Video"]["QualityLevel"].IntValue = 2;
            PreferencesController.preferences["Accessibility"]["LocaleName"].StringValue = "English";

        }

        public static void setLocale(string localeName) {
            Locale locale = LocalizationSettings.AvailableLocales.Locales.Find((l) => l.LocaleName.Equals(localeName));
            PreferencesController.setLocale(locale);

        }

        public static void setLocale(int index) {
            if(index < 0 || index >= LocalizationSettings.AvailableLocales.Locales.Count) return;
            
            PreferencesController.setLocale(LocalizationSettings.AvailableLocales.Locales[index]);

        }

        public static void setLocale(Locale locale) {
            LocalizationSettings.SelectedLocale = locale;
            // PreferencesController.preferences.accessibility.localeName = locale.LocaleName;
            PreferencesController.preferences["Accessibility"]["LocaleName"].StringValue = locale.LocaleName;
            PreferencesController.save();

        }

        public static string getLocale() {
            return PreferencesController.preferences["Accessibility"]["LocaleName"].StringValue;

        }

        public static void setQualityLevel(string levelName) {
            int index = getQualityLevels().ToList().IndexOf(levelName); 
            PreferencesController.setQualityLevel(index);

        }

        public static void setQualityLevel(int levelIndex) {
            QualitySettings.SetQualityLevel(levelIndex, true);
            PreferencesController.preferences["Video"]["QualityLevel"].IntValue = levelIndex;
            PreferencesController.save();

        }

        public static string[] getQualityLevels() {
            string[] names = QualitySettings.names;
            return names.ToList().FindAll((s) => names.ToList().IndexOf(s) < 3).ToArray();

        }

        public static int getQualityLevel() {
            return QualitySettings.GetQualityLevel();

        }

        public static void save(string path) {
            PreferencesController.preferences.SaveToFile(path);

        }

        public static void save() {
            string path = PreferencesController.DEFAULT_PATH;
            PreferencesController.save(path);

        }

        public static void load(string path) {
            PreferencesController.preferences = Configuration.LoadFromFile(path);
            Configuration c = PreferencesController.preferences;

            PreferencesController.setQualityLevel(c["Video"]["QualityLevel"].IntValue);
            PreferencesController.setLocale(c["Accessibility"]["LocaleName"].StringValue);
            
        }

        public static void load() {
            string path = PreferencesController.DEFAULT_PATH;
            PreferencesController.load(path);

        }

    }

}
