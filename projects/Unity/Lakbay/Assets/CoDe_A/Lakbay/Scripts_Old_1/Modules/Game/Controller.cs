using System;
using Ph.Com.CoDe_A.Lakbay.Utilities;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Ph.Com.CoDe_A.Lakbay.Modules.Game {
    public interface IController : Core.IController {
        // float timeScale { get; set; }
        // bool isPaused { get; set; }
        // Controller.Event events { get; set; }

        // void Pause(MonoBehaviour mono=null, float duration=0.0f, Action onResume=null);
        // void Resume();

    }

    /// <summary>Used to control mostly anything related to the game.</summary>
    public class Controller : Core.Controller, IController {
        [Serializable]
        public struct Event {
            public UnityEvent onPause;
            public UnityEvent onResume;

        }

        // public float timeScale { get; set; }

        private static float _lastStaticTimeScale = 1.0f;
        public static float staticTimeScale {
            get => Time.timeScale;
            set {
                float t = Time.timeScale;
                if(value < 0.0f || value == t) return;
                _lastStaticTimeScale = t > 0.0f ? t : _lastStaticTimeScale;
                Time.timeScale = value;

                if(Time.timeScale == 0.0f) events.onPause?.Invoke();
                if(t == 0.0f && Time.timeScale > 0.0f) events.onResume?.Invoke();

            }

        }
        public static bool isPaused => staticTimeScale <= 0.0f;

        // bool timeScale { get; set; }
        // bool isPaused { get; set; }
        protected static Event _events = new Event();
        public static Event events { get => _events; set => _events = value; }

        public static void Pause(MonoBehaviour mono=null, float duration=0.0f, Action onResume=null) {
            if(!mono) {
                mono = new GameObject(
                    $"Pause - {duration}"
                ).AddComponent<Core.Controller>();

            }

            if(isPaused) return;
            staticTimeScale = 0.0f;

            if(duration > 0.0f) {
                Helper.DoOver(
                    mono: mono,
                    duration: duration,
                    onStop: () => {
                        Resume();
                        onResume?.Invoke();

                    }
                );

            }

        }

        public static void Resume() {
            if(!isPaused) return;
            staticTimeScale = _lastStaticTimeScale;

        }

    }

}
