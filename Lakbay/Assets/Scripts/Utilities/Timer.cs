using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace Utilities {

    public class Timer : Utilities.ExtendedMonoBehaviour {
        public interface ITimer {
            public void onRun(Utilities.Timer timer);
            public void onPause(Utilities.Timer timer);
            public void onResume(Utilities.Timer timer);
            public void onStart(Utilities.Timer timer);
            public void onStop(Utilities.Timer timer);

        }

        public enum RunningStatus {
            PAUSED, RUNNING
        }

        public enum InitialStatus {
            STARTED, STOPPED
        }

        private float _defaultTimeScale = 1.0f;
        private RunningStatus _runningStatus = RunningStatus.RUNNING;
        private InitialStatus _initialStatus = InitialStatus.STARTED;

        public RunningStatus runningStatus {
            get => this._runningStatus;
        }
        public InitialStatus initialStatus {
            get => this._initialStatus;
        }

        public bool runOnPlay = true;
        public float timeRemaining = 0.0f;
        public float timeDuration = 5.0f;

        public float progress {
            get => 1.0f - (this.timeRemaining / this.timeDuration);
        }

        public List<ITimer> onCallbacks = new List<ITimer>();

        private void Start() {
            this._defaultTimeScale = this.timeScale;

            if(this.runOnPlay) this.start();

        }

        private void Update() {
            this.run();

        }

        public void run() {
            if(this.initialStatus == InitialStatus.STARTED) {
                this.timeRemaining = Mathf.Max(this.timeRemaining - (Time.deltaTime * this.timeScale), 0.0f);

            }

            if(this.timeRemaining == 0.0f) this.stop();

            foreach(ITimer callback in this.onCallbacks) callback.onRun(this);

        }

        public void start() {
            this.timeScale = this._defaultTimeScale;
            this.timeRemaining = this.timeDuration;

            this._initialStatus = InitialStatus.STARTED;

            foreach(ITimer callback in this.onCallbacks) callback.onStart(this);

        }

        public void stop() {
            this.timeRemaining = 0.0f;

            this._initialStatus = InitialStatus.STOPPED;

            foreach(ITimer callback in this.onCallbacks) callback.onStop(this);

        }

        public void pause() {
            if(this.initialStatus == InitialStatus.STOPPED) return;

            this.timeScale = 0.0f;
            this._runningStatus = RunningStatus.PAUSED;

            foreach(ITimer callback in this.onCallbacks) callback.onPause(this);

        }

        public void resume() {
            if(this.initialStatus == InitialStatus.STOPPED) return;

            this.timeScale = this._defaultTimeScale;
            this._runningStatus = RunningStatus.RUNNING;

            foreach(ITimer callback in this.onCallbacks) callback.onResume(this);

        }

    }

}
