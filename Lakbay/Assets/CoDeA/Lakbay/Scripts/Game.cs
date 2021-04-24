using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace CoDeA.Lakbay {
    public class Game : Utilities.ExtendedMonoBehaviour {
        private static float _lastTimeScale = 0.0f;
        public static new bool paused {
            get => Time.timeScale == 0.0f;
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
            SceneManager.LoadScene(sceneBuildIndex);

        }

        public static void sample(Modules.PlayerModule.PlayerController pc) {
            print(pc);
        }

    }

}
