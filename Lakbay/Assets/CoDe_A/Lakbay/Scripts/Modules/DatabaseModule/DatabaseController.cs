using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using CoDe_A.Lakbay.Modules.LinearPlayModule;
using UnityEngine.Localization;

namespace CoDe_A.Lakbay.Modules.DatabaseModule {
    // [System.Serializable]
    // public class Mode {
    //     public string name = "";

    //     public LinearPlay linearPlay = null;

    // }

    // [System.Serializable]
    // public class Play {
    //     // public string name = "";

    // }

    // [System.Serializable]
    // public class LinearPlay : Play {
    //     [System.Serializable]
    //     public class Stage {
    //         public LocalizedAsset<TextAsset> roadFile;
    //         public LocalizedAsset<TextAsset> setFile;

    //     }

    //     public LocalizedAsset<TextAsset> playerFile;
    //     public LocalizedAsset<TextAsset> vehicleFile;
    //     public List<Stage> stages = new List<Stage>();

    // }

    // public class DatabaseController : MonoBehaviour {
    //     public static Mode nonProMode = null;
    //     public static Mode proMode = null;

    //     // private async Task Start() {
    //     //     await DatabaseController.loadNonProMode();
    //     //     await DatabaseController.loadProMode();

    //     // }

    //     public static async Task<Mode> loadMode(string modeName) {
    //         LocalizedAsset<TextAsset> player = null;

    //         AsyncOperationHandle<LocalizedAsset<TextAsset>> handle = Addressables.LoadAssetAsync<LocalizedAsset<TextAsset>>(
    //             $"Text Files/Modes/{modeName}/Linear Play/player.yaml"
    //         );

    //         await handle.Task;
    //         // print("1 - " + handle.Task.Result);
    //         player = handle.Result;

    //         LocalizedAsset<TextAsset> vehicle = null;

    //         handle = Addressables.LoadAssetAsync<LocalizedAsset<TextAsset>>(
    //             $"Text Files/Modes/{modeName}/Linear Play/vehicle.yaml"
    //         );

    //         await handle.Task;
    //         // print("2 - " + handle.Task.Result);
    //         vehicle = handle.Result;

    //         List<LinearPlay.Stage> stages = new List<LinearPlay.Stage>();

    //         for(int i = 1; i <= 3; i++) {
    //             Task<LinearPlay.Stage> t = DatabaseController.loadLinearPlayStage(modeName, i);
    //             await t;
    //             stages.Add(
    //                t.Result
    //             );

    //             // print("---> " + i);
    //             // print(t.Result.set.items.Count);

    //         }

    //         LinearPlay linearPlay = new LinearPlay();
    //         linearPlay.playerFile = player;
    //         linearPlay.stages = stages;
    //         linearPlay.vehicleFile = vehicle;

    //         Mode mode = new Mode();
    //         mode.name = $"{modeName}";
    //         mode.linearPlay = linearPlay;

    //         return mode;

    //     }

    //     // public static LinearPlay.Stage loadLinearPlayStage(string modeName, int stageNumber) {
    //     //     LinearPlay.Stage stage = new LinearPlay.Stage();
    //     //     stage.road = Utilities.Helper.parseYAML<LinearPlayModule.RoadModule.Road>(
    //     //         Resources.Load<TextAsset>(
    //     //             $"Text Files/Modes/{modeName}/Linear Play/Levels/{stageNumber}/road"
    //     //         ).text
    //     //     );
    //     //     stage.set = Utilities.Helper.parseYAML<LinearPlayModule.QuestionModule.Set>(
    //     //         Resources.Load<TextAsset>(
    //     //             $"Text Files/Modes/{modeName}/Linear Play/Levels/{stageNumber}/set"
    //     //         ).text
    //     //     );

    //     //     return stage;

    //     // }

    //     public static async Task<LinearPlay.Stage> loadLinearPlayStage(string modeName, int stageNumber) {
    //         LinearPlay.Stage stage = new LinearPlay.Stage();

    //         AsyncOperationHandle<LocalizedAsset<TextAsset>> handle = Addressables.LoadAssetAsync<LocalizedAsset<TextAsset>>(
    //             $"Text Files/Modes/{modeName}/Linear Play/Levels/{stageNumber}/road.yaml"
    //         );

    //         await handle.Task;
    //         stage.roadFile = handle.Result;

    //         handle = Addressables.LoadAssetAsync<LocalizedAsset<TextAsset>>(
    //             $"Text Files/Modes/{modeName}/Linear Play/Levels/{stageNumber}/set.yaml"
    //         );

    //         await handle.Task;
    //         stage.setFile = handle.Result;

    //         // Task<LinearPlay.Stage> t = Task<LinearPlay.Stage>.Factory.StartNew(
    //         //     () => {
    //         //         return stage;
    //         //     }
    //         // );

    //         // await t;

    //         return stage;

    //     }

    //     // public static void loadNonProMode() {
    //     //     DatabaseController.nonProMode = DatabaseController.loadMode("Non-Pro");

    //     // }

    //     // public static void loadProMode() {
    //     //     DatabaseController.proMode = DatabaseController.loadMode("Pro");

    //     // }

    //     public static async Task<Mode> loadNonProMode() {
    //         Task<Mode> m = DatabaseController.loadMode("Non-Pro");
    //         await m;
    //         return m.Result;

    //     }

    //     public static async Task<Mode> loadProMode() {
    //         Task<Mode> m = DatabaseController.loadMode("Pro");
    //         await m;
    //         return m.Result;

    //     }



    [System.Serializable]
    public class Mode {
        public string name = "";

        public LinearPlay linearPlay = null;

    }

    [System.Serializable]
    public class Play {
        // public string name = "";

    }

    [System.Serializable]
    public class LinearPlay : Play {
        [System.Serializable]
        public class Level {
            // public string name = "";

            public LinearPlayModule.RoadModule.Road road;
            public LinearPlayModule.QuestionModule.Set set;

            public LocalizedAsset<TextAsset> localizedSetFile = new LocalizedAsset<TextAsset>();

        }

        // public new string name = "Linear Play";

        public LinearPlayModule.PlayerModule.Player player = null;
        public VehicleModule.Vehicle vehicle = null;
        public List<Level> levels = new List<Level>();

    }

    public class DatabaseController : MonoBehaviour {
        public static Mode nonProMode = null;
        public static Mode proMode = null;

        private async Task Start() {
            await DatabaseController.loadNonProMode();
            await DatabaseController.loadProMode();

        }

        // public static Mode loadMode(string modeName) {
        //     LinearPlayModule.PlayerModule.Player player = null;
        //     player = Utilities.Helper.parseYAML<LinearPlayModule.PlayerModule.Player>(
        //         Resources.Load<TextAsset>(
        //             $"Text Files/Modes/{modeName}/Linear Play/player"
        //         ).text
        //     );

        //     player = Utilities.Helper.parseYAML<LinearPlayModule.PlayerModule.Player>(
        //         Resources.Load<TextAsset>(
        //             $"Text Files/Modes/{modeName}/Linear Play/player"
        //         ).text
        //     );

        //     VehicleModule.Vehicle vehicle = null;
        //     vehicle = Utilities.Helper.parseYAML<VehicleModule.Vehicle>(
        //         Resources.Load<TextAsset>(
        //             $"Text Files/Modes/{modeName}/Linear Play/vehicle"
        //         ).text
        //     );

        //     List<LinearPlay.Stage> stages = new List<LinearPlay.Stage>();

        //     for(int i = 1; i <= 3; i++) {
        //         stages.Add(
        //             DatabaseController.loadLinearPlayStage(modeName, i)
        //         );

        //     }

        //     LinearPlay linearPlay = new LinearPlay();
        //     linearPlay.player = player;
        //     linearPlay.stages = stages;
        //     linearPlay.vehicle = vehicle;

        //     Mode mode = new Mode();
        //     mode.name = $"{modeName}";
        //     mode.linearPlay = linearPlay;

        //     return mode;

        // }

        public static async Task<Mode> loadMode(string modeName) {
            LinearPlayModule.PlayerModule.Player player = null;

            AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(
                $"Text Files/Modes/{modeName}/Linear Play/player.yaml"
            );

            await handle.Task;
            // print("1 - " + handle.Task.Result);
            player = Utilities.Helper.parseYAML<LinearPlayModule.PlayerModule.Player>(handle.Result.text);

            VehicleModule.Vehicle vehicle = null;

            handle = Addressables.LoadAssetAsync<TextAsset>(
                $"Text Files/Modes/{modeName}/Linear Play/vehicle.yaml"
            );

            await handle.Task;
            // print("2 - " + handle.Task.Result);
            vehicle = Utilities.Helper.parseYAML<VehicleModule.Vehicle>(handle.Result.text);

            List<LinearPlay.Level> stages = new List<LinearPlay.Level>();

            for(int i = 1; i <= 3; i++) {
                Task<LinearPlay.Level> t = DatabaseController.loadLinearPlayStage(modeName, i);
                await t;
                stages.Add(
                   t.Result
                );

                print("---> " + i);
                print(t.Result.set.items.Count);

            }

            LinearPlay linearPlay = new LinearPlay();
            linearPlay.player = player;
            linearPlay.levels = stages;
            linearPlay.vehicle = vehicle;

            Mode mode = new Mode();
            mode.name = $"{modeName}";
            mode.linearPlay = linearPlay;

            return mode;

        }

        // public static LinearPlay.Stage loadLinearPlayStage(string modeName, int stageNumber) {
        //     LinearPlay.Stage stage = new LinearPlay.Stage();
        //     stage.road = Utilities.Helper.parseYAML<LinearPlayModule.RoadModule.Road>(
        //         Resources.Load<TextAsset>(
        //             $"Text Files/Modes/{modeName}/Linear Play/Levels/{stageNumber}/road"
        //         ).text
        //     );
        //     stage.set = Utilities.Helper.parseYAML<LinearPlayModule.QuestionModule.Set>(
        //         Resources.Load<TextAsset>(
        //             $"Text Files/Modes/{modeName}/Linear Play/Levels/{stageNumber}/set"
        //         ).text
        //     );

        //     return stage;

        // }

        public static async Task<LinearPlay.Level> loadLinearPlayStage(string modeName, int levelNumber) {
            LinearPlay.Level level = new LinearPlay.Level();

            AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(
                $"Text Files/Modes/{modeName}/Linear Play/Levels/{levelNumber}/road.yaml"
            );

            await handle.Task;
            level.road = Utilities.Helper.parseYAML<LinearPlayModule.RoadModule.Road>(handle.Result.text);

            handle = Addressables.LoadAssetAsync<TextAsset>(
                $"Text Files/Modes/{modeName}/Linear Play/Levels/{levelNumber}/set_eng.yaml"
            );

            await handle.Task;
            level.set = Utilities.Helper.parseYAML<LinearPlayModule.QuestionModule.Set>(handle.Result.text);

            level.localizedSetFile.SetReference("Asset Table", $"{modeName} - Linear Play - Level {levelNumber} - Set File");
            level.localizedSetFile.AssetChanged += (ta) => {
                level.set = Utilities.Helper.parseYAML<LinearPlayModule.QuestionModule.Set>(ta.text);

            };

            return level;

        }

        // public static void loadNonProMode() {
        //     DatabaseController.nonProMode = DatabaseController.loadMode("Non-Pro");

        // }

        // public static void loadProMode() {
        //     DatabaseController.proMode = DatabaseController.loadMode("Pro");

        // }

        private UnityAction<TextAsset> _parseSetFile(LinearPlay.Level stage) {
            return (ta) => {
                stage.set = Utilities.Helper.parseYAML<LinearPlayModule.QuestionModule.Set>(ta.text);
            };

        }

        public static async Task<Mode> loadNonProMode() {
            Task<Mode> m = DatabaseController.loadMode("Non-Pro");
            await m;
            return m.Result;

        }

        public static async Task<Mode> loadProMode() {
            Task<Mode> m = DatabaseController.loadMode("Pro");
            await m;
            return m.Result;

        }

    }

}
