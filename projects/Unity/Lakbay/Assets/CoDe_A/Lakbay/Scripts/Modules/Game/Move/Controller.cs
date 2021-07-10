/*
 * Date Created: Wednesday, July 7, 2021 12:42 PM
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

namespace CoDe_A.Lakbay.Modules.Game.Move {
    using Event = Utilities.Event;

    public interface IController : Core.Transition.IController<Transform>, IPropertyEvent {

    }

    [ExecuteAlways]
    public class Controller : Core.Transition.Controller<Transform>, IController {
        public new const string BoxGroupName = "Move.Controller";

        [ContextMenu("Set Data")]
        public override void SetData() {
            Data.Create(dataTextAsset, this);
            
        }

        public override void Update() {
            base.Update();
            if(paused || !to || !from) return;
            transform.position = (to.position - from.position) * progress + from.position;

        }

        public override void OnPausedChange(bool old, bool @new) {
            StopAllCoroutines();

            if(@new) {

            } else {
                Helper.DoOver(
                    this,
                    duration,
                    null,
                    (t, e, d) => {
                        if(paused) return 0;
                        progress = Easing.EaseInBack(0.0f, 1.0f, e / d);
                        return t * speed;

                    },
                    () => progress = 1.0f,
                    true
                );

            }

        }

        
        // [ContextMenu("hehe")]
        // public override void hehe() => print("hoho");

    }

}