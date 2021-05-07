using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace CoDeA.Lakbay.Modules.GameModule {
    public class GameController : MonoBehaviour {
        public enum Mode { NON_PRO, PRO }

        public static DatabaseModule.Mode currentMode = null;
        public static Mode currentModeType;
        public static DatabaseModule.LinearPlay.Stage linearPlayStage = null;

        private void Awake() {
            if(DatabaseModule.DatabaseController.nonProMode == null) {
                DatabaseModule.DatabaseController.loadNonProMode();

            }

            if(DatabaseModule.DatabaseController.proMode == null) {
                DatabaseModule.DatabaseController.loadProMode();

            }

            if(SceneManager.GetActiveScene().buildIndex == 0) {
                GameController.DontDestroyOnLoad(this.gameObject);
                GameController.loadScene(1);

            } else {

            }

        }

        public static void setMode(Mode mode) {
            switch(mode) {
                case(Mode.NON_PRO): {
                    GameController.currentMode = DatabaseModule.DatabaseController.nonProMode;
                    break;

                } case(Mode.PRO): {
                    GameController.currentMode = DatabaseModule.DatabaseController.nonProMode;
                    break;

                }

            }

            GameController.currentModeType = mode;
            GameController.linearPlayStage = null;
            GameController.forwardLinearPlayStage();

        }

        public static void setMode(int mode) {
            GameController.setMode((Mode) mode);

        }

        public static void loadScene(int sceneBuildIndex) {
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);

        }

        public static DatabaseModule.LinearPlay.Stage forwardLinearPlayStage() {
            if(GameController.linearPlayStage == null) {
                GameController.linearPlayStage = GameController.currentMode.linearPlay.stages[0];
                return GameController.linearPlayStage;

            }

            int ci = GameController.currentMode.linearPlay.stages.IndexOf(GameController.linearPlayStage);
            int tl = GameController.currentMode.linearPlay.stages.Count;

            if(ci == tl - 1) {
                GameController.linearPlayStage = null;
                return null;

            }

            GameController.linearPlayStage = GameController.currentMode.linearPlay.stages[ci + 1];

            return GameController.linearPlayStage;

        }

    }

}
