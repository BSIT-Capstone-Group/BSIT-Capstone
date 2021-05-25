using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.AddressableAssets;
using CoDe_A.Lakbay.Modules.LinearPlayModule;
using UnityEngine.Localization;
using System;

namespace CoDe_A.Lakbay.Modules.GameModule {
    [System.Serializable]
    public class Data {
        public static T parseYAML<T>(TextAsset textAsset) {
            return Utilities.Helper.parseYAML<T>(textAsset.text);

        }

        public static T parseYAML<T>(String stringText) {
            return Utilities.Helper.parseYAML<T>(stringText);

        }

    }

    [System.Serializable]
    public class ModeData : Data {
        public LinearPlayData linearPlayData = null;

    }

    [System.Serializable]
    public class PlayData : Data {
        // public string name = "";

    }

    [System.Serializable]
    public class LinearPlayData : PlayData {
        [System.Serializable]
        public class Level : Data {
            public TextAsset roadFile;
            public TextAsset setFile;
            public LocalizedAsset<TextAsset> localizedSetFile;

            public LinearPlayModule.RoadModule.Road parseRoadFile() {
                return Data.parseYAML<LinearPlayModule.RoadModule.Road>(this.roadFile);

            }

        }

        public TextAsset playerFile;
        public TextAsset vehicleFile;
        public List<Level> levels = new List<Level>();

        public LinearPlayModule.PlayerModule.Player parsePlayerFile() {
            return Data.parseYAML<LinearPlayModule.PlayerModule.Player>(this.playerFile);

        }

        public VehicleModule.Vehicle parseVehicleFile() {
            return Data.parseYAML<VehicleModule.Vehicle>(this.vehicleFile);

        }

    }

    public class DatabaseController : MonoBehaviour {
        private static List<TextAsset> textFiles = new List<TextAsset>();
        private static List<Sprite> images = new List<Sprite>();
        private static List<AudioClip> audios = new List<AudioClip>();

        public static ModeData nonProModeData;
        public static ModeData proModeData;

        // private async Task Start() {

        // }

        public static async Task setUp() {

            Task<ModeData> md = DatabaseController.loadNonProMode();
            await md;
            DatabaseController.nonProModeData = md.Result;

            md = DatabaseController.loadProMode();
            await md;
            DatabaseController.proModeData = md.Result;

        }

        public static async Task<ModeData> loadMode(string modeName) {
            ModeData modeData = new ModeData();

            LinearPlayData linearPlayData = new LinearPlayData();
            List<LinearPlayData.Level> levels = new List<LinearPlayData.Level>();
            linearPlayData.levels = levels;

            modeData.linearPlayData = linearPlayData;

            AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(
                $"Text Files/Modes/{modeName}/Linear Play/player.yaml"
            );

            await handle.Task;
            linearPlayData.playerFile = handle.Result;

            handle = Addressables.LoadAssetAsync<TextAsset>(
                $"Text Files/Modes/{modeName}/Linear Play/vehicle.yaml"
            );
            await handle.Task;
            linearPlayData.vehicleFile = handle.Result;

            for(int i = 1; i <= 3; i++) {
                Task<LinearPlayData.Level> t = DatabaseController.loadLinearPlayLevel(modeName, i);
                await t;
                levels.Add(
                   t.Result
                );

            }

            return modeData;

        }

        public static async Task<LinearPlayData.Level> loadLinearPlayLevel(string modeName, int levelNumber) {
            string mmodeName = modeName == "Non-Pro" ? "nonPro" : "pro";

            LinearPlayData.Level level = new LinearPlayData.Level();

            AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(
                $"Text Files/Modes/{modeName}/Linear Play/Levels/{levelNumber}/road.yaml"
            );

            await handle.Task;
            level.roadFile = handle.Result;

            level.localizedSetFile = new LocalizedAsset<TextAsset>();
            level.localizedSetFile.AssetChanged += DatabaseController._parseSetFile(level);
            level.localizedSetFile.SetReference("Localized Assets Table", $"{mmodeName}LinearPlayLevel{levelNumber}SetFile");

            return level;

        }

        private static LocalizedAsset<TextAsset>.ChangeHandler _parseSetFile(LinearPlayData.Level level) {
            return (ta) => {
                level.setFile = ta;

            };

        }

        public static async Task<ModeData> loadNonProMode() {
            Task<ModeData> m = DatabaseController.loadMode("Non-Pro");
            await m;
            return m.Result;

        }

        public static async Task<ModeData> loadProMode() {
            Task<ModeData> m = DatabaseController.loadMode("Pro");
            await m;
            return m.Result;

        }

        public static AsyncOperationHandle<T> loadAsset<T>(IResourceLocation path) {
            return Addressables.LoadAssetAsync<T>(path);

        }
        public static AsyncOperationHandle<T> loadAsset<T>(string path) {
            return Addressables.LoadAssetAsync<T>(path);

        }

        public static AsyncOperationHandle<IList<T>> loadAssets<T>(string path, Action<T> callback) {
            return Addressables.LoadAssetsAsync<T>(path, callback);

        }

        public static AsyncOperationHandle<IList<T>> loadAssets<T>(string path) {
            return loadAssets<T>(path, (t) => {});

        }

        public static AsyncOperationHandle<IList<IResourceLocation>> getAddresses(string path) {
            return Addressables.LoadResourceLocationsAsync(path);
            // await loadAssetsWithAddresses<Sprite>("Images");
        }

        public static async Task<Dictionary<string, T>> loadAssetsWithAddresses<T>(string path) {
            Dictionary<string, T> rvs = new Dictionary<string, T>();

            var locations = await getAddresses(path).Task;
            foreach(var loc in locations) {
                var asset = await loadAsset<T>(loc.PrimaryKey).Task;
                rvs[loc.PrimaryKey] = asset;

            }

            // await new Task<Dictionary<string, T>>(() => rvs);
            return rvs;

        }

    }

}
