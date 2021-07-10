/*
 * Date Created: Monday, July 5, 2021 4:57 AM
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

namespace CoDe_A.Lakbay.ModulesOld2.Core.Asset {
    using Event = Utilities.Event;

    public interface IData<T0, T1> : Core.IData {
        string path { get; set; }
            
    }

    public interface IData<T0, T1, T2> : Core.IData<T0>, IData<T1, T2>
        where T0 : IController<T1, T2> {
        T2 component { get; set; }
        T1 asset { get; set; }

        Event.OnValueChange<T0, T2> onComponentChange { get; }
        Event.OnValueChange<T0, T1> onAssetChange { get; }
        Event.OnStringChange<T0> onPathChange { get; }

    }

    [Serializable]
    public class Data<T0, T1, T2> : Core.Data<T0>, IData<T0, T1, T2>
        where T0 : class, IController<T1, T2>
        where T1 : UnityEngine.Object
        where T2 : MonoBehaviour {
        public new const string HeaderName = "Asset.Data";

        protected T2 m_component;
        [Header(HeaderName)]
        [SerializeField]
        protected T2 _component;
        [YamlIgnore]
        public virtual T2 component {
            get => _component;
            set {
                if(value == component) return;
                var o = component; var n = value;
                // _component = m_component = value;
                _component = value;
                onComponentChange.Invoke(controller, o, n);
                controller?.OnComponentChange(o, n);

            }

        }
        protected T1 m_asset;
        [SerializeField]
        protected T1 _asset;
        [YamlIgnore]
        public virtual T1 asset {
            get => _asset;
            set {
                if(value == asset) return;
                var o = asset; var n = value;
                // _asset = m_asset = value;
                _asset = value;
                onAssetChange.Invoke(controller, o, n);
                controller?.OnAssetChange(o, n);

            }

        }
        protected string m_path;
        [SerializeField]
        protected string _path;
        public virtual string path {
            get => _path;
            set {
                if(value == path) return;
                var o = path; var n = value;
                // _path = m_path = value;
                _path = value;
                onPathChange.Invoke(controller, o, n);
                controller?.OnPathChange(o, n);

            }

        }

        [SerializeField]
        protected Event.OnValueChange<T0, T2> _onComponentChange = new Event.OnValueChange<T0, T2>();
        public virtual Event.OnValueChange<T0, T2> onComponentChange => _onComponentChange;
        [SerializeField]
        protected Event.OnValueChange<T0, T1> _onAssetChange = new Event.OnValueChange<T0, T1>();
        public virtual Event.OnValueChange<T0, T1> onAssetChange => _onAssetChange;
        [SerializeField]
        protected Event.OnStringChange<T0> _onPathChange = new Event.OnStringChange<T0>();
        public virtual Event.OnStringChange<T0> onPathChange => _onPathChange;


        public Data() { Create(instance: this); }

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            // component = m_component;
            // asset = m_asset;
            // path = m_path;

            if(m_component != component) {
                var o = m_component; var n = component;
                onComponentChange.Invoke(controller, o, n);
                controller?.OnComponentChange(o, n);
                m_component = component;

            }
            if(m_asset != asset) {
                var o = m_asset; var n = asset;
                onAssetChange.Invoke(controller, o, n);
                controller?.OnAssetChange(o, n);
                m_asset = asset;

            }
            if(m_path != path) {
                var o = m_path; var n = path;
                onPathChange.Invoke(controller, o, n);
                controller?.OnPathChange(o, n);
                m_path = path;

            }

        }

        public override void Load(TextAsset textAsset) => Create(textAsset, this);

        public static Data<T0, T1, T2> Create(
            string path="",
            Core.IData<T0> data=null,
            Data<T0, T1, T2> instance=null
        ) {
            instance ??= new Data<T0, T1, T2>();
            Core.Data<T0>.Create(data, instance);
            instance.path = path;

            return instance;

        }

        public static Data<T0, T1, T2> Create(IData<T0, T1, T2> data, Data<T0, T1, T2> instance=null) {
            data ??= new Data<T0, T1, T2>();
            return Create(
                data.path,
                data,
                instance
            );

        }

        public static Data<T0, T1, T2> Create(TextAsset textAsset, Data<T0, T1, T2> instance=null) {
            return Create(textAsset.Parse<Data<T0, T1, T2>>(), instance);
            
        }

    }

}