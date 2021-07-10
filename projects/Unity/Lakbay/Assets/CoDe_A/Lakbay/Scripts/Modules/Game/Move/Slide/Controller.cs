/*
 * Date Created: Wednesday, July 7, 2021 12:15 PM
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

namespace CoDe_A.Lakbay.Modules.Game.Move.Slide {
    using Event = Utilities.Event;

    public interface IController : Core.IController, IPropertyEvent {

    }

    public class Controller : Core.Controller, IController {
        [SerializeField] protected bool _active;
        public virtual bool active {
            get => _active;
            set => Helper.SetInvoke(this, ref _active, value, onActiveChange, OnActiveChange);
        
        }
        [SerializeField] protected int _index;
        public virtual int index {
            get => _index;
            set => Helper.SetInvoke(this, ref _index, value, onIndexChange, OnIndexChange);
        
        }
        [SerializeField] protected float _speed;
        public virtual float speed {
            get => _speed;
            set => Helper.SetInvoke(this, ref _speed, value, onSpeedChange, OnSpeedChange);
        
        }
        [SerializeField] protected float _duration;
        public virtual float duration {
            get => _duration;
            set => Helper.SetInvoke(this, ref _duration, value, onDurationChange, OnDurationChange);
        
        }
        [SerializeField] protected _EasingFunction.Ease _easing;
        public virtual _EasingFunction.Ease easing {
            get => _easing;
            set => Helper.SetInvoke(this, ref _easing, value, onEasingChange, OnEasingChange);
        
        }

        public Event.OnBoolChange<IEvent> onActiveChange => throw new NotImplementedException();

        public Event.OnIntChange<IEvent> onIndexChange => throw new NotImplementedException();

        public Event.OnFloatChange<IEvent> onSpeedChange => throw new NotImplementedException();

        public Event.OnFloatChange<IEvent> onDurationChange => throw new NotImplementedException();

        public Event.OnValueChange<IEvent, _EasingFunction.Ease> onEasingChange => throw new NotImplementedException();

        public virtual void OnActiveChange(bool old, bool @new) {
            

        }

        public virtual void OnDurationChange(float old, float @new) {
            

        }

        public virtual void OnEasingChange(_EasingFunction.Ease old, _EasingFunction.Ease @new) {
            

        }

        public virtual void OnIndexChange(int old, int @new) {
            

        }

        public virtual void OnSpeedChange(float old, float @new) {
            

        }
        
    }

}