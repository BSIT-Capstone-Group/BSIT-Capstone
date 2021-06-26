/*
 * Date Created: Saturday, June 26, 2021 6:28 AM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CoDe_A.Lakbay.Utilities {
    public interface ITimer {
        void run();
        void pause();
        void resume();
        void start(float duration, float timeScale);
        void start(float duration);
        void start();
        void stop();

    }

    public class Timer : MonoBehaviour, ITimer {
        public enum MainStatus { STARTED, STOPPED }
        public enum Status { PAUSED, RUNNING }

        private MainStatus _mainStatus = MainStatus.STOPPED;
        private Status _status = Status.PAUSED;
        private float _currentDuration = 0.0f;
        private float _lastTimeScale = 0.0f;

        public MainStatus mainStatus => _mainStatus;
        public Status status => _status;
        public float currentDuration => _currentDuration;
        public bool indefinite => duration == 0.0f;
        public float progress => !indefinite ? 1 - (currentDuration / duration) : 1.0f;
        public bool debug = false;
        public bool startOnAwake = false;
        public float duration = 0.0f;
        public float timeScale = 1.0f;

        public UnityEvent<Timer, float> onRun = new UnityEvent<Timer, float>();
        public UnityEvent<Timer, float> onPause = new UnityEvent<Timer, float>();
        public UnityEvent<Timer, float> onResume = new UnityEvent<Timer, float>();
        public UnityEvent<Timer, float> onStart = new UnityEvent<Timer, float>();
        public UnityEvent<Timer, float> onStop = new UnityEvent<Timer, float>();

        public void start(float duration, float timeScale) {
            if(mainStatus == MainStatus.STARTED) return;
            this.duration = duration;
            this.timeScale = timeScale;

            _mainStatus = MainStatus.STARTED;
            _currentDuration = indefinite ? 0.0f : this.duration;
            _status = this.timeScale > 0.0f ? Status.RUNNING : Status.PAUSED;
            onStart.Invoke(this, currentDuration);

        }

        public void start(float duration) { start(duration, 1.0f); }

        public void start() { start(duration); }

        public void stop() {
            if(mainStatus == MainStatus.STOPPED) return;
            
            _mainStatus = MainStatus.STOPPED;
            onStop.Invoke(this, currentDuration);

        }

        public void pause() {
            if(status == Status.PAUSED) return;

            _status = Status.PAUSED;
            _lastTimeScale = timeScale;
            timeScale = 0.0f;
            onPause.Invoke(this, currentDuration);

        }

        public void resume() {
            if(status == Status.RUNNING) return;

            _status = Status.RUNNING;
            timeScale = _lastTimeScale;
            onResume.Invoke(this, currentDuration);

        }
        
        public void run() {
            if(mainStatus == MainStatus.STARTED) {
                if(status == Status.RUNNING) {
                    if(!indefinite) {
                        _currentDuration = Mathf.Max(_currentDuration - (Time.deltaTime * timeScale), 0.0f);

                        if(_currentDuration == 0.0f) this.stop();

                    } else _currentDuration = _currentDuration + (Time.deltaTime * timeScale);

                    if(debug) {
                        Helper.Log("Timer", new OrderedDictionary() {
                            {"duration", duration},
                            {"currentDuration", currentDuration},
                            {"progress", progress},
                        });

                    }

                    onRun.Invoke(this, currentDuration);

                }

            }

        }
        public static Timer quickStart(float duration, float timeScale, bool start) {
            if(duration > 0.0f && timeScale > 0.0f) {
                GameObject go = new GameObject("Quick Timer");
                Timer t = go.AddComponent<Timer>();
                t.startOnAwake = true;
                t.duration = duration;
                t.timeScale = timeScale;

                if(start) t.start(); t.onStop.AddListener((t, d) => Destroy(go));

                return t;

            }

            return null;

        }

        public static Timer quickStart(float duration, bool start) {
            return quickStart(duration, 1.0f, start);

        }

        public static Timer quickStart(float duration) {
            return quickStart(duration, false);

        }

        private void Awake() {
            if(startOnAwake) start();
            
        }

        private void Update() {
            run();
            
        }

    }

}
