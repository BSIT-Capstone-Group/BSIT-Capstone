/*
 * Date Created: Friday, July 9, 2021 5:54 AM
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

namespace CoDe_A.Lakbay.ModulesOld3.Core.Transition {
    using Event = Utilities.Event;

    public interface IEvent<T> : Input.IEvent {
        Event.OnValueChange<IEvent, T> onFromChange { get; }
        Event.OnValueChange<IEvent, T> onToChange { get; }

        Event.OnFloatChange<IEvent> onProgressChange { get; }
        Event.OnFloatChange<IEvent> onSpeedChange { get; }
        Event.OnFloatChange<IEvent> onDurationChange { get; }
        Event.OnValueChange<IEvent, Easing.Ease> onEasingChange { get; }

        void OnFromChange(T old, T @new);
        void OnToChange(T old, T @new);

        void OnProgressChange(float old, float @new);
        void OnSpeedChange(float old, float @new);
        void OnDurationChange(float old, float @new);
        void OnEasingChange(Easing.Ease old, Easing.Ease @new);

    }

    public interface IProperty<T> : Input.IProperty {
        T from { get; set; }
        T to { get; set; }

        float progress { get; set; }
        float speed { get; set; }
        float duration { get; set; }
        Easing.Ease easing { get; set; }

    }

    public interface IPropertyEvent<T> : IProperty, IEvent {}

    public interface IData<T> : Input.IData, IProperty {

    }

    [Serializable]
    public class Data<T> : Input.Data, IData<T> {
        [SerializeField] protected T _from;
        [YamlIgnore] public virtual T from { get => _from; set => _from = value; }
        [SerializeField] protected T _to;
        [YamlIgnore] public virtual T to { get => _to; set => _to = value; }

        [SerializeField] protected float _progress;
        public virtual float progress { get => _progress; set => _progress = value; }
        [SerializeField] protected float _speed;
        public virtual float speed { get => _speed; set => _speed = value; }
        [SerializeField] protected float _duration;
        public virtual float duration { get => _duration; set => _duration = value; }
        [SerializeField] protected _EasingFunction.Ease _easing;
        public virtual _EasingFunction.Ease easing { get => _easing; set => _easing = value; }


        public Data() { Create(instance: this); }

        public static Data<T> Create(
            float progress=0.0f,
            float speed=1.0f,
            float duration=2.0f,
            _EasingFunction.Ease easing=default,
            Input.IProperty data=null,
            IProperty<T> instance=null
        ) {
            // instance ??= new Data<T>();
            if(object.Equals(instance, null)) instance = new Data<T>() as IProperty<T>;
            Create(data ?? new Input.Data(), instance);
            instance.progress = progress;
            instance.speed = speed;
            instance.duration = duration;
            instance.easing = easing;

            return instance as Data<T>;

        }

        public static Data<T> Create(IProperty<T> data, IProperty<T> instance=null) {
            return Create(
                data.progress,
                data.speed,
                data.duration,
                data.easing,
                data,
                instance
            );
        }

        public static Data<T> Create(TextAsset textAsset, IProperty<T> instance=null) {
            return Create(textAsset.Parse<Data<T>>() as IProperty<T>, instance);

        }

    }
    
}