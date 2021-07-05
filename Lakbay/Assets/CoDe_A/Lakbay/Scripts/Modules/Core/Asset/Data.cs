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

namespace CoDe_A.Lakbay.Modules.Core.Asset {
    using Event = Utilities.Event;

    public interface IData : Core.IData {
            
    }

    public interface IData<T> : Core.IData<T>
        where T : IController {
        string path { get; set; }

        Event.OnStringChange<T> onPathChange { get; }

    }

    [Serializable]
    public class Data<T> : Core.Data<T>, IData<T>
        where T : class, IController {
        public new const string HeaderName = "Asset.Data";

        [Header(HeaderName)]
        [SerializeField]
        protected string m_path;
        protected string _path;
        public virtual string path {
            get => _path;
            set {
                if(value == path) return;
                var o = path; var n = value;
                _path = m_path = value;
                onPathChange.Invoke(controller, o, n);
                controller?.OnPathChange(o, n);

            }

        }

        [SerializeField]
        protected Event.OnStringChange<T> _onPathChange = new Event.OnStringChange<T>();
        public virtual Event.OnStringChange<T> onPathChange => _onPathChange;


        public Data() : this("") {}

        public Data(
            string path="",
            Core.IData<T> data=null
        ) : base(data ?? new Core.Data<T>()) {
            this.path = path;

        }

        public Data(IData<T> data) : this(
            data.path,
            data
        ) {}

        public Data(TextAsset textAsset) : this(textAsset.Parse<Data<T>>()) {}

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            path = m_path;

        }

    }

}