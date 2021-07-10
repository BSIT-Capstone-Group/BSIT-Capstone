/*
 * Date Created: Saturday, July 3, 2021 6:28 PM
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

namespace CoDe_A.Lakbay.ModulesOld1.Core.Asset.Text {
    using Text_ = TMP_Text;
    using OnValueChange = OnValueChange<IController>;
    using OnTextComponentChange = OnTextComponentChange<IController>;

    [Serializable]
    public class OnValueChange<T> : Core.OnStringValueChange<T> {}
    [Serializable]
    public class OnTextComponentChange<T> : Core.OnValueChange<T, Text_> {}

    public interface IController : Asset.IController<Data, TextAsset>, IData {
        Text_ textComponent { get; set; }

        OnTextComponentChange onTextComponentChange { get; }
        void OnTextComponentChange(Text_ old, Text_ @new);
        OnValueChange onValueChange { get; }
        void OnValueChange(string old, string @new);

    }

    public class Controller : Asset.Controller<Data, TextAsset>, IController {
        public new const string BoxGroupName = "Text.Controller";

        public override Data data {
            get => new Data(this);
            set {
                base.data = value;
                if(value != null) {
                    this.value = value.value;

                }

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected Text_ m_textComponent;
        protected Text_ _textComponent;
        public Text_ textComponent {
            get => _textComponent;
            set {
                if(value == textComponent) return;
                var o = textComponent; var n = value;
                _textComponent = m_textComponent = value;
                onTextComponentChange.Invoke(this, o, n);
                OnTextComponentChange(o, n);

            }
            
        }
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected string m_value;
        protected string _value;
        public virtual string value {
            get => _value;
            set {
                if(value == this.value) return;
                var o = this.value; var n = value;
                _value = m_value = value;
                onValueChange.Invoke(this, o, n);
                OnValueChange(o, n);

            }

        }
        
        [Foldout(EventFoldoutName)]
        [SerializeField]
        protected OnTextComponentChange _onTextComponentChange = new OnTextComponentChange();
        public OnTextComponentChange onTextComponentChange => _onTextComponentChange;
        [Foldout(EventFoldoutName)]
        [SerializeField]
        protected OnValueChange _onValueChange = new OnValueChange();
        public OnValueChange onValueChange => _onValueChange;

        public Controller() : base() {
            data = new Data();

        }

        public virtual void OnValueChange(string old, string @new) {
            asset = data?.asset;
            textComponent?.SetText(@new);
            
        }

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            textComponent = m_textComponent;
            value = m_value;

        }

        public override void OnAssetChange(TextAsset old, TextAsset @new) {
            base.OnAssetChange(old, @new);
            value = @new ? @new.text : "";

        }
        
        [ContextMenu("Set Data")]
        public override void SetData() => base.SetData();

        [ContextMenu("Localize")]
        public override void Localize() => base.Localize();

        public virtual void OnTextComponentChange(Text_ old, Text_ @new) {
            @new?.SetText(value);

        }

    }

}