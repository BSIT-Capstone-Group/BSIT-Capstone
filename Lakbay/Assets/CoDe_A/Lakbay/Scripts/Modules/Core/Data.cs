/*
 * Date Created: Sunday, July 4, 2021 11:44 AM
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

namespace CoDe_A.Lakbay.Modules.Core {
    using Event = Utilities.Event;

    public interface IData : IInterface {
        string label { get; set; }
        string description { get; set; }

        void OnInspectorHasUpdate();

    }

    public interface IData<T> : IData
        where T : IController {
        T controller { get; set; }

        Event.OnStringChange<T> onLabelChange { get; }
        Event.OnStringChange<T> onDescriptionChange { get; }

    }

    [Serializable]
    public class Data<T> : IData<T>
        where T : class, IController {
        public const string HeaderName = "Core.Data";

        [Header(HeaderName)]
        [SerializeField, ReadOnly]
        protected T _controller;
        public virtual T controller { get => _controller; set => _controller = value; }
        [SerializeField]
        protected string m_label;
        protected string _label;
        public virtual string label {
            get => _label;
            set {
                if(value == label) return;
                var o = label; var n = value;
                _label = m_label = value;
                onLabelChange.Invoke(controller, o, n);
                controller?.OnLabelChange(o, n);

            }

        }
        [SerializeField]
        protected string m_description;
        protected string _description;
        public virtual string description {
            get => _description;
            set {
                if(value == description) return;
                var o = description; var n = value;
                _description = m_description = value;
                onDescriptionChange.Invoke(controller, o, n);
                controller?.OnDescriptionChange(o, n);

            }

        }

        [SerializeField]
        protected Event.OnStringChange<T> _onLabelChange = new Event.OnStringChange<T>();
        public virtual Event.OnStringChange<T> onLabelChange => _onLabelChange;
        [SerializeField]
        protected Event.OnStringChange<T> _onDescriptionChange = new Event.OnStringChange<T>();
        public virtual Event.OnStringChange<T> onDescriptionChange => _onDescriptionChange;

        public Data() : this("") {}

        public Data(
            string label="",
            string description=""
        ) : base() {
            this.label = label;
            this.description = description;

        }

        public Data(IData<T> data) : this(
            data.label,
            data.description
        ) {}

        public Data(TextAsset textAsset) : this(textAsset.Parse<Data<T>>()) {}

        public virtual void OnInspectorHasUpdate() {
            label = m_label;
            description = m_description;

        }

    }

}