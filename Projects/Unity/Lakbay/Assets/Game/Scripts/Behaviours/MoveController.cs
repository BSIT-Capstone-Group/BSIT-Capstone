/*
 * Date Created: Wednesday, August 25, 2021 11:20 PM
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
using UnityEngine.UI;

using TMPro;

using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Behaviours {
    public struct Move {


    }

    public interface IMoveable {
        GameObject OnMove();

    }

    public class MoveController : Controller {
        // public IEnumerator 

    }

}