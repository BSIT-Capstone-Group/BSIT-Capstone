using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace CoDeA.Lakbay.Utilities {
    public class Timer : ExtendedMonoBehaviour {
        public enum RunningStatus {
            PAUSED, RUNNING

        }

        public enum InitialStatus {
            STARTED, STOPPED

        }

        private float _defaultTimeScale = 1.0f;
        private RunningStatus _runningStatus = RunningStatus.RUNNING;
        private InitialStatus _initialStatus = InitialStatus.STOPPED;

        public RunningStatus runningStatus {
            get => this._runningStatus;
        }
        public InitialStatus initialStatus {
            get => this._initialStatus;
        }

        public bool startOnPlay = true;
        public float timeRemaining = 0.0f;
        public float timeDuration = 5.0f;

        public float progress {
            get => 1.0f - (this.timeRemaining / this.timeDuration);

        }
        
        public UnityEvent<Timer> onRun = new UnityEvent<Timer>();
        public UnityEvent<Timer> onPause = new UnityEvent<Timer>();
        public UnityEvent<Timer> onResume = new UnityEvent<Timer>();
        public UnityEvent<Timer> onStart = new UnityEvent<Timer>();
        public UnityEvent<Timer> onStop = new UnityEvent<Timer>();

        private void Awake() {
            this._defaultTimeScale = this.timeScale;

            if(this.startOnPlay) this.start();

        }

        private void Update() {
            this.run();

        }

        public void run() {
            if(this.initialStatus == InitialStatus.STARTED) {
                // print("running...");
                this.timeRemaining = Mathf.Max(this.timeRemaining - (Time.deltaTime * this.timeScale), 0.0f);

                if(this.timeRemaining == 0.0f) this.stop();

            }

            if(this.initialStatus != InitialStatus.STOPPED) this.onRun.Invoke(this);

        }

        public void start() {
            this.timeScale = this._defaultTimeScale;
            this.timeRemaining = this.timeDuration;

            this._initialStatus = InitialStatus.STARTED;

            this.onStart.Invoke(this);

        }

        public void stop() {
            this.timeRemaining = 0.0f;

            this._initialStatus = InitialStatus.STOPPED;

            this.onStop.Invoke(this);

        }

        public void pause() {
            if(this.initialStatus == InitialStatus.STOPPED) return;

            this.timeScale = 0.0f;
            this._runningStatus = RunningStatus.PAUSED;

            this.onPause.Invoke(this);

        }

        public void resume() {
            if(this.initialStatus == InitialStatus.STOPPED) return;

            this.timeScale = this._defaultTimeScale;
            this._runningStatus = RunningStatus.RUNNING;

            this.onResume.Invoke(this);

        }

    }

}