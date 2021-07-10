/*
 * Date Created: Monday, July 5, 2021 4:43 PM
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

namespace CoDe_A.Lakbay.ModulesOld2.Core.Asset.Text {
    using Event = Utilities.Event;
    using BaseIData = IData<Controller, TextAsset, TextMeshProUGUI>;
    using BaseData = Data<Controller, TextAsset, TextMeshProUGUI>;
    using Text_ = TextMeshProUGUI;

    public interface IData : IData<TextAsset, Text_> {
        string value { get; set; }

    }

    public interface IData<T> : IData<T, TextAsset, Text_>, IData
        where T : IController {
        Event.OnStringChange<T> onValueChange { get; }

    }

    [Serializable]
    public class Data : BaseData, IData<Controller> {
        public new const string HeaderName = "Text.Data";

        protected string m_value;
        [Header(HeaderName)]
        [SerializeField, TextArea]
        protected string _value;
        public virtual string value {
            get => _value;
            set {
                if(value == this.value) return;
                var o = this.value; var n = value;
                // _value = m_value = value;
                _value = value;
                onValueChange.Invoke(controller, o, n);
                controller?.OnValueChange(o, n);

            }

        }
        [YamlIgnore]
        public override TextAsset asset {
            get {
                try {
                    if(path != null && path.Length != 0) {
                        return new TextAsset(path);

                    } else if(value != null && value.Length != 0) {
                        return new TextAsset(value);

                    } else {
                        return _asset;

                    }

                } catch {}

                return default;

            }
            set {
                if(value == _asset) return;
                var o = _asset; var n = value;
                // _asset = m_asset = value;
                _asset = value;
                onAssetChange.Invoke(controller, o, n);
                controller?.OnAssetChange(o, n);

            }

        }

        [SerializeField]
        protected Event.OnStringChange<Controller> _onValueChange = new Event.OnStringChange<Controller>();
        public virtual Event.OnStringChange<Controller> onValueChange => _onValueChange;

        public Data() { Create(instance: this); }

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            // value = m_value;

            if(m_value != value) {
                var o = m_value; var n = value;
                onValueChange.Invoke(controller, o, n);
                controller?.OnValueChange(o, n);
                m_value = value;

            }

        }

        public override void Load(TextAsset textAsset) => Create(textAsset, this);

        public static Data Create(
            string value="",
            BaseIData data=null,
            Data instance=null
        ) {
            instance ??= new Data();
            BaseData.Create(data, instance);
            instance.value = value;

            return instance;

        }

        public static Data Create(IData<Controller> data, Data instance=null) {
            data ??= new Data();
            return Create(
                data.value,
                data,
                instance
            );

        }

        public static Data Create(TextAsset textAsset, Data instance=null) {
            return Create(textAsset.Parse<Data>(), instance);

        }

    }

}