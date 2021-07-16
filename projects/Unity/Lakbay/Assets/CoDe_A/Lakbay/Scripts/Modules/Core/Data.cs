/*
 * Date Created: Tuesday, July 13, 2021 10:27 AM
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

namespace CoDe_A.Lakbay.Modules.Core {
    using Event = Utilities.Event;

    public interface IData {
        void Set(TextAsset textAsset);
        void SetController(MonoBehaviour controller);

    }

    public interface IData<T> : IData
        where T : IController {
        T controller { get; set; }

    }

    [Serializable]
    public class Data<T> : IData<T>
        where T : class, IController {
        [SerializeField, ReadOnly]
        protected T _controller;
        [YamlIgnore]
        public virtual T controller { get => _controller; set => _controller = value; }


        public Data() => Create(instance: this);

        public static Data<T> Create(
            IData<T> instance=null
        ) {
            instance ??= new Data<T>();

            return instance as Data<T>;

        }

        public static Data<T> Create(
            IData<T> data,
            IData<T> instance=null
        ) {
            return Create(
                instance
            );

        }

        public static Data<T> Create(TextAsset textAsset, IData<T> instance=null) {
            return Create(textAsset.Parse<Data<T>>(), instance);

        }

        public virtual void Set(TextAsset textAsset) => Create(textAsset, this); 

        public virtual void SetController(MonoBehaviour controller) => this.controller = controller as T;

    }

}