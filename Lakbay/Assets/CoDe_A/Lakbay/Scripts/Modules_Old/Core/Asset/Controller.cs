/*
 * Date Created: Friday, July 2, 2021 5:08 PM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

using NaughtyAttributes;
using YamlDotNet.Serialization;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.ModulesOld.Core.Asset {
    [Serializable]
    public abstract class Event<T> 
            where T : UnityEngine.Object {
        [Serializable]
        public class OnValueChange<T0, T1> : UnityEvent<Controller<T>, T0, T1> 
            where T0 : class 
            where T1 : class {}
        [Serializable]
        public class OnPathChange : OnValueChange<string, string> {}
        [Serializable]
        public class OnAssetChange : OnValueChange<T, T> {}


    }
    // public class OnPathChange<T> : UnityEvent<Controller<T>, string, string> where T : UnityEngine.Object {}
    // public class OnAssetChange<T> : UnityEvent<Controller<T>, T, T> where T : UnityEngine.Object {}

    public interface IController<T> : Core.Base.IController, ISerializable<T> where T : UnityEngine.Object {
        new Data<T> data { get; set; }
        T asset { get; }

        Event<T>.OnPathChange onPathChange { get; }
        Event<T>.OnAssetChange onAssetChange { get; }

        void OnPathChange(string oldValue, string newValue);
        void OnAssetChange(T oldValue, T newValue);

    }

    public class Controller<T> : Core.Base.Controller, IController<T> where T : UnityEngine.Object {
        public new const string BoxGroupName = "Asset.Controller";

        Data<T> IController<T>.data {
            get => new Data<T>(this);
            set {
                value ??= new Data<T>();
                (this as Core.Base.IController).data = value;
                path = value.path;
                asset = value.asAsset;

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected string m_path;
        protected string _path;
        public virtual string path {
            get => _path;
            set {
                if(value == path) return;
                var o = path; var n = value;
                _path = m_path = value;
                onPathChange.Invoke(this, o, n);
                OnPathChange(o, n);

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected T m_asset;
        protected T _asset;
        public T asset {
            get => _asset;
            set {
                if(value == asset) return;
                var o = asset; var n = value;
                _asset = m_asset = value;
                onAssetChange.Invoke(this, o, n);
                OnAssetChange(o, n);

            }

        }

        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected Event<T>.OnPathChange _onPathChange = new Event<T>.OnPathChange();
        public virtual Event<T>.OnPathChange onPathChange => _onPathChange;
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected Event<T>.OnAssetChange _onAssetChange = new Event<T>.OnAssetChange();
        public virtual Event<T>.OnAssetChange onAssetChange => _onAssetChange;


        public Controller() : base() {
            var self = this as IController<T>;
            self.data = null;

        }

        public override void OnInspectorHasUpdate() {
            var self = this as IController<T>;
            base.OnInspectorHasUpdate();
            path = m_path;
            asset = m_asset;

        }

        public override void SetData(TextAsset textAsset) {
            var self = this as IController<T>;
            self.data = new Data<T>(textAsset);
            
        }

        public virtual void OnPathChange(string oldValue, string newValue) {

        }

        public virtual void OnAssetChange(T oldValue, T newValue) {

        }

    }

}