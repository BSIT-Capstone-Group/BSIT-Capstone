/*
 * Date Created: Tuesday, July 13, 2021 8:39 PM
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

namespace CoDe_A.Lakbay.Modules.Game.Road {
    using Event = Utilities.Event;
    using Input = Utilities.Input;
    using BaseIController = Core.Interactable.IController<Data>;
    using BaseController = Core.Interactable.Controller<Data>;
    using Row = List<Spawn>;


    public interface IController : BaseIController {
        void OnRowsChange(List<Row> old, List<Row> @new);
        
    }

    public class Controller : BaseController, IController {
        public virtual void OnRowsChange(List<Row> old, List<Row> @new) {
            // GetComponent<Renderer>().bounds.s

        }

        public override void OnPlayingChange(bool old, bool @new) {
            base.OnPlayingChange(old, @new);
            // Road.Spawn.None.has
        }

        public override void Update() {
            base.Update();
            if(data.rows != null && transform.childCount != data.rows.Count) {
                Spawn();

            }

        }

        public virtual void Spawn() {
            gameObject.DestroyChildren();

            if(data.rows != null) {
                foreach(var row in data.rows) {
                    var ri = data.rows.IndexOf(row);
                    var rowgo = new GameObject($"Row ({ri})");
                    rowgo.transform.SetParent(gameObject.transform);
                    var rpos = rowgo.transform.localPosition;
                    rowgo.transform.localPosition = new Vector3(
                        0, 0, data.rowSize.z * ri
                    );

                    int si = 0;
                    var spawns_ = new List<List<GameObject>>();
                    foreach(var spawns in row) {
                        var spawngo = new GameObject($"Spawns ({si})");
                        spawngo.transform.SetParent(rowgo.transform);
                        var spos = spawngo.transform.localPosition;
                        spawngo.transform.localPosition = new Vector3(
                            data.spawnSize.x * si, 0, 0
                        );

                        spawns_.Add(new List<GameObject>());
                        foreach(var spawn in spawns.GetFlags()) {
                            if(spawn.Equals(Road.Spawn.None)) {


                            } else {
                                var spgo = data.spawnGameObjects.Find((g) => g.type == spawn);
                                var gos = spgo.gameObjects;
                                if(gos != null) {
                                    var go = gos.PickRandomly();

                                    if(spawn == Road.Spawn.Ground) {
                                        if(gos.Count >= 2 && (si == 0 || si == row.Count - 1)) {
                                            go = gos[0];

                                        } else go = gos[1];

                                    } else if(spawn == Road.Spawn.PowerUp) {
                                        var last = spawns_.Last();
                                        while(last != null && last.Contains(go)) {
                                            go = gos.PickRandomly();

                                        } 

                                    }
                                    
                                    spawns_.Last().Add(go);

                                    go = Instantiate(go, spawngo.transform);
                                    go.name = $"{spawn}";
                                    
                                }

                            }

                        }

                        si++;

                    }

                }

            }   

        }
        
        [ContextMenu("Generate Rows As Yaml")]
        public void GenerateRowsAsYaml() {
            var s = Data.GenerateRowsAsYaml(3, 200, obstaclesChance: 0.50f, powerUpsGap: 6);
            s.CopyToClipboard();
            print(s);

        }

    }

}