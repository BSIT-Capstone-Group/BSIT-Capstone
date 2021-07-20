/*
 * Date Created: Tuesday, July 13, 2021 1:14 PM
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
using UnityEngine.InputSystem;

using NaughtyAttributes;
using TMPro;
using YamlDotNet.Serialization;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.ModulesOld4.Core.Minimal {
    using Event = Utilities.Event;
    using Input = Utilities.Input;


    public interface IData : Core.IData {
        string label { get; set; }
        string description { get; set; }
        
    }

    public class Data : Core.Data, IData {
        public new const string HeaderName = "Minimal.Data";

        [Space(HeaderSpace), Header(HeaderName)]
        [SerializeField]
        protected string _label;
        public virtual string label {
            get => _label;
            set => _label = value;

        }

        [SerializeField]
        protected string _description;
        public virtual string description {
            get => _description;
            set => _description = value;

        }

    }

    public interface IData<T> : Core.IData<T>, IData
        where T : IController {
        Event.OnStringChange<T> onLabelChange { get; }
        Event.OnStringChange<T> onDescriptionChange { get; }
        
    }

    [Serializable]
    public class Data<T> : Data, IData<T>
        where T : class, IController {
        [SerializeField, ReadOnly]
        protected T _controller;
        [YamlIgnore]
        public virtual T controller { get => _controller; set => _controller = value; }

        public override string label {
            get => base.label;
            set {
                var r = Helper.SetInvoke(controller, ref _label, value, onLabelChange);
                if(r.Item1) controller?.OnLabelChange(r.Item2[0], r.Item2[1]);
                
            }

        }
        public override string description {
            get => base.description;
            set {
                var r = Helper.SetInvoke(controller, ref _description, value, onDescriptionChange);
                if(r.Item1) controller?.OnDescriptionChange(r.Item2[0], r.Item2[1]);
                
            }

        }

        // [SerializeField]
        protected Event.OnStringChange<T> _onLabelChange = new Event.OnStringChange<T>();
        [YamlIgnore]
        public virtual Event.OnStringChange<T> onLabelChange => _onLabelChange;
        // [SerializeField]
        protected Event.OnStringChange<T> _onDescriptionChange = new Event.OnStringChange<T>();
        [YamlIgnore]
        public virtual Event.OnStringChange<T> onDescriptionChange => _onDescriptionChange;


        public Data() => Create(instance: this);

        public static Data<T> Create(
            string label="",
            string description="",
            Core.IData<T> data=null,
            IData<T> instance=null
        ) {
            instance ??= new Data<T>();
            Core.Data<T>.Create(data, instance);
            instance.label = label;
            instance.description = description;

            return instance as Data<T>;

        }

        public static Data<T> Create(
            IData<T> data,
            IData<T> instance=null
        ) {
            data ??= new Data<T>();
            return Create(
                data.label,
                data.description,
                data,
                instance
            );

        }

        public static Data<T> Create(TextAsset textAsset, IData<T> instance=null) {
            return Create(textAsset.Parse<Data<T>>(), instance);

        }

        public override void Set(TextAsset textAsset) => Create(textAsset, this);

    }

}