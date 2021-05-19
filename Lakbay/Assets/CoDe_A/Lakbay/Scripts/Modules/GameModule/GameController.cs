using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
using TMPro;
// using UnityEngine.Localization.Settings;

namespace CoDe_A.Lakbay.Modules.GameModule {
    public class GameController : MonoBehaviour {
        public enum Mode { NON_PRO, PRO }

        public static DatabaseModule.ModeData currentModeData = null;
        public static Mode currentModeType;
        public static DatabaseModule.LinearPlayData.Level currentLinearPlayLevel = null;

        public Slider loadingSlider;
        public TMP_Text loadingText;

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
                try {
                    await DatabaseModule.DatabaseController.setUp();
                    if(this.loadingSlider) this.loadingSlider.value = 1 / 3.0f;
                    await AudioController.setUp();
                    if(this.loadingSlider) this.loadingSlider.value = 1 / 2.0f;
                    await PreferencesController.setUp();
                    if(this.loadingSlider) this.loadingSlider.value = 1 / 1.0f;

                } catch (System.Exception e) {
                    if(this.loadingText) this.loadingText.SetText(e.ToString());
                    throw e;

                }

                GameController.DontDestroyOnLoad(this.gameObject);
                GameController.loadScene(1);

            } else {

            }
            
            return;

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
