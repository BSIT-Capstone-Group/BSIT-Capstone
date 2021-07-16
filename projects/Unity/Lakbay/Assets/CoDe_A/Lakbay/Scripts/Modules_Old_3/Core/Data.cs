/*
 * Date Created: Wednesday, July 7, 2021 5:24 AM
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

namespace CoDe_A.Lakbay.ModulesOld3.Core {
    using Event = Utilities.Event;
    // using BaseIData = Core.IData<Controller>;
    // using BaseData = Core.Data<Controller>;

    public interface IInterface {

    }

    public interface IEvent : IInterface {
        Event.OnValueChange<IEvent, TextAsset> onDataTextAssetChange { get; }
        Event.OnBoolChange<IEvent> onPlayingChange { get; }

        void OnDataTextAssetChange(TextAsset old, TextAsset @new);
        void OnPlayingChange(bool old, bool @new);

    }

    public interface IProperty : IInterface {
        TextAsset dataTextAsset { get; set; }

        string controllerName { get; }
        bool playing { get; set; }

    }

    public interface IPropertyEvent : IProperty, IEvent {}

    public interface IData : IProperty {}

    [Serializable]
    public class Data : IData {
        [SerializeField] protected TextAsset _dataTextAsset;
        [YamlIgnore] public virtual TextAsset dataTextAsset { get => _dataTextAsset; set => _dataTextAsset = value; }
        protected string _controllerName;
        [YamlIgnore] public virtual string controllerName => _controllerName;
        [SerializeField, ReadOnly] protected bool _playing;
        public virtual bool playing { get => _playing; set => _playing = value; }

        public Data() : base() { Create(instance: this); }

        public static Data Create(
            bool playing=false,
            IProperty instance=null
        ) {
            instance ??= new Data();
            instance.playing = playing;
            return instance as Data;

        }

        public static Data Create(
            IProperty data,
            IProperty instance=null
        ) {
            data ??= new Data();
            return Create(data.playing, instance: instance);

        }

        public static Data Create(TextAsset textAsset, IProperty instance=null) {
            return Create(textAsset.Parse<Data>(), instance);

        }

    }

}