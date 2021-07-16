/*
 * Date Created: Monday, July 12, 2021 5:47 PM
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

namespace CoDe_A.Lakbay.ModulesOld3.Core.Input {
    using Event = Utilities.Event;


    public interface IController : Core.IController, IPropertyEvent {
        void ReceiveInput(Keyboard keyboard, Mouse mouse, Touchscreen touchscreen);
        
    }

    public class Controller : Core.Controller, IController {
        public new const string BoxGroupName = "Input.Controller";

        [BoxGroup(BoxGroupName)]
        [SerializeField] protected bool _active;
        public virtual bool active {
            get => _active;
            set => Helper.SetInvoke(this, ref _active, value, onActiveChange, OnActiveChange);

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField] protected Event.OnBoolChange<IEvent> _onActiveChange = new Event.OnBoolChange<IEvent>();
        public virtual Event.OnBoolChange<IEvent> onActiveChange => throw new NotImplementedException();


        public Controller() : base() { Data.Create(instance: this); }

        public virtual void OnActiveChange(bool old, bool @new) {


        }

        public virtual void ReceiveInput(Keyboard keyboard, Mouse mouse, Touchscreen touchscreen) {


        }

        public override void Update() {
            base.Update();
            if(active) {
                ReceiveInput(
                    Utilities.Input.keyboard,
                    Utilities.Input.mouse,
                    Utilities.Input.touchscreen
                );

            }

        }

    }

}