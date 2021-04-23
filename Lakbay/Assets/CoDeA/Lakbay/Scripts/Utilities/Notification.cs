using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine.Events;
using UnityEngine;

namespace CoDeA.Lakbay.Utilities {
    public class Notification : ExtendedMonoBehaviour {
        [HideInInspector]
        public CanvasGroup canvasGroup;

        public static readonly float NORMAL_DURATION = 2.0f;

        public TMP_Text text;
        public Timer timer;

        private void Awake() {
            this.canvasGroup = this.GetComponent<CanvasGroup>();
            this.timer.startOnPlay = false;
            this.timer.onStop.AddListener(this.hide);

            this.hide();

        }

        public void show(string text, float duration) {
            this.timer.timeRemaining = duration;
            this.timer.timeDuration = duration;
            this.text.SetText(text);
            this.canvasGroup.alpha = 1.0f;
            this.timer.start();

        }

        public void show(string text) {
            float duration = text.Length / Utilities.Helper.READING_CHARACTER_PER_SECOND;
            duration += 1.0f;
            this.show(text, duration);

        }
        
        public void hide() {
            this.text.SetText("");
            this.canvasGroup.alpha = 0.0f;

        }

    }

}
