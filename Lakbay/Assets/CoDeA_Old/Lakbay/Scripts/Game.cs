using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using CoDeA_Old.Lakbay.Modules.RoadModule;
using CoDeA_Old.Lakbay.Modules.QuestionModule;
using CoDeA_Old.Lakbay.Modules.PlayerModule;
using CoDeA_Old.Lakbay.Modules.VehicleModule;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
// using UnityEditor;

namespace CoDeA_Old.Lakbay {
    public class Game : Utilities.ExtendedMonoBehaviour {
        [System.Serializable]
        public class Stage {
            public TextAsset roadFile;
            public TextAsset setFile;
            // public List<Sprite> images = new List<Sprite>();
            public Dictionary<string, Sprite> images = new Dictionary<string, Sprite>();

        }

        [System.Serializable]
        public class ModeData {
            public TextAsset playerFile;
            public TextAsset vehicleFile;

            public List<Stage> stages = new List<Stage>();

            public Stage stage = null;

            public Stage forwardStage() {
                if(this.stages.Count <= 0) return null;

                if(this.stage == null) {
                    this.stage = this.stages[0];
                    return this.stage;

                }

                int ci = this.stages.IndexOf(this.stage);

                if(ci != this.stages.Count - 1) {
                    this.stage = this.stages[ci + 1];

                } else {
                    this.stage = null;

                }

                return this.stage;

            }

            public void resetStage() {
                this.stage = null;

            }

        }

        public enum Mode {
            NON_PRO, PRO

        }

        public static class Loader {
            public static readonly string PATH_SEPARATOR = Utilities.FileManager.PATH_SEPARATOR;
            public static readonly string MODE_NON_PRO_PATH = "Non-Pro";
            public static readonly string MODE_PRO_PATH = "Pro";
            public static readonly string MODES_PATH = String.Join(
                PATH_SEPARATOR, "Assets", "CoDeA", "Lakbay", "Res", "Modes"
            );

            public static bool loadedModes = false;

            public static void loadScene(int sceneBuildIndex) {
                SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);

            }

            public static string[] getPaths(Game.Mode mode) {
                string path = (mode == Game.Mode.PRO) ? Loader.MODE_PRO_PATH : Loader.MODE_NON_PRO_PATH;
                string fullPath = String.Join(Loader.PATH_SEPARATOR, Loader.MODES_PATH, path);
                string stagesPath = String.Join(Loader.PATH_SEPARATOR, fullPath, "Stages");
                string playerPath = String.Join(Loader.PATH_SEPARATOR, fullPath, "player.yaml");
                string vehiclePath = String.Join(Loader.PATH_SEPARATOR, fullPath, "vehicle.yaml");

                return new string[] {playerPath, vehiclePath, stagesPath};

            }

            public static async Task loadPaths(
                ModeData modeData,
                string playerPath, string vehiclePath, string stagesPath
            ) {
                var handle = Addressables.LoadAssetAsync<TextAsset>(playerPath);
                handle.Completed += (h) => { modeData.playerFile = h.Result; };
                await handle.Task;
                // yield return handle;
                
                handle = Addressables.LoadAssetAsync<TextAsset>(vehiclePath);
                handle.Completed += (h) => { modeData.vehicleFile = h.Result; };
                await handle.Task;
                // yield return handle;
                
                int i = 1;
                while(i != 4) {
                    string roadPath = String.Join(Loader.PATH_SEPARATOR, stagesPath, i, "road.yaml");
                    string setPath = String.Join(Loader.PATH_SEPARATOR, stagesPath, i, "set.yaml");
                    string imagesPath = String.Join(Loader.PATH_SEPARATOR, stagesPath, i, "Images");

                    Stage stage = new Stage();

                    handle = Addressables.LoadAssetAsync<TextAsset>(roadPath);
                    handle.Completed += (h) => { stage.roadFile = h.Result; };
                    await handle.Task;
                    // yield return handle;

                    handle = Addressables.LoadAssetAsync<TextAsset>(setPath);
                    handle.Completed += (h) => { stage.setFile = h.Result; };
                    await handle.Task;

                    foreach(string imgpath in Utilities.FileManager.getImagesFromProject(imagesPath)) {
                        string[] paths = imgpath.Split(Utilities.FileManager.PATH_SEPARATOR[0]);
                        string fn = paths[paths.Length - 1];
                        string rimgpath = String.Join(Utilities.FileManager.PATH_SEPARATOR, imagesPath, fn);

                        var shandle = Addressables.LoadAssetAsync<Sprite>(rimgpath);
                        shandle.Completed += (h) => { stage.images.Add(rimgpath, h.Result); };
                        await shandle.Task;

                    }

                    modeData.stages.Add(stage);

                    i++;

                }

            }

            public static async Task loadPaths(ModeData modeData, params string[] paths) {
                await Loader.loadPaths(modeData, paths[0], paths[1], paths[2]);

            }

            public static async Task loadModes() {
                if(Loader.loadedModes) return;

                Game.modeDatas.Clear();
                ModeData np = new ModeData();
                ModeData p = new ModeData();

                string[] nppaths = Loader.getPaths(Mode.NON_PRO);
                string[] ppaths = Loader.getPaths(Mode.PRO);

                await Loader.loadPaths(np, nppaths);
                await Loader.loadPaths(p, nppaths);

                Game.modeDatas.Add(np);
                Game.modeDatas.Add(p);
                
                Loader.loadedModes = true;

            }

        }

        public static readonly string DEVELOPER = "CoDeA";

        private static float _lastTimeScale = 0.0f;
        public static new bool paused {
            get => Time.timeScale == 0.0f;
        }
        public static bool debugMode = true;

        public static Mode mode = Mode.NON_PRO;
        public static ModeData modeData;
        public static List<ModeData> modeDatas = new List<ModeData>();
        public List<ModeData> _modeDatas;

        private async Task Awake() {
            await Game.Loader.loadModes();

            if(SceneManager.GetActiveScene().buildIndex == 0) {
                Game.DontDestroyOnLoad(this.gameObject);
                Game.Loader.loadScene(1);

            } else {

            }

        }

        private void Start() {

        }

        // private async Task Start() {
        //     await Game.Loader.loadModes();

        // }

        private void Update() {

        }
        

        public static void setMode(int mode) {
            Game.setMode((Mode) mode);

        }

        public static void setMode(Mode mode) {
            ModeData modeData = Game.modeDatas[(int) mode];
            Game.modeData = modeData;
            Game.mode = mode;
            Game.modeData.resetStage();
            Game.modeData.forwardStage();

        }

        public static void pause() {
            if(Game.paused) return;

            Game._lastTimeScale = Time.timeScale;
            Time.timeScale = 0.0f;

        }

        public static void resume() {
            if(!Game.paused) return;

            Time.timeScale = Game._lastTimeScale;

        }

        public static void loadScene(int sceneBuildIndex) {
            Loader.loadScene(sceneBuildIndex);

        }

    }

}
