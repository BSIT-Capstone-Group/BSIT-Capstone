using CoDe_A.Lakbay.Utilities;

using UnityEngine;
using UnityEngine.Events;

namespace CoDe_A.Lakbay.Modules.Game {
    /// <summary>Used to control mostly anything related to the game.</summary>
    public class Controller : Core.Controller {
        public struct Event {
            public UnityEvent onPause;
            public UnityEvent onResume;

        }

        private static float _lastTimeScale = 1.0f;
        public static float timeScale {
            get => Time.timeScale;
            set {
                float t = Time.timeScale;
                if(value < 0.0f || value == t) return;
                _lastTimeScale = t > 0.0f ? t : _lastTimeScale;
                Time.timeScale = value;

                if(Time.timeScale == 0.0f) events.onPause.Invoke();
                if(t == 0.0f && Time.timeScale > 0.0f) events.onResume.Invoke();

            }

        }
        public static bool isPaused => timeScale <= 0.0f;

        public static Event events = new Event();

        public static void Pause(MonoBehaviour mono, float duration) {
            if(isPaused) return;
            timeScale = 0.0f;

            if(duration > 0.0f) {
                Helper.DoOver(
                    mono: mono,
                    duration: duration,
                    onStop: () => Resume()
                );

            }

        }

        public static void Pause(float duration) {
            Pause(
                new GameObject(
                    $"Pause - {duration}"
                ).AddComponent<MonoBehaviour>(), duration
            );

        }

        public static void Pause() => Pause(0.0f);

        public static void Resume() {
            if(!isPaused) return;
            timeScale = _lastTimeScale;

        }

    }

}
