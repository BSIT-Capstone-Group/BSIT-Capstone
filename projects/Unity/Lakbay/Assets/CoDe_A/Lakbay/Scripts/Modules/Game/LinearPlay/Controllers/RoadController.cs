/*
 * Date Created: Tuesday, July 20, 2021 7:05 PM
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
using CoDe_A.Lakbay.Modules.Core;

namespace CoDe_A.Lakbay.Modules.Game.LinearPlay.Controllers {
    using Event = Utilities.Event;
    using Input = Utilities.Input;
    using RowList = List<List2D<List2D<string>>>;


    public interface IRoadController : Core.IInteractable {
        List<Controller> spawnControllers { get; set; }

        RowList Plot(Vector2Int count);
        void Spawn(RowList road);
        
    }

    public class RoadController : Core.Controller, IRoadController {
        [SerializeField]
        private List<Controller> _spawnControllers = new List<Controller>();
        public List<Controller> spawnControllers { get => _spawnControllers; set => _spawnControllers = value; }


        public Tuple<GameObject, Focus> OnFocus()
        {
            throw new NotImplementedException();
        }

        public void OnLoad(TextAsset textAsset)
        {
            throw new NotImplementedException();
        }

        public void OnPause()
        {
            throw new NotImplementedException();
        }

        public void OnPlay()
        {
            throw new NotImplementedException();
        }

        public void OnResume()
        {
            throw new NotImplementedException();
        }

        public TextAsset OnSave()
        {
            throw new NotImplementedException();
        }

        public void OnStop()
        {
            throw new NotImplementedException();
        }

        public void OnTutorialHide()
        {
            throw new NotImplementedException();
        }

        public List<Entry> OnTutorialShow()
        {
            throw new NotImplementedException();
        }

        public GameObject OnUnfocus()
        {
            throw new NotImplementedException();
        }

        public virtual string AsPrettyString(RowList rows) {
            var str = "";
            
            foreach(var row in rows) {
                var srow = "- [{0}]\n";

                var scols = new List<string>();
                foreach(var col in row) {
                    var scol = col.ToString(true);
                    scols.Add(scol);

                }

                str += string.Format(srow, string.Join(", ", scols));

            }

            return str;

        }
        
        [ContextMenu("Plot")]
        public virtual RowList Plot() => Plot(new Vector2Int(3, 100));

        [ContextMenu("Spawn")]
        public virtual void Spawn() => Spawn(Plot());

        public virtual RowList Plot(Vector2Int count) {
            var scs = spawnControllers.Select((sc) => sc as ISpawnable);

            int columnCount = count.x;
            int rowCount = count.y;

            // Road rows = new Road(columnCount, rowCount);
            
            var rows = new RowList();

            for(int i = 0; i < rowCount; i++) {
                rows.Add(new List2D<List2D<string>>(
                    from ii in Enumerable.Range(0, columnCount) select new List2D<string>()

                ));

            }
            
            var ris = Enumerable.Range(0, rows.GetCount()).Shuffle();

            foreach(var ri in ris) {
                var cis = Enumerable.Range(0, rows[ri].GetCount()).Shuffle();

                foreach(var ci in cis) {
                    var col = rows[ri][ci];
                    float chance = UnityEngine.Random.value;

                    foreach(var sc in scs) {
                        var colRange = sc.column.range.AsEnumerable();
                        colRange = colRange == null || colRange.GetCount() == 0 ?
                            Enumerable.Range(0, columnCount) : colRange;
                        var rowRange = sc.row.range.AsEnumerable();
                        rowRange = rowRange == null || rowRange.GetCount() == 0 ?
                            Enumerable.Range(0, rowCount) : rowRange;

                        int ccount = rows.Select(
                            (r) => r[ci].Count((l) => l == sc.key)
                        ).Sum();
                        int rcount = rows[ri].Select((l) => l.Count((s) => s == sc.key)).Sum();

                        int cinterval = sc.column.interval <= 0 ? 1 : sc.column.interval;
                        int rinterval = sc.row.interval <= 0 ? 1 : sc.row.interval;

                        int cmax = sc.column.max <= -1 ? cis.Max() : sc.column.max;
                        int rmax = sc.row.max <= -1 ? ris.Max() : sc.row.max;

                        print(sc.key);

                        if(colRange.Contains(ci) && rowRange.Contains(ri)) {
                            if(ccount >= cmax || rcount >= rmax)
                                continue;

                            print("1");

                            if(chance > sc.chance) continue;

                            print("2");

                            if(ri % rinterval != 0 || ci % cinterval != 0) continue;

                            print("3");

                            // while(col.Contains(sc.notKey)) col.Remove(sc.notKey);
                            var k = sc.OnPlot(rows, new Vector2Int(ci, ri), chance);
                            
                            col.Add(k ?? "");

                        } else continue;

                    }

                }

            }

            var str = AsPrettyString(rows);
            print(str);
            str.CopyToClipboard();

            return rows;

        }

        public virtual void Spawn(RowList rows) {
            gameObject.DestroyChildren();

            var scs = spawnControllers.Select((sc) => sc as ISpawnable);

            foreach(var row in rows.Enumerate()) {
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

                    foreach(var spawnKey in col.Value) {
                        if(scs.TryFind((sc) => sc.key == spawnKey, out var sc)) {
                            var go = sc.OnSpawn(rows, new Vector2Int(col.Key, row.Key));
                            if(go) Instantiate(go, gocol.transform);

                        }

                    }

                }

            }

        }

    }

}