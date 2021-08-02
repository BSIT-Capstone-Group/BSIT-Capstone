/*
 * Date Created: Sunday, July 25, 2021 9:47 AM
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


    public interface IGroundController : ISpawnController {
        GameObject inner { get; set; }
        GameObject outer { get; set; }
        
    }

    public class GroundController : SpawnController, IGroundController {
        [SerializeField]
        private GameObject _inner;
        public virtual GameObject inner { get => _inner; set => _inner = value; }
        [SerializeField]
        private GameObject _outer;
        public virtual GameObject outer { get => _outer; set => _outer = value; }


        public override GameObject OnSpawn(in List<List<GameObject>> rows, in Vector2Int location) {
            int colCount = rows.GetCount() != 0 ? rows[0].GetCount() : 0;
            int startIndex = column.startIndex <= -1 ? 0 : column.startIndex;
            int endIndex = column.endIndex <= -1 ? rows[0].GetCount() : column.endIndex;
            
            if(location.x.Either(startIndex, endIndex)) {
                return outer;

            } else return inner;

        }

    }

}