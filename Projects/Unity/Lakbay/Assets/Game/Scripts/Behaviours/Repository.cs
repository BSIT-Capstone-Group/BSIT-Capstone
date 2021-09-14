/*
 * Date Created: Thursday, September 9, 2021 8:28 AM
 * Author: NI.L.A
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Specialized;
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
    public enum AssetType {
        TextAsset, Sprite, AudioClip

    }

    public class Repository : Controller {
        public delegate void OnStart();
        public delegate void OnLoad(IList<IResourceLocation> locations, IResourceLocation location);
        public delegate void OnComplete<T>(IList<T> assets);
        public delegate void PerAssetCallback<T>(AsyncOperationHandle<T> handle, IResourceLocation location);

        protected bool _loaded = false;
        protected readonly OrderedDictionary _assets = new OrderedDictionary();

        public IEnumerator LoadAssetsEnumerator<T>(
            string path,
            OnStart onStart=null,
            OnLoad onLoad=null,
            OnComplete<T> onComplete=null,
            PerAssetCallback<T> perAssetOnStart=null,
            PerAssetCallback<T> perAssetOnLoad=null,
            PerAssetCallback<T> perAssetOnComplete=null
        ) {
            if(!HasAsset(path)) {
                onStart?.Invoke();
                IList<IResourceLocation> locations = new List<IResourceLocation>();

                var resourceHandle = Addressables.LoadResourceLocationsAsync(path);
                resourceHandle.Completed += (h) => locations = h.Result;
                while(!resourceHandle.IsDone) {
                    yield return new WaitForEndOfFrame();

                }

                Addressables.Release(resourceHandle);

                IList<T> assets = new List<T>();

                foreach(var location in locations) {
                    onLoad?.Invoke(locations, location);
                    if(HasAsset(location.PrimaryKey)) continue;

                    var handle = Addressables.LoadAssetAsync<T>(location);
                    perAssetOnStart?.Invoke(handle, location);
                    while(!handle.IsDone) {
                        perAssetOnLoad?.Invoke(handle, location);
                        yield return new WaitForEndOfFrame();
                        
                    }

                    perAssetOnComplete?.Invoke(handle, location);
                    var asset = handle.Result;
                    _assets.Add(location.PrimaryKey, asset);
                    assets.Add(asset);

                    Addressables.Release(handle);

                }

                onComplete?.Invoke(assets);

            }

        }

        public IEnumerator LoadAssetsEnumerator<T>(
            OnStart onStart=null,
            OnLoad onProgress=null,
            OnComplete<T> onComplete=null,
            PerAssetCallback<T> perAssetOnStart=null,
            PerAssetCallback<T> perAssetOnProgress=null,
            PerAssetCallback<T> perAssetOnComplete=null
        ) {
            return LoadAssetsEnumerator(
                typeof(T).Name,
                onStart, onProgress, onComplete,
                perAssetOnStart, perAssetOnProgress, perAssetOnComplete
            );

        }

        public void LoadAssets<T>(
            string path,
            OnStart onStart=null,
            OnLoad onLoad=null,
            OnComplete<T> onComplete=null,
            PerAssetCallback<T> perAssetOnStart=null,
            PerAssetCallback<T> perAssetOnLoad=null,
            PerAssetCallback<T> perAssetOnComplete=null
        ) {
            StartCoroutine(LoadAssetsEnumerator(
                path,
                onStart, onLoad, onComplete,
                perAssetOnStart, perAssetOnLoad, perAssetOnComplete
            ));

        }

        public void LoadAssets<T>(
            OnStart onStart=null,
            OnLoad onLoad=null,
            OnComplete<T> onComplete=null,
            PerAssetCallback<T> perAssetOnStart=null,
            PerAssetCallback<T> perAssetOnLoad=null,
            PerAssetCallback<T> perAssetOnComplete=null
        ) {
            LoadAssets(typeof(T).Name, 
                onStart, onLoad, onComplete,
                perAssetOnStart, perAssetOnLoad, perAssetOnComplete
            );

        }

        public T GetAsset<T>(string path) {
            if(!HasAsset(path)) return default;
            return (T) _assets[path];

        }

        public bool HasAsset(string path) => _assets.Contains(path);

        public void UnloadAsset(string path) {
            if(HasAsset(path)) {
                var asset = GetAsset<object>(path);
                Addressables.Release(asset);
                _assets.Remove(path);

            }

        }

        public void UnloadAssets() {
            var keys = _assets.Keys.Cast<string>();
            foreach(var key in keys) {
                UnloadAsset(key);

            }

        }

        public override void Awake() {
            base.Awake();
            if(!Game.initialized) DontDestroyOnLoad(gameObject);

        }

        public override void Update() {
            base.Update();

        }

    }

}