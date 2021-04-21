using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace CoDeA.Lakbay {
    public class Game : Utilities.ExtendedMonoBehaviour {
        private static float _lastTimeScale = 0.0f;

        public static void pause() {
            Game._lastTimeScale = Time.timeScale;
            Time.timeScale = 0.0f;

        }

        public static void resume() {
            Time.timeScale = Game._lastTimeScale;

        }

        public static void loadScene(int sceneBuildIndex) {
            SceneManager.LoadScene(sceneBuildIndex);

        }

    }

}
