/*
 * Date Created: Monday, August 2, 2021 2:51 PM
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


    public interface IPowerUpController : ISpawnController {
        List<GameObject> powerUps { get; set; }
        
    }

    public class PowerUpController : SpawnController, IPowerUpController {
        [SerializeField]
        private List<GameObject> _powerUps = new List<GameObject>();
        public virtual List<GameObject> powerUps { get => _powerUps; set => _powerUps = value; }


        public override GameObject OnSpawn(in List<List<GameObject>> rows, in Vector2Int location) {
            var go = powerUps.PickRandomly();
            var rowPowerUps = rows[location.y].Select(
                (g) => g.GetChildren().Select((g) => g.name).Where(
                    (s) => powerUps.Contains((g) => g.name == s)
                )
            );

            var f = rowPowerUps.Find((l) => l.Contains(go.name));
            var used = powerUps.Select((p) => rowPowerUps.Find((l) => l.Contains(p.name)) != null);
            while(f != null && !used.All()) {
                go = powerUps.PickRandomly();
                f = rowPowerUps.Find((l) => l.Contains(go.name));

            }

            return go;

        }

    }

}