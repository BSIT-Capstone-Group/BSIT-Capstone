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

namespace CoDe_A.Lakbay.Modules.Core.Transition {
    using Event = Utilities.Event;

    public interface IController<T> : Core.IController, IPropertyEvent<T> {

    }

    [ExecuteAlways]
    public class Controller<T> : Core.Controller, IController<T> {
        public new const string BoxGroupName = "Transition.Controller";
        
        [BoxGroup(BoxGroupName)]
        [SerializeField] protected T _from;
        [YamlIgnore] public virtual T from {
            get => _from;
            set => Helper.SetInvoke(this, ref _from, value, onFromChange, OnFromChange);
        
        }
        [BoxGroup(BoxGroupName)]
        [SerializeField] protected T _to;
        [YamlIgnore] public virtual T to {
            get => _to;
            set => Helper.SetInvoke(this, ref _to, value, onToChange, OnToChange);
        
        }

        // [SerializeField] protected int _index;
        // public virtual int index {
        //     get => _index;
        //     set => Helper.SetInvoke(this, ref _index, value, onIndexChange, OnIndexChange);
        
        // }
        [BoxGroup(BoxGroupName)]
        [SerializeField, Range(0.0f, 1.0f)] protected float _progress;
        public virtual float progress {
            get => _progress;
            set => Helper.SetInvoke(this, ref _progress, value, onProgressChange, OnProgressChange);
        
        }
        [BoxGroup(BoxGroupName)]
        [SerializeField] protected float _speed;
        public virtual float speed {
            get => _speed;
            set => Helper.SetInvoke(this, ref _speed, value, onSpeedChange, OnSpeedChange);
        
        }
        [BoxGroup(BoxGroupName)]
        [SerializeField] protected float _duration;
        public virtual float duration {
            get => _duration;
            set => Helper.SetInvoke(this, ref _duration, value, onDurationChange, OnDurationChange);
        
        }
        [BoxGroup(BoxGroupName)]
        [SerializeField] protected _EasingFunction.Ease _easing;
        public virtual _EasingFunction.Ease easing {
            get => _easing;
            set => Helper.SetInvoke(this, ref _easing, value, onEasingChange, OnEasingChange);
        
        }

        [BoxGroup(BoxGroupName)]
        [SerializeField] protected Event.OnValueChange<IEvent, T> _onFromChange = new Event.OnValueChange<IEvent, T>();
        public virtual Event.OnValueChange<IEvent, T> onFromChange => _onFromChange;
        [BoxGroup(BoxGroupName)]
        [SerializeField] protected Event.OnValueChange<IEvent, T> _onToChange = new Event.OnValueChange<IEvent, T>();
        public virtual Event.OnValueChange<IEvent, T> onToChange => _onToChange;

        // public Event.OnIntChange<IEvent> onIndexChange => throw new NotImplementedException();

        [BoxGroup(BoxGroupName)]
        [SerializeField] protected Event.OnFloatChange<IEvent> _onProgressChange = new Event.OnFloatChange<IEvent>();
        public Event.OnFloatChange<IEvent> onProgressChange => _onProgressChange;
        [BoxGroup(BoxGroupName)]
        [SerializeField] protected Event.OnFloatChange<IEvent> _onSpeedChange = new Event.OnFloatChange<IEvent>();
        public Event.OnFloatChange<IEvent> onSpeedChange => _onSpeedChange;

        [BoxGroup(BoxGroupName)]
        [SerializeField] protected Event.OnFloatChange<IEvent> _onDurationChange = new Event.OnFloatChange<IEvent>();
        public Event.OnFloatChange<IEvent> onDurationChange => _onDurationChange;

        [BoxGroup(BoxGroupName)]
        [SerializeField] protected Event.OnValueChange<IEvent, _EasingFunction.Ease> _onEasingChange = new Event.OnValueChange<IEvent, _EasingFunction.Ease>();
        public Event.OnValueChange<IEvent, _EasingFunction.Ease> onEasingChange => _onEasingChange;


        public override void Update() {
            base.Update();

        }

        public virtual void OnFromChange(T old, T @new) {

            
        }

        public virtual void OnToChange(T old, T @new) {


        }

        public virtual void OnDurationChange(float old, float @new) {

        }

        public virtual void OnEasingChange(_EasingFunction.Ease old, _EasingFunction.Ease @new) {
            

        }

        // public virtual void OnIndexChange(int old, int @new) {
            

        // }

        public virtual void OnProgressChange(float old, float @new) {
            

        }

        public virtual void OnSpeedChange(float old, float @new) {
            

        }

    }

}