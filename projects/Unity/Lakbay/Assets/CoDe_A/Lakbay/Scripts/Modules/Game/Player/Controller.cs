/*
 * Date Created: Tuesday, July 13, 2021 2:05 PM
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

namespace CoDe_A.Lakbay.Modules.Game.Player {
    using Event = Utilities.Event;
    using Input = Utilities.Input;


    public interface IController : Core.Interactable.IController {

        
    }

    public class Controller : Core.Interactable.Controller<Data>, IController {
        // public override Data data {
        //     get => base.data;
        //     set {
        //         var old = data;
        //         base.data = value;
        //         var @new = data;
        //         old?.SetController(null);
        //         @new?.SetController(this);

        //     }
        // }

        [ContextMenu("Randomize")]
        public void a(){
            data.label = UnityEngine.Random.value.ToString();
            
        }

    }

}