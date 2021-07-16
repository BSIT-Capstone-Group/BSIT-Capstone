/*
 * Date Created: Wednesday, July 7, 2021 11:55 AM
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

namespace CoDe_A.Lakbay.ModulesOld3.Game.Travel {
    using Event = Utilities.Event;

    public interface IEvent : Core.IEvent {
        Event.OnBoolChange<IEvent> onActiveChange { get; }
        Event.OnFloatChange<IEvent> onSpeedChange { get; }
        Event.OnFloatChange<IEvent> onDurationChange { get; }
        Event.OnValueChange<IEvent, Easing.Ease> onEasingChange { get; }

        void OnActiveChange(bool old, bool @new);
        void OnSpeedChange(float old, float @new);
        void OnDurationChange(float old, float @new);
        void OnEasingChange(Easing.Ease old, Easing.Ease @new);

    }

    public interface IProperty : Core.IProperty {
        bool active { get; set; }
        float speed { get; set; }
        float duration { get; set; }
        Easing.Ease easing { get; set; }

    }

    public interface IPropertyEvent : IProperty, IEvent {}

    public interface IData : Core.IData, IProperty {

    }

    [Serializable]
    public class Data : Core.Data, IData {
        [SerializeField] protected bool _active;
        public virtual bool active { get => _active; set => _active = value; }
        [SerializeField] protected float _speed;
        public virtual float speed { get => _speed; set => _speed = value; }
        [SerializeField] protected float _duration;
        public virtual float duration { get => _duration; set => _duration = value; }
        [SerializeField] protected _EasingFunction.Ease _easing;
        public virtual _EasingFunction.Ease easing { get => _easing; set => _easing = value; }

    }

}