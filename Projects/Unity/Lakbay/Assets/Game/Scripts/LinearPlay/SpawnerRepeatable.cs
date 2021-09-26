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

namespace Ph.CoDe_A.Lakbay.LinearPlay {
    public class SpawnerRepeatable : Repeatable {
        protected virtual GameObject[][] _spawnAreas {
            get {
                int i = 0;
                List<List<GameObject>> sas = new List<List<GameObject>>();
                sas.Add(new List<GameObject>());
                var sa = sas.Last();
                foreach(var spawnArea in spawnAreas) {
                    sa.Add(spawnArea);

                    if(i == columnCount - 1) {
                        i = 0;
                        sas.Add(new List<GameObject>());
                        sa = sas.Last();

                    } else i++;

                }

                return (from s in sas select s.ToArray()).ToArray();

            }

        }
        public int columnCount = 3;
        public List<GameObject> spawnAreas = new List<GameObject>();

        public override void OnRepeat(Repeater repeater, int index) {
            base.OnRepeat(repeater, index);
            PopulateSpawns(repeater as SpawnerRepeater, index);

        }

        public virtual void PopulateSpawns(SpawnerRepeater repeater, int index) {
            foreach(var spawnAreas in _spawnAreas.Enumerate()) {
                // int nindex = index + _spawnAreas.Where(
                //     (s, i) => i < spawnAreas.Key
                // ).Select((s) => s.Length).Count();
                // print(index, spawnAreas.Key, nindex);
                _PopulateSpawns(repeater, spawnAreas.Key + (index * _spawnAreas.Length), spawnAreas.Value);

            }

        }

        protected virtual void _PopulateSpawns(
            SpawnerRepeater repeater,
            int index,
            GameObject[] spawnAreas
        ) {
            if(repeater.index < repeater.spawnStartIndex) return;
            var spawns = (from a in spawnAreas select new List<Spawn>()).ToArray();
            var sas = spawnAreas.Enumerate().ToList();

            while(sas.Count != 0) {
                var spawnArea = sas.PopRandomly(); 
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
                        // print(spawn.name, index);
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