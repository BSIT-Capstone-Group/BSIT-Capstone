/*
 * Date Created: Sunday, July 25, 2021 8:23 AM
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

namespace CoDe_A.Lakbay.Modules.Game.LinearPlay {
    using Event = Utilities.Event;
    using Input = Utilities.Input;


    /// <summary>Anything that can be spawned must inherit from this.</summary>
    public interface ISpawnable {
        string key { get; set; }
        List<string> notKeys { get; set; }
        float chance { get; set; }
        AxisInfo column { get; set; }
        AxisInfo row { get; set; }
        
        string OnPlot(in List<List2D<List2D<string>>> rows, in Vector2Int location, float chance);
        GameObject OnSpawn(in List<List<GameObject>> root, in Vector2Int location);
        
    }

}