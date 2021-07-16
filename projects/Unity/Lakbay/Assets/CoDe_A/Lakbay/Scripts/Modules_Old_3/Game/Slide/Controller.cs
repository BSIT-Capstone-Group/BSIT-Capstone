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
using UnityEngine.InputSystem;

using NaughtyAttributes;
using TMPro;
using YamlDotNet.Serialization;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.ModulesOld3.Game.Slide {
    using Event = Utilities.Event;

    public interface IController : Core.Transition.Vector3.IController, IPropertyEvent {

    }

    public class Controller : Core.Transition.Vector3.Controller, IController {
        public new const string BoxGroupName = "Slide.Controller";

        public override Vector3 from {
            get {
                return new Vector3(base.from.x, base.from.y, transform.position.z);
            }
            set => base.from = value;

        }

        public override Vector3 to {
            get {
                return new Vector3(base.to.x, base.to.y, transform.position.z);
            }
            set => base.to = value;

        }

        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected int _index;
        public virtual int index {
            get => _index;
            set => Helper.SetInvoke(this, ref _index, value, onIndexChange, OnIndexChange);
        }

        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected Event.OnIntChange<IEvent> _onIndexChange = new Event.OnIntChange<IEvent>();
        public Event.OnIntChange<IEvent> onIndexChange => _onIndexChange;


        public override void Update() {
            base.Update();
            if(playing && (from != null && to != null)) {
                transform.position = ((to - from) * progress) + from;

            }

        }

        public virtual void OnIndexChange(int old, int @new) {
            print("changing...");
            var d = @new - old;
            transform.position += (Vector3.right * 5) * d;

        }

        public override void ReceiveInput(Keyboard keyboard, Mouse mouse, Touchscreen touchscreen) {
            if(keyboard.leftArrowKey.wasPressedThisFrame) index--;
            if(keyboard.rightArrowKey.wasPressedThisFrame) index++;

        }

    }

}