using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using CoDeA.Lakbay.Modules.RoadModule;
using CoDeA.Lakbay.Modules.QuestionModule;
using CoDeA.Lakbay.Modules.PlayerModule;
using CoDeA.Lakbay.Modules.VehicleModule;
using UnityEngine.AddressableAssets;
// using UnityEditor;

namespace CoDeA.Lakbay {
    [System.Serializable]
    public class Stage {
        public TextAsset road;
        public TextAsset set;
        public List<Sprite> images;

    }

    [System.Serializable]
    public class ModeData {
        public TextAsset playerFile;
        public TextAsset vehicleFile;

        public List<Stage> stages;

        public Stage stage = null;

        public ModeData(Game.Mode mode) {
            string path = (mode == Game.Mode.PRO) ? Game.MODE_PRO_PATH : Game.MODE_NON_PRO_PATH;
            string fpath = String.Join(Game.DIR_SEP, Game.MODES_PATH, path);
            string spath = String.Join(Game.DIR_SEP, fpath, "Stages");
            string ipath = String.Join(Game.DIR_SEP, spath, "Images");

            // Addressables.LoadAssetAsync<TextAsset>(
            //     String.Join(Game.DIR_SEP, fpath, "player.yaml")
            // ).Completed += (h) => {
            //     this.playerFile = h.Result;

            // };

            // this.playerFile = Resources.Load<TextAsset>(String.Join(Game.DIR_SEP, "player"));
            // this.vehicleFile = Resources.Load<TextAsset>(String.Join(Game.DIR_SEP, "vehicle"));

            // Addressables.LoadAssetAsync<TextAsset>(
            //     String.Join(Game.DIR_SEP, fpath, "vehicle.yaml")
            // ).Completed += (h) => {
            //     this.vehicleFile = h.Result;

            // };

            // List<string> stage_paths = new List<string>(AssetDatabase.GetSubFolders(
            //         spath
            //     ).OrderBy<string, int>((string stage) => {
            //         string[] parts = stage.Split(Game.DIR_SEP.ToCharArray()[0]);
            //         string fn = parts[parts.Count() - 1];

            //         return Convert.ToInt32(fn);
            //     })
            // );

            // this.stages.Clear();

            // foreach(string sp in stage_paths) {
            //     this.stages.Add(
            //         new Tuple<TextAsset, TextAsset>(
            //             AssetDatabase.LoadAssetAtPath<TextAsset>(
            //                 String.Join(Game.DIR_SEP, new string[] {
            //                     sp, "road.yaml"
            //                 })
            //             ),
            //             AssetDatabase.LoadAssetAtPath<TextAsset>(
            //                 String.Join(Game.DIR_SEP, new string[] {
            //                     sp, "set.yaml"
            //                 })
            //             )
                        
            //         )
            //     );

            // }

            if(this.stages.Count > 0) {
                this.stage = this.stages[0];

            }

            // this.playerFile = AssetDatabase.LoadAssetAtPath<TextAsset>(
            //     String.Join(Game.DIR_SEP, fpath, "player.yaml")
            // );

            // this.vehicleFile = AssetDatabase.LoadAssetAtPath<TextAsset>(
            //     String.Join(Game.DIR_SEP, fpath, "vehicle.yaml")
            // );

            // List<string> stage_paths = new List<string>(AssetDatabase.GetSubFolders(
            //         spath
            //     ).OrderBy<string, int>((string stage) => {
            //         string[] parts = stage.Split(Game.DIR_SEP.ToCharArray()[0]);
            //         string fn = parts[parts.Count() - 1];

            //         return Convert.ToInt32(fn);
            //     })
            // );

            // this.stages.Clear();

            // foreach(string sp in stage_paths) {
            //     this.stages.Add(
            //         new Tuple<TextAsset, TextAsset>(
            //             AssetDatabase.LoadAssetAtPath<TextAsset>(
            //                 String.Join(Game.DIR_SEP, new string[] {
            //                     sp, "road.yaml"
            //                 })
            //             ),
            //             AssetDatabase.LoadAssetAtPath<TextAsset>(
            //                 String.Join(Game.DIR_SEP, new string[] {
            //                     sp, "set.yaml"
            //                 })
            //             )
                        
            //         )
            //     );

            // }

            // if(this.stages.Count > 0) {
            //     this.stage = this.stages[0];

            // }

        }

        public Stage forwardStage() {
            int ci = this.stages.IndexOf(this.stage);

            if(ci != this.stages.Count - 1) {
                this.stage = this.stages[ci + 1];

            } else {
                this.stage = null;

            }

            return this.stage;

        }

    }

    public class Game : Utilities.ExtendedMonoBehaviour {
        public enum Mode {
            NON_PRO, PRO

        }

        public static readonly string DIR_SEP = "/";
        public static readonly string DEVELOPER = "CoDeA";

        public static readonly string MODE_NON_PRO_PATH = "Non-Pro";
        public static readonly string MODE_PRO_PATH = "Pro";
        public static readonly string MODES_PATH = String.Join(
            "/",
            new string[] {
                "Assets", "CoDeA", "Lakbay", "Res", "Modes"
            }
        );

        private static float _lastTimeScale = 0.0f;
        public static new bool paused {
            get => Time.timeScale == 0.0f;
        }

        public static Mode mode = Mode.NON_PRO;
        public static ModeData modeData;
        public static List<ModeData> modeDatas = null;
        public List<ModeData> _modeDatas;

        private void Awake() {


            if(SceneManager.GetActiveScene().buildIndex == 0) {
                Game.DontDestroyOnLoad(this.gameObject);
                Game.loadScene(1);

            } else {

            }

        }

        private void Start() {
            if(Game.modeDatas == null) {
                Game.modeDatas = this._modeDatas;
                

            }
            // print(this._modeDatas.Count + " hhehe");

        }

        private void Update() {
            // Game.modeDatas = this._modeDatas;

        }

        public static void setMode(int mode) {
            Game.setMode((Mode) mode);

        }

        public static void setMode(Mode mode) {
            // Game.modeData = new Lakbay.ModeData(mode);
            Game.modeData = Game.modeDatas[(int) mode];
            Game.mode = mode;
            Game.modeData.stage = Game.modeData.stages[0];

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
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);

        }

    }

}
