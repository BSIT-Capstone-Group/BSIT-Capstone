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

namespace CoDe_A.Lakbay.ModulesOld4.Game.Road {
    using Event = Utilities.Event;
    using Input = Utilities.Input;
    using BaseIController = Core.Interactable.IController<Data>;
    using BaseController = Core.Interactable.Controller<Data>;
    using Columnn = List<string[]>;
    using Row = List<List<string[]>>;

    public interface IController : BaseIController {
        // SpawnGameObjectsList spawnGameObjects { get; set; }

        Vector2Int size { get; set; }
        List<Spawn.Controller> spawnControllers { get; set; }

        void OnRowsChange(List<Row> old, List<Row> @new);
        
    }

    public class Controller : BaseController, IController {
        public new const string BoxGroupName = "Road.Controller";

        // public Sample sample;

        // [BoxGroup(BoxGroupName)]
        // [SerializeField]
        // protected SpawnGameObjectsList _spawnGameObjects = new SpawnGameObjectsList();
        // [YamlIgnore]
        // public virtual SpawnGameObjectsList spawnGameObjects {
        //     get => _spawnGameObjects;
        //     set => Helper.SetInvoke(this, ref _spawnGameObjects, value);

        // }

        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected Vector2Int _size;
        // [YamlIgnore]
        public virtual Vector2Int size {
            get => _size;
            set {
                var r = Helper.SetInvoke(this, ref _size, value);
                // if(r.Item1) controller?.OnRowsChange(r.Item2[0], r.Item2[1]);

            }

        }
        // [BoxGroup(BoxGroupName)]
        // [SerializeField]
        // protected SpawnList _spawns;
        // // [YamlIgnore]
        // public virtual SpawnList spawns {
        //     get => _spawns;
        //     set {
        //         var r = Helper.SetInvoke(this, ref _spawns, value);
        //         // if(r.Item1) controller?.OnRowsChange(r.Item2[0], r.Item2[1]);

        //     }

        // }
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected List<Spawn.Controller> _spawnControllers = new List<Spawn.Controller>();
        public List<Spawn.Controller> spawnControllers {
            get => _spawnControllers;
            set {
                var r = Helper.SetInvoke(this, ref _spawnControllers, value);
                // if(r.Item1) controller?.OnRowsChange(r.Item2[0], r.Item2[1]);

            }

        }


        public virtual void OnRowsChange(List<Row> old, List<Row> @new) {
            // GetComponent<Renderer>().bounds.s

        }

        public override void OnPlayingChange(bool old, bool @new) {
            base.OnPlayingChange(old, @new);
            // Road.Spawn.None.has
        }

        public override void Update() {
            base.Update();
            // print(data.rows.Count, data.rows != null, transform.childCount != data.rows.Count);
            if(data.rows != null && transform.childCount != data.rows.Count) {
                Spawn();

            }

        }

        public virtual void Spawn(Spawn.Controller controller, GameObject column, Vector2Int point) {
            var go = controller?.OnSpawn(point);
            
            if(go) {
                go = Instantiate(go, column.transform);
                go.name = "[" + string.Join(",", controller.keys) + "]";

            }

        }

        public virtual void Spawn() {
            gameObject.DestroyChildren();

            if(data.rows != null) {
                foreach(var row in data.rows.Enumerate()) {
                    var gorows = gameObject.GetChildren();
                    float zpos = 0.0f;
                    if(gorows != null && gorows.GetCount() > 0) {
                        zpos = gorows.Sum((g) => g.GetSize().z);

                    }

                    var gorow = new GameObject($"Row ({row.Key})");
                    var gorowpos = gorow.transform.localPosition;
                    gorow.transform.SetParent(transform);
                    gorow.transform.localPosition = new Vector3(
                        0.0f, 0.0f, zpos
                    );

                    foreach(var col in row.Value.Enumerate()) {
                        var gocols = gorow.GetChildren();
                        float xpos = 0.0f;
                        if(gocols != null && gocols.GetCount() > 0) {
                            xpos = gocols.Sum((g) => g.GetSize().x);

                        }

                        var gocol = new GameObject($"Column ({col.Key})");
                        var gocolpos = gocol.transform.localPosition;
                        gocol.transform.SetParent(gorow.transform);
                        gocol.transform.localPosition = new Vector3(
                            xpos, 0.0f, 0.0f
                        );

                        // var flags = col.Value.GetFlags(true);
                        foreach(var spawn in col.Value) {
                            if(spawn == null || spawn.GetCount() == 0) {

                            } else {
                                foreach(var key in spawn.Enumerate()) {
                                    if(spawnControllers.TryFind((s) => s.keys.Contains(key.Value), out var c)) {
                                        Spawn(c, gocol, new Vector2Int(col.Key, row.Key));

                                    }

                                }

                                if(
                                    spawnControllers.TryFind((s) => s.keys.Equals(spawn), out var cc)
                                ) {
                                    Spawn(cc, gocol, new Vector2Int(col.Key, row.Key));

                                }

                            }

                        }

                    }

                }

            }   

        }
        
        [ContextMenu("Generate Rows As Yaml")]
        public void GenerateRowsAsYaml() {
            var s = Data.GenerateRowsAsYaml(
                size,
                spawnControllers
            );
            s.CopyToClipboard();
            print(s);


        }

    }

}