using System.Collections;
using System.Collections.Generic;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using UnityEngine;
using System.Linq;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
using SharpConfig;

namespace CoDe_A_Old.Lakbay.Modules.GameModule {
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

        // private IEnumerator Start() {
        //     yield return LocalizationSettings.InitializationOperation;

        //     PreferencesController._DEFAULT_PATH = Application.persistentDataPath + $"/{DEFAULT_FILENAME}";
        //     PreferencesController.setDefault();
            
        //     try {
        //         print($"[PREFERENCES] Finding `{DEFAULT_FILENAME}`...");
        //         PreferencesController.load();
        //         print($"[PREFERENCES] Loaded `{DEFAULT_FILENAME}`.");
                
        //     } catch {
        //         print($"[PREFERENCES] Failed to find `{DEFAULT_FILENAME}`.");
        //         print($"[PREFERENCES] Creating `{DEFAULT_FILENAME}`.");
        //         PreferencesController.save();
        //         print($"[PREFERENCES] Created `{DEFAULT_FILENAME}`.");

        //     }

        // }

        // private async Task Awake() {


        // }

        public static async Task setUp() {
            await LocalizationSettings.InitializationOperation.Task;

            PreferencesController._DEFAULT_PATH = Application.persistentDataPath + $"/{DEFAULT_FILENAME}";
            PreferencesController.setDefault();
            
            try {
                print($"[PREFERENCES] Finding `{DEFAULT_FILENAME}`...");
                PreferencesController.load();
                print($"[PREFERENCES] Loaded `{DEFAULT_FILENAME}`.");
                
            } catch {
                print($"[PREFERENCES] Failed to find `{DEFAULT_FILENAME}`.");
                print($"[PREFERENCES] Creating `{DEFAULT_FILENAME}`.");
                PreferencesController.save();
                PreferencesController.load();
                print($"[PREFERENCES] Created `{DEFAULT_FILENAME}`.");

            }

        }

        public static void setDefault() {
            PreferencesController.preferences["Audio"]["MasterVolume"].FloatValue = 0.5f;
            PreferencesController.preferences["Audio"]["MusicVolume"].FloatValue = 1.0f;
            PreferencesController.preferences["Audio"]["SoundVolume"].FloatValue = 1.0f;

            PreferencesController.preferences["Video"]["QualityLevel"].IntValue = 2;
            PreferencesController.preferences["Video"]["LandscapeAutoRotation"].IntValue = 2;

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
            return LocalizationSettings.SelectedLocale.LocaleName;

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

        public static void setLandscapeAutoRotation(int index) {
            bool left = true, right = true;

            if(index == 0) right = false;
            else if(index == 1) left = false;

            setLandscapeAutoRotation(left, right);

        }

        public static void setLandscapeAutoRotation(bool left, bool right) {
            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;

            Screen.autorotateToLandscapeLeft = left;
            Screen.autorotateToLandscapeRight = right;

            // print(Screen.autorotateToLandscapeLeft + " " + Screen.autorotateToLandscapeRight + " " + getLandscapeAutoRotation());

            preferences["Video"]["LandscapeAutoRotation"].IntValue = getLandscapeAutoRotation();
            save();

        }

        public static int getLandscapeAutoRotation() {
            bool left = Screen.autorotateToLandscapeLeft, right = Screen.autorotateToLandscapeRight;
            int index = 2;

            if(left && !right) index = 0;
            if(!left && right) index = 1;
            else if(left && right) index = 2;

            // print($"index: {index}");

            return index;

        }

        public static void setVolume(string name, float value) {
            AudioController.setVolume(name, value);

            preferences["Audio"][name[0].ToString().ToUpper() + name.Substring(1)].FloatValue = value;
            save();

        }

        public static void setMasterVolume(float value) {
            setVolume(AudioController.MASTER_VOLUME_PARAMETER, value);

        }

        public static void setMusicVolume(float value) {
            setVolume(AudioController.MUSIC_VOLUME_PARAMETER, value);

        }

        public static void setSoundVolume(float value) {
            setVolume(AudioController.SOUND_VOLUME_PARAMETER, value);

        }

        public static void save(string path) {
            print("saving...");
            PreferencesController.preferences.SaveToFile(path);

        }

        public static void save() {
            string path = PreferencesController.DEFAULT_PATH;
            PreferencesController.save(path);

        }

        public static void load(string path) {
            PreferencesController.preferences = Configuration.LoadFromFile(path);
            Configuration c = PreferencesController.preferences;

            // print("mastervolume: " + c["Audio"]["MasterVolume"].FloatValue);
            PreferencesController.setMasterVolume(c["Audio"]["MasterVolume"].FloatValue);
            PreferencesController.setMusicVolume(c["Audio"]["MusicVolume"].FloatValue);
            PreferencesController.setSoundVolume(c["Audio"]["SoundVolume"].FloatValue);

            PreferencesController.setQualityLevel(c["Video"]["QualityLevel"].IntValue);
            PreferencesController.setLandscapeAutoRotation(c["Video"]["LandscapeAutoRotation"].IntValue);

            PreferencesController.setLocale(c["Accessibility"]["LocaleName"].StringValue);
            
        }

        public static void load() {
            string path = PreferencesController.DEFAULT_PATH;
            PreferencesController.load(path);

        }

    }

}
