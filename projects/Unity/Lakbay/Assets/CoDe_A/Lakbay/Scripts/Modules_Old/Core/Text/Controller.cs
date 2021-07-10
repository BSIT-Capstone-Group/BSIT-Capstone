using System.Runtime;
/*
 * Date Created: Friday, July 2, 2021 10:19 PM
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
using TMPro;

using CoDe_A.Lakbay.Utilities;

using Text_ = TMPro.TMP_Text;

namespace CoDe_A.Lakbay.ModulesOld.Core.Text {
    [Serializable]
    public abstract class Event : Asset.Event<TextAsset> {
        [Serializable]
        public class OnTextComponentChange : OnValueChange<Text_, Text_> { }
        [Serializable]
        public class OnValueChange : OnValueChange<string, string> { }

    }

    public interface IController : Asset.IController<TextAsset>, ISerializable {
        new Data data { get; set; }
        Text_ textComponent { get; set; }
        Event.OnTextComponentChange onTextComponentChange { get; }
        Event.OnValueChange onValueChange { get; }

        void OnTextComponentChange(Text_ oldValue, Text_ newValue);
        void OnValueChange(string oldValue, string newValue);

    }

    public class Controller : Asset.Controller<TextAsset>, IController {
        public new const string BoxGroupName = "Text.Controller";

        Data IController.data {
            get => new Data(this);
            set {
                value ??= new Data();
                (this as Asset.IController<TextAsset>).data = value;
                this.value = value.value;

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

        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected Event.OnTextComponentChange _onTextComponentChange = new Event.OnTextComponentChange();
        public Event.OnTextComponentChange onTextComponentChange => _onTextComponentChange;
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected Event.OnValueChange _onValueChange = new Event.OnValueChange();
        public Event.OnValueChange onValueChange => _onValueChange;


        public override void OnInspectorHasUpdate() {
            var self = this as IController;
            base.OnInspectorHasUpdate();
            textComponent = m_textComponent;
            value = m_value;

        }

        public override void OnPathChange(string oldValue, string newValue) {
            var self = this as IController;
            base.OnPathChange(oldValue, newValue);
            asset = self.data.asAsset;

        }

        public override void OnAssetChange(TextAsset oldValue, TextAsset newValue) {
            var self = this as IController;
            base.OnAssetChange(oldValue, newValue);
            if(newValue) value = newValue.text;

        }

        public virtual void OnTextComponentChange(Text_ oldValue, Text_ newValue) {
            var self = this as IController;
            if(newValue) newValue.SetText(value);

        }

        public virtual void OnValueChange(string oldValue, string newValue) {
            var self = this as IController;
            if(textComponent) {
                textComponent.SetText(newValue);
                
            }

            asset = self.data.asAsset;

        }

    }

}