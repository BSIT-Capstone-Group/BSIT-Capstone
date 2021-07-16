/*
 * Date Created: Tuesday, July 13, 2021 2:06 PM
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


    public interface IData : Core.Interactable.IData<Controller> {

        
    }

    [Serializable]
    public class Data : Core.Interactable.Data<Controller>, IData {


    }

}