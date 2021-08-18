/*
 * Date Created: Sunday, July 25, 2021 4:24 AM
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

namespace CoDe_A.Lakbay.Modules.Core.Controllers {
    using Event = Utilities.Event;
    using Input = Utilities.Input;


    public interface IMoveController {
        void Move(IMoveable moveable);
        void Move(Controller moveable);
        
    }

    public class MoveController : Controller, IMoveController {
        public static MoveController[] instances => GameObject.FindObjectsOfType
            <MoveController>();
        public static MoveController instance => !instances.IsEmpty() ? instances[0] : null;

        public virtual void Move(IMoveable moveable) {
            var rv = moveable.OnMove();
            var go = rv.Item1;
            var move = rv.Item2;

            Helper.DoOver(
                this,
                move.duration,
                null,
                (t, e, d) => {
                    moveable.OnMoving(go, move);
                    go.transform.localPosition = move.current;
                    move.progress = e / d;
                    return t * move.speed;

                },
                () => go.transform.localPosition = move.to,
                true
            );

        }

        public virtual void Move(Controller moveable) => Move(moveable as IMoveable);
        
    }

}