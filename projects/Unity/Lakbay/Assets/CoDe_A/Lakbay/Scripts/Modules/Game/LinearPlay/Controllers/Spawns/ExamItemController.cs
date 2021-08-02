/*
 * Date Created: Sunday, July 25, 2021 11:45 AM
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

namespace CoDe_A.Lakbay.Modules.Game.LinearPlay.Controllers.Spawns {
    using Event = Utilities.Event;
    using Input = Utilities.Input;


    public interface IExamItemController : ISpawnController {

        
    }

    public class ExamItemController : SpawnController, IExamItemController {
        public override string OnPlot(in List<List2D<List2D<string>>> rows, in Vector2Int location, float chance) {
            return base.OnPlot(rows, location, chance);
            
        }

    }

}