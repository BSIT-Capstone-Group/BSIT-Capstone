/*
 * Date Created: Thursday, September 9, 2021 3:13 PM
 * Author: NI.L.A
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

using TMPro;

using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Behaviours {
    public class Game : Controller {
        protected static bool _initialized = false;
        public static bool initialized => _initialized;

        [SerializeField]
        protected EventAndInput _eventAndInput;
        public static EventAndInput eventAndInput;
        [SerializeField]
        protected Repository _repository;
        public static Repository repository;
        [SerializeField]
        protected LoadingScreen _loadingScreen;
        public static LoadingScreen loadingScreen;

        public override void Awake() {
            base.Awake();
            if(!initialized) {
                DontDestroyOnLoad(gameObject);
                Initialize();
            
            }

        }

        public virtual void Initialize() {
            if(initialized) return;

            if(!FindObjectOfType<EventAndInput>()) {
                var go = Instantiate(_eventAndInput);
                eventAndInput = go.GetComponent<EventAndInput>();
                
            }

            if(!FindObjectOfType<Repository>()) {
                var go = Instantiate(_repository);
                repository = go.GetComponent<Repository>();
                
            }

            if(!FindObjectOfType<LoadingScreen>()) {
                var go = Instantiate(_loadingScreen);
                loadingScreen = go.GetComponent<LoadingScreen>();
                
            }

            LoadAssets();

            _initialized = true;

        }

        public virtual void LoadAssets() {
            var repo = repository;
            if(repo) {
                repo.UnloadAssets();

                var audioClipsLoad = repo.LoadAssetsEnumerator<AudioClip>(
                    onLoad: (ls, l) => loadingScreen?.SetProgress(ls.IndexOf(l) / ls.Count - 1),
                    perAssetOnLoad: (h, l) => loadingScreen?.SetAsset(l.PrimaryKey),
                    onComplete: (a) => loadingScreen?.Hide()
                );
                var spritesLoad = repo.LoadAssetsEnumerator<Sprite>(
                    perAssetOnLoad: (h, l) => loadingScreen?.SetAsset(l.PrimaryKey),
                    onLoad: (ls, l) => loadingScreen?.SetProgress(ls.IndexOf(l) / ls.Count - 1),
                    onComplete: (a) => repo.StartCoroutine(audioClipsLoad)
                );
                var textAssetsLoad = repo.LoadAssetsEnumerator<TextAsset>(
                    perAssetOnLoad: (h, l) => loadingScreen?.SetAsset(l.PrimaryKey),
                    onStart: () => loadingScreen?.Show(),
                    onLoad: (ls, l) => loadingScreen?.SetProgress(ls.IndexOf(l) / ls.Count - 1),
                    onComplete: (a) => repo.StartCoroutine(spritesLoad)
                );

                repo.StartCoroutine(textAssetsLoad);

            }

        }

    }

}