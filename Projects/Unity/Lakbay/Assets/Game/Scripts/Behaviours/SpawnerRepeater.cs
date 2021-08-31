/*
 * Date Created: Monday, August 30, 2021 7:35 AM
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
    public class SpawnerRepeater : Repeater {
        protected bool _spawned = false;
        public List<GameObject> spawnAreas = new List<GameObject>();
        public virtual SpawnerRepeaterHandler spawnerHandler => handler as SpawnerRepeaterHandler;

        public override void OnRepeat() {
            var snext = next as SpawnerRepeater;
            if(!snext._spawned) {
                snext.PopulateSpawns();
                snext._spawned = true;

            }

        }

        public override void OnFree() {
            _spawned = false;

        }

        public virtual void PopulateSpawns() {
            foreach(var spawnArea in spawnAreas) {
                spawnArea.DestroyChildren();
                float chance = UnityEngine.Random.value;
                foreach(var spawn in spawnerHandler.spawns) {
                    if(chance < spawn.chance) {
                        var nspawn = Instantiate(spawn, spawnArea.transform);

                    }

                }

            }

        }

    }

}