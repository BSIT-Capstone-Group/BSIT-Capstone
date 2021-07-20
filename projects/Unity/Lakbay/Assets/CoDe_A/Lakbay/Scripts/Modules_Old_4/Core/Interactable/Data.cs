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

namespace CoDe_A.Lakbay.ModulesOld4.Core.Interactable {
    using Event = Utilities.Event;
    using Input = Utilities.Input;


    public interface IData : Minimal.IData {
        bool playing { get; set; }
        bool handlingInputs { get; set; }
        int maxCollisionCount { get; set; }
        int collisionCount { get; set; }
    
    }

    public class Data : Minimal.Data, IData {
        public new const string HeaderName = "Interactable.Data";

        [Space(HeaderSpace), Header(HeaderName)]
        [SerializeField]
        protected int _maxCollisionCount;
        public virtual int maxCollisionCount {
            get => _maxCollisionCount;
            set => _maxCollisionCount = value;
            
        }
        [SerializeField]
        protected int _collisionCount;
        public virtual int collisionCount {
            get => _collisionCount;
            set => _collisionCount = value;
            
        }
        [SerializeField]
        protected bool _playing;
        public virtual bool playing {
            get => _playing;
            set => _playing = value;
            
        }
        [SerializeField]
        protected bool _handlingInputs;
        public virtual bool handlingInputs {
            get => _handlingInputs;
            set => _handlingInputs = value;

        }

    }

    public interface IData<T> : Minimal.IData<T>, IData
        where T : IController {

        Event.OnBoolChange<IController> onPlayingChange { get; }
        Event.OnBoolChange<IController> onHandlingInputsChange { get; }
        
    }

    [Serializable]
    public class Data<T> : Data, IData<T>
        where T : class, IController {
        [SerializeField, ReadOnly]
        protected T _controller;
        [YamlIgnore]
        public virtual T controller { get => _controller; set => _controller = value; }

        public override int maxCollisionCount {
            get => base.maxCollisionCount;
            set {
                var r = Helper.SetInvoke(controller, ref _maxCollisionCount, value);
                // if(r.Item1) controller?.OnPlayingChange(r.Item2[0], r.Item2[1]);
                
            }
            
        }
        public override int collisionCount {
            get => base.collisionCount;
            set {
                if(maxCollisionCount >= 0) Mathf.Clamp(value, 0, maxCollisionCount);
                var r = Helper.SetInvoke(controller, ref _collisionCount, value);
                // if(r.Item1) controller?.OnPlayingChange(r.Item2[0], r.Item2[1]);
                
            }
            
        }
        public override bool playing {
            get => base.playing;
            set {
                var r = Helper.SetInvoke(controller, ref _playing, value, onPlayingChange);
                if(r.Item1) controller?.OnPlayingChange(r.Item2[0], r.Item2[1]);
                
            }
            
        }
        public override bool handlingInputs {
            get => base.handlingInputs;
            set {
                var r = Helper.SetInvoke(controller, ref _handlingInputs, value, onHandlingInputsChange);
                if(r.Item1) controller?.OnHandlingInputsChange(r.Item2[0], r.Item2[1]);
                
            }

        }

        [SerializeField]
        protected Event.OnBoolChange<IController> _onPlayingChange = new Event.OnBoolChange<IController>();
        [YamlIgnore]
        public virtual Event.OnBoolChange<IController> onPlayingChange => _onPlayingChange;
        [SerializeField]
        protected Event.OnBoolChange<IController> _onHandlingInputsChange = new Event.OnBoolChange<IController>();
        [YamlIgnore]
        public virtual Event.OnBoolChange<IController> onHandlingInputsChange => _onHandlingInputsChange;

        public Event.OnStringChange<T> onLabelChange => throw new NotImplementedException();

        public Event.OnStringChange<T> onDescriptionChange => throw new NotImplementedException();

        public Data() => Create(instance: this);

        public static Data<T> Create(
            bool playing=false,
            bool handlingInputs=true,
            int maxCollisionCount=-1,
            int collisionCount=0,
            Minimal.IData<T> data=null,
            IData<T> instance=null
        ) {
            instance ??= new Data<T>();
            Minimal.Data<T>.Create(data, instance);
            instance.playing = playing;
            instance.handlingInputs = handlingInputs;
            instance.maxCollisionCount = maxCollisionCount;
            instance.collisionCount = collisionCount;

            return instance as Data<T>;

        }

        public static Data<T> Create(
            IData<T> data,
            IData<T> instance=null
        ) {
            data ??= new Data<T>();
            return Create(
                data.playing,
                data.handlingInputs,
                data.maxCollisionCount,
                data.collisionCount,
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