/*
 * Date Created: Saturday, July 3, 2021 4:56 PM
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
using UnityEngine.UI;

using NaughtyAttributes;
using TMPro;
using YamlDotNet.Serialization;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.ModulesOld1.Core.Asset {
    [Serializable]
    public class OnAssetChange<T0, T1> : Core.OnValueChange<IController<T0, T1>, T1>
        where T0 : class, IData<T1> 
        where T1 : UnityEngine.Object {}
    [Serializable]
    public class OnPathChange<T0, T1> : Core.OnStringValueChange<IController<T0, T1>>
        where T0 : class, IData<T1> 
        where T1 : UnityEngine.Object {}

    public interface IController<T0, T1> : Core.IController<T0>, IData<T1>
        where T0 : class, IData<T1>
        where T1 : UnityEngine.Object {
        OnAssetChange<T0, T1> onAssetChange { get; }
        OnPathChange<T0, T1> onPathChange { get; }

        void OnAssetChange(T1 old, T1 @new);
        void OnPathChange(string old, string @new);

    }

    public class Controller<T0, T1> : Core.Controller<T0>, IController<T0, T1>
        where T0 : class, IData<T1>
        where T1 : UnityEngine.Object {
        public new const string BoxGroupName = "Asset.Controller";

        public override T0 data {
            get => base.data;
            set {
                base.data = value;
                if(value != null) {
                    path = value.path;

                }

            }
            
        }
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected T1 m_asset;
        protected T1 _asset;
        public virtual T1 asset {
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
        [SerializeField, ReadOnly]
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
        
        [Foldout(EventFoldoutName)]
        [SerializeField]
        protected OnAssetChange<T0, T1> _onAssetChange = new OnAssetChange<T0, T1>();
        public OnAssetChange<T0, T1> onAssetChange => _onAssetChange;
        [Foldout(EventFoldoutName)]
        [SerializeField]
        protected OnPathChange<T0, T1> _onPathChange = new OnPathChange<T0, T1>();
        public OnPathChange<T0, T1> onPathChange => _onPathChange;


        public Controller() : base() {
            data = default;

        }

        public virtual void OnAssetChange(T1 old, T1 @new) {


        }

        public virtual void OnPathChange(string old, string @new) {
            asset = data?.asset;

        }

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            asset = m_asset;
            path = m_path;

        }

    }

}