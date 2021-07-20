/*
 * Date Created: Monday, July 19, 2021 10:27 PM
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

namespace CoDe_A.Lakbay.ModulesOld4.Game.Road.Spawn.Ground {
    using Event = Utilities.Event;
    using Input = Utilities.Input;
    using Base = Spawn;


    public interface IData : Base.IData<Controller> {
        
    }

    [Serializable]
    public class Data : Base.Data<Controller>, IData {
        public new const string HeaderName = "Ground.Data";

    }

}