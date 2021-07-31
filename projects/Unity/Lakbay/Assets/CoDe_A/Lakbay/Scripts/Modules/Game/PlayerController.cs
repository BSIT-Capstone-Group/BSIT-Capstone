/*
 * Date Created: Sunday, July 25, 2021 5:04 AM
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
using CoDe_A.Lakbay.Modules.Core;

namespace CoDe_A.Lakbay.Modules.Game {
    using Event = Utilities.Event;
    using Input = Utilities.Input;


    public interface IPlayerController : Core.IMoveable {

        
    }

    public class PlayerController : Core.Controller, IPlayerController {
        public Vector3 from;
        public Vector3 to;

        public Tuple<GameObject, Move> OnMove() {
            return new Tuple<GameObject, Move>(gameObject, new Move(from, to, speed: 1.0f, duration: 2.0f));
            
        }

        public void OnMoving(GameObject gameObject, in Move move) {
            
        }

    }

}