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


    public interface IController : Core.IController {
        void OnLabelChange(string old, string @new);
        void OnDescriptionChange(string old, string @new);
        
    }

    public class Controller : Core.Controller, IController {
        public new const string BoxGroupName = "Minimal.Controller";

        public virtual void OnDescriptionChange(string old, string @new) {
            

        }

        public virtual void OnLabelChange(string old, string @new) {


        }
        
    }

    public interface IController<T> : Core.IController<T>, IController
        where T : IData {
        
    }

    public class Controller<T> : Controller, IController<T>
        where T : IData, new() {
        public override TextAsset dataTextAsset {
            get => base.dataTextAsset;
            set => Core.Controller<T>.SetDataTextAsset(this, ref _dataTextAsset, value);

        }

        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected T _data;
        public virtual T data {
            get => _data;
            set => Core.Controller<T>.SetData(this, ref _data, value);

        }


        public Controller() : base() {
            data = new T();

        }

        [ContextMenu("Reset")]
        public override void Reset() => data = new T();

    }

}