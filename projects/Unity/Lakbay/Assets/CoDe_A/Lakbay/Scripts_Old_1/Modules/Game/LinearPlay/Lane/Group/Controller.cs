/*
 * Date Created: Saturday, June 26, 2021 6:28 AM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

using Ph.Com.CoDe_A.Lakbay.Utilities;

namespace Ph.Com.CoDe_A.Lakbay.Modules.Game.LinearPlay.Lane.Group {
    public class Controller : Input.Controller {
        [BoxGroup("Group.Controller")]
        public List<Lane.Controller> laneControllers = new List<Lane.Controller>();

        public override void Start() {
            base.Start();

            // foreach(var lc in laneControllers) {
            //     lc.Populate();
            //     lc.RegenerateHighlight();

            // }

            RegenerateHighlight();
            
        }

    }

}