using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Threading.Tasks;
// using UnityEngine.Localization.Settings;

namespace CoDe_A.Lakbay.Modules.GameModule {
    public class GameController : MonoBehaviour {
        public enum Mode { NON_PRO, PRO }

        public static DatabaseModule.ModeData currentModeData = null;
        public static Mode currentModeType;
        public static DatabaseModule.LinearPlayData.Level currentLinearPlayLevel = null;

        private async Task Awake() {
            // Task<DatabaseModule.Mode> t = null;
            // if(DatabaseModule.DatabaseController.nonProMode == null) {
            //     t = DatabaseModule.DatabaseController.loadNonProMode();
            //     await t;
            //     DatabaseModule.DatabaseController.nonProMode = t.Result;

            // }

            // Task<string> s = Task<string>.Factory.StartNew(() => "heheh");
            // await s;

            // if(DatabaseModule.DatabaseController.proMode == null) {
            //     t = DatabaseModule.DatabaseController.loadProMode();
            //     await t;
            //     DatabaseModule.DatabaseController.nonProMode = t.Result;

            // }

            if(SceneManager.GetActiveScene().buildIndex == 0) {
                GameController.DontDestroyOnLoad(this.gameObject);
                GameController.loadScene(1);

            } else {

            }

        }

        public static void setMode(Mode mode) {
            switch(mode) {
                case(Mode.NON_PRO): {
                    GameController.currentModeData = DatabaseModule.DatabaseController.nonProModeData;
                    break;

                } case(Mode.PRO): {
                    GameController.currentModeData = DatabaseModule.DatabaseController.nonProModeData;
                    break;

                }

            }

            GameController.currentModeType = mode;
            GameController.currentLinearPlayLevel = null;
            GameController.forwardLinearPlayLevel();

        }

        public static void setMode(int mode) {
            GameController.setMode((Mode) mode);

        }

        public static void loadScene(int sceneBuildIndex) {
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);

        }

        public static DatabaseModule.LinearPlayData.Level forwardLinearPlayLevel() {
            if(GameController.currentLinearPlayLevel == null) {
                GameController.currentLinearPlayLevel = GameController.currentModeData.linearPlayData.levels[0];
                return GameController.currentLinearPlayLevel;

            }

            int ci = GameController.currentModeData.linearPlayData.levels.IndexOf(GameController.currentLinearPlayLevel);
            int tl = GameController.currentModeData.linearPlayData.levels.Count;

            if(ci == tl - 1) {
                GameController.currentLinearPlayLevel = null;
                return null;

            }

            GameController.currentLinearPlayLevel = GameController.currentModeData.linearPlayData.levels[ci + 1];

            return GameController.currentLinearPlayLevel;

        }

    }

}
