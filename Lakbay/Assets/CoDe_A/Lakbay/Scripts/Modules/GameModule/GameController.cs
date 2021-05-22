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
        public enum DayPhase { MORNING, AFTERNOON, EVENING };

        public static DayPhase dayPhase = DayPhase.MORNING;
        public static float lastTimeScale = 0.0f;
        public static bool paused => Time.timeScale == 0.0f;
        public static GameModule.ModeData currentModeData = null;
        public static Mode currentModeType;
        public static GameModule.LinearPlayData.Level currentLinearPlayLevel = null;
        public static int currentLinearPlayLevelIndex {
            get {
                if(currentLinearPlayLevel != null && currentModeData != null) {
                    return currentModeData.linearPlayData.levels.IndexOf(currentLinearPlayLevel);

                } else return -1;

            }
        }

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
                    await GameModule.DatabaseController.setUp();
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
                    GameController.currentModeData = GameModule.DatabaseController.nonProModeData;
                    break;

                } case(Mode.PRO): {
                    GameController.currentModeData = GameModule.DatabaseController.nonProModeData;
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

        public static GameModule.LinearPlayData.Level forwardLinearPlayLevel() {
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

        public static void pause() {
            if(paused) return;

            lastTimeScale = Time.timeScale;
            Time.timeScale = 0.0f;

        }

        public static void resume() {
            if(!paused) return;

            Time.timeScale = lastTimeScale;

        }

        public static void setDayPhase(DayPhase dayPhase, Light light) {
            GameController.dayPhase = dayPhase;

            switch(dayPhase) {
                case(DayPhase.MORNING): {
                    RenderSettings.ambientIntensity = 1.0f;
                    RenderSettings.fog = false;

                    if(light) {
                        light.color = Utilities.Color.create("#F5FBC8");
                        light.intensity = 1.0f;
                        light.transform.rotation = Quaternion.Euler(new Vector3(50.0f, 0.0f, 0.0f));

                    }

                    break;

                } case(DayPhase.AFTERNOON): {
                    RenderSettings.ambientIntensity = 1.0f;
                    RenderSettings.fog = false;

                    if(light) {
                        light.color = Utilities.Color.create("#FFE099");
                        light.intensity = 1.0f;
                        light.transform.rotation = Quaternion.Euler(new Vector3(50.0f, 72.0f, 0.0f));

                    }

                    break;

                } case(DayPhase.EVENING): {
                    RenderSettings.ambientIntensity = 0.4f;
                    RenderSettings.fog = true;

                    if(light) {
                        light.color = Utilities.Color.create("#00C0FF");
                        light.intensity = 0.35f;
                        light.transform.rotation = Quaternion.Euler(new Vector3(50.0f, -110.0f, 0.0f));

                    }

                    break;

                }
            }

        }

        public static void setDayPhase(DayPhase dayPhase) => setDayPhase(dayPhase, RenderSettings.sun);
        public static void setDayPhase() {
            if(currentLinearPlayLevelIndex != -1) setDayPhase(currentLinearPlayLevelIndex);
            else setDayPhase(DayPhase.MORNING);

        }

        public static void setDayPhase(int dayPhase, Light light) => setDayPhase((DayPhase) dayPhase, light);
        public static void setDayPhase(int dayPhase) => setDayPhase(dayPhase, RenderSettings.sun);

    }

}
