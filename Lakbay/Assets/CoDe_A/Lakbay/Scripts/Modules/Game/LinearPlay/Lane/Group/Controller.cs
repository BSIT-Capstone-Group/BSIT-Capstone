using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.Modules.Game.LinearPlay.Lane.Group {
    public class Controller : Input.Controller {
        [BoxGroup("Group.Controller")]
        public List<Lane.Controller> laneControllers = new List<Lane.Controller>();

    }

}