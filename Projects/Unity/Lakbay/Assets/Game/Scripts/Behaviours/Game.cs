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
using UnityEngine.SceneManagement;

using TMPro;

using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Behaviours {
    public class Game : Controller {
        protected static bool _initialized = false;
        public static bool initialized => _initialized;
        protected static int _initialSceneBuildIndex = -1;

        [SerializeField]
        protected EventAndInput _eventAndInput;
        public static EventAndInput eventAndInput;
        [SerializeField]
        protected Repository _repository;
        public static Repository repository;
        [SerializeField]
        protected LoadingScreen _loadingScreen;
        public static LoadingScreen loadingScreen;
        [SerializeField]
        protected SceneChanger _sceneChanger;

        public override void Awake() {
            base.Awake();

            if(Application.isEditor && !FindObjectOfType<SceneChanger>()) {
                if(_sceneChanger) Instantiate(_sceneChanger);

            }

            if(!initialized) {
                var scene = SceneManager.GetActiveScene();
                int sceneBuildIndex = scene.buildIndex;
                if(sceneBuildIndex != 0) {
                    _initialSceneBuildIndex = sceneBuildIndex;
                    foreach(var go in scene.GetRootGameObjects()) {
                        if(go.gameObject != gameObject) {
                            go.SetActive(false);
                            
                        }

                    }
                    LoadScene(0);

                } else {
                    DontDestroyOnLoad(gameObject);
                    Initialize();

                }
            
            }

        }

        public virtual void Initialize() {
            if(initialized) return;

            if(!FindObjectOfType<EventAndInput>() && _eventAndInput) {
                var go = Instantiate(_eventAndInput);
                eventAndInput = go.GetComponent<EventAndInput>();
                
            }

            if(!FindObjectOfType<Repository>() && _repository) {
                var go = Instantiate(_repository);
                repository = go.GetComponent<Repository>();
                
            }

            if(!FindObjectOfType<LoadingScreen>() && _loadingScreen) {
                var go = Instantiate(_loadingScreen);
                loadingScreen = go.GetComponent<LoadingScreen>();
                
            }

            LoadAssets();

            _initialized = true;

        }

        public static void LoadAssets() {
            var repo = repository;
            if(repo) {
                repo.UnloadAssets();

                var audioClipsLoad = repo.LoadAssetsEnumerator<AudioClip>(
                    onProgress: (ls, l) => loadingScreen?.SetProgress(ls.IndexOf(l) / ls.Count - 1),
                    perAssetOnProgress: (h, l) => loadingScreen?.SetInformation(l.PrimaryKey),
                    onComplete: (a) => {
                        int index = _initialSceneBuildIndex;
                        if(index == -1) index = 1;
                        LoadScene(
                            index,
                            onProgress: (o) => {
                                loadingScreen?.SetInformation("Loading Scene...");
                                loadingScreen?.SetProgress(o.progress);

                            },
                            onComplete: (o) => {
                                loadingScreen?.SetInformation("Loading Scene...");
                                loadingScreen?.SetProgress(o.progress);
                                loadingScreen?.Hide();
                                
                            }
                        );
                    }
                );
                var spritesLoad = repo.LoadAssetsEnumerator<Sprite>(
                    perAssetOnProgress: (h, l) => loadingScreen?.SetInformation(l.PrimaryKey),
                    onProgress: (ls, l) => loadingScreen?.SetProgress(ls.IndexOf(l) / ls.Count - 1),
                    onComplete: (a) => repo.StartCoroutine(audioClipsLoad)
                );
                var textAssetsLoad = repo.LoadAssetsEnumerator<TextAsset>(
                    perAssetOnProgress: (h, l) => loadingScreen?.SetInformation(l.PrimaryKey),
                    onStart: () => loadingScreen?.Show(),
                    onProgress: (ls, l) => loadingScreen?.SetProgress(ls.IndexOf(l) / ls.Count - 1),
                    onComplete: (a) => repo.StartCoroutine(spritesLoad)
                );

                repo.StartCoroutine(textAssetsLoad);

            }

        }

        public static IEnumerator LoadSceneEnumerator(
            int buildIndex,
            LoadSceneMode mode=LoadSceneMode.Single,
            bool activated=true,
            Action<AsyncOperation> onStart=null,
            Action<AsyncOperation> onProgress=null,
            Action<AsyncOperation> onComplete=null
        ) {
            // var uoperation = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            // while(!uoperation.isDone) {
            //     yield return new WaitForEndOfFrame();

            // }

            var operation = SceneManager.LoadSceneAsync(buildIndex, mode);
            operation.allowSceneActivation = activated;
            
            onStart?.Invoke(operation);
            while(!operation.isDone) {
                onProgress?.Invoke(operation);
                yield return new WaitForEndOfFrame();

            }

            onComplete?.Invoke(operation);

        }

        public static void LoadScene(
            int buildIndex,
            LoadSceneMode mode=LoadSceneMode.Single,
            bool activated=true,
            Action<AsyncOperation> onStart=null,
            Action<AsyncOperation> onProgress=null,
            Action<AsyncOperation> onComplete=null
        ) {
            var instance = GetInstance<Game>();
            if(!instance) return;
            instance.StartCoroutine(
                LoadSceneEnumerator(buildIndex, mode, activated, onStart, onProgress, onComplete)
            );

        }

    }

}