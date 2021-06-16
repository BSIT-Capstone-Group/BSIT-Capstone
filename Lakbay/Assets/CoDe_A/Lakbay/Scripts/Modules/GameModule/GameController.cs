using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CoDe_A.Lakbay.Modules.CoreModule;
using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.Modules.GameModule {
    /// <summary>Used to control mostly anything related to the game.</summary>
    public class GameController : Controller {
        /// <summary>Contains the <see cref="Time.timeScale"/> before the game is paused.</summary>
        private static float _lastTimeScale = 0.0f;

        /// <summary>Determines whether the game is paused or not.</summary>
        public static bool paused {
            get => _lastTimeScale == 0.0f;
            set { if(value) pause(0.0f); else resume(); }

        }    

        /// <summary>Pauses the game for a certain duration.</summary>
        public static void pause(float duration) {
            if(paused) return;

            _lastTimeScale = Time.timeScale;
            Time.timeScale = 0.0f;

            if(duration > 0.0f) {
                Timer.quickStart(duration).onStop.AddListener((t_, d) => {
                    GameController.resume();
                    Destroy(t_.gameObject);
                });

            }

        }

        /// <summary>Pauses the game indefinitely.</summary>
        public static void pause() => pause(0.0f);

        /// <summary>Resumes the game if its paused.</summary>
        public static void resume() {
            if(!paused) return;

            Time.timeScale = _lastTimeScale;

        }

        private void Awake() {

        }

    }

}
