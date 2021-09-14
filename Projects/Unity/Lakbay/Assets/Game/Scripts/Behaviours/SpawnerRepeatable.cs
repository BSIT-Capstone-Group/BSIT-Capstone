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
    public class SpawnerRepeatable : Repeatable {
        public List<GameObject> spawnAreas = new List<GameObject>();

        public override void OnRepeat(Repeater repeater, int index) {
            base.OnRepeat(repeater, index);
            PopulateSpawns(repeater as SpawnerRepeater, index);

        }

        public virtual void PopulateSpawns(SpawnerRepeater repeater, int index) {
            var spawns = (from a in spawnAreas select new List<Spawn>()).ToArray();

            foreach(var spawnArea in spawnAreas.Enumerate()) {
                spawnArea.Value.DestroyChildren();
                float chance = UnityEngine.Random.value;
                foreach(var spawn in repeater.spawns) {
                    if(
                        chance < spawn.chance &&
                        index % spawn.rowInterval == 0 &&
                        spawnArea.Key % spawn.columnInterval == 0 &&
                        spawns.Select(
                            (sl) => sl.Count((s) => s.IsInstance(spawn.GetType()))
                        ).Sum() != spawn.maxCount
                    ) {
                        var sp = spawn.OnSpawn(spawns, spawnArea.Key);
                        if(sp) spawns[spawnArea.Key].Add(sp);

                    }

                }

            }

            foreach(var spawnList in spawns.Enumerate()) {
                foreach(var spawn in spawnList.Value) {
                    Instantiate(spawn, spawnAreas[spawnList.Key].transform);

                }

            }

        }

    }

}