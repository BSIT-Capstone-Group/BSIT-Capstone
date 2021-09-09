/*
 * Date Created: Thursday, September 9, 2021 4:04 PM
 * Author: NI.L.A
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

using TMPro;

using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Behaviours {
    public class EventAndInput : Controller {
        public override void Awake() {
            base.Awake();
            if(!Game.initialized) DontDestroyOnLoad(gameObject);

        }

    }

}