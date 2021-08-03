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
        
    }

    public class PowerUpController : SpawnController, IPowerUpController {
        public override GameObject OnSpawn(in List<List<GameObject>> rows, in Vector2Int location) {
            var go = gameObjects.PickRandomly();
            var rowPowerUps = rows[location.y].Select(
                (g) => g.GetChildren().Select((g) => g.name).Where(
                    (s) => gameObjects.Contains((g) => g.name == s)
                )
            );

            var used = gameObjects.Select((p) => rowPowerUps.Find((l) => l.Contains(p.name)) != null);
            // Continuously pick at random so that
            // the same types of powerup won't appear in a single row.
            while(
                rowPowerUps.Find((l) => l.Contains(go.name)) != null &&
                !used.All()
            ) {
                go = gameObjects.PickRandomly();

            }

            return go;

        }

    }

}