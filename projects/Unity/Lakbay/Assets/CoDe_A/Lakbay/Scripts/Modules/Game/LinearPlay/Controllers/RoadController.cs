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


    public interface IRoadController : Core.IController, Core.IInteractable {
        [YamlIgnore]
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

        public void OnLoad(TextAsset textAsset) {
            var d = textAsset.ParseYaml<RoadController>();

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

        public TextAsset OnSave() {
            return new TextAsset(this.AsYaml<IRoadController>());

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
        public virtual RowList Plot() => Plot(new Vector2Int(5, 210));

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
                    from ii in Enumerable.Range(0, columnCount)
                    select new List2D<string>()
                ));

            }

            foreach(var row in rows.Enumerate()) {
                var cis = Enumerable.Range(0, row.Value.GetCount()).ToList();

                while(!cis.IsEmpty()) {
                    int colKey = cis.PickRandomly();
                    cis.Remove(colKey);
                    var colValue = row.Value[colKey];

                    var location = new Vector2Int(colKey, row.Key);
                    float chance = UnityEngine.Random.value;

                    foreach(var spawnController in spawnControllers) {
                        var spawnable = spawnController as ISpawnable;
                        int colInterval = spawnable.column.indexInterval <= 0 ? 1 :
                            spawnable.column.indexInterval;
                        int rowInterval = spawnable.row.indexInterval <= 0 ? 1 :
                            spawnable.row.indexInterval;
                        int colMax = spawnable.column.maxCount <= -1 ? rows.GetCount() :
                            spawnable.column.maxCount;
                        int rowMax = spawnable.row.maxCount <= -1 ? row.Value.GetCount() :
                            spawnable.row.maxCount;
                        var colRange = spawnable.column.indexRange == null || spawnable.column.indexRange.GetCount() == 0 ?
                            Enumerable.Range(0, row.Value.GetCount()) : spawnable.column.indexRange;
                        var rowRange = spawnable.row.indexRange == null || spawnable.row.indexRange.GetCount() == 0 ?
                            Enumerable.Range(0, rows.GetCount()) : spawnable.row.indexRange;
                        int colStartIndex = spawnable.column.startIndex <= -1 ? 0 :
                            spawnable.column.startIndex;
                        int colEndIndex = spawnable.column.endIndex <= -1 ? row.Value.GetCount() - 1 :
                            spawnable.column.endIndex;
                        int rowStartIndex = spawnable.row.startIndex <= -1 ? 0 :
                            spawnable.row.startIndex;
                        int rowEndIndex = spawnable.row.endIndex <= -1 ? rows.GetCount() - 1 :
                            spawnable.row.endIndex;
                        colRange = colRange.Where((i) => i.Within(colStartIndex, colEndIndex));
                        rowRange = rowRange.Where((i) => i.Within(rowStartIndex, rowEndIndex));

                        var currentRowCounts = row.Value.Select((c) => c.Count((s) => s == spawnable.key));
                        int currentRowCount = currentRowCounts.Sum();
                        var currentColCounts = rows.Select((r) => r[colKey].Count((s) => s == spawnable.key));
                        int currentColCount = currentColCounts.Sum();

                        if(
                            !rowRange.Contains(row.Key) ||
                            !colRange.Contains(colKey) ||
                            currentRowCount >= rowMax ||
                            currentColCount >= colMax ||
                            row.Key % rowInterval != 0 ||
                            colKey % colInterval != 0 ||
                            chance > spawnable.chance
                        ) continue;

                        foreach(var notKey in spawnable.notKeys) {
                            while(colValue.Contains(notKey)) colValue.Remove(notKey);

                        }
                        
                        colValue.Add(spawnable.OnPlot(rows, location, chance));

                    }

                }

            }

            string str = AsPrettyString(rows);
            print(str);
            str.CopyToClipboard();

            return rows;

        }

        public virtual void Spawn(RowList rows) {
            gameObject.DestroyChildren(true);

            var scs = spawnControllers.Select((sc) => sc as ISpawnable);

            foreach(var row in rows.Enumerate()) {
                var gorow = new GameObject($"Row ({row.Key})");
                gorow.transform.SetParent(transform);

                foreach(var col in row.Value.Enumerate()) {
                    var gocol = new GameObject($"Column ({col.Key})");
                    gocol.transform.SetParent(gorow.transform);
                    
                }

            }

            var rows_ = gameObject.GetChildren().Select((g) => g.GetChildren().ToList()).ToList();

            foreach(var row in gameObject.GetChildren().Enumerate()) {
                var gorow = row.Value;
                var gorows = gameObject.GetChildren();
                float zpos = 0.0f;
                if(gorows != null && gorows.GetCount() > 0) {
                    zpos = gorows.Sum((g) => g.GetSize().z);

                }
                var gorowpos = gorow.transform.localPosition;
                gorow.transform.localPosition = new Vector3(
                    0.0f, 0.0f, zpos
                );

                foreach(var col in row.Value.GetChildren().Enumerate()) {
                    var gocol = rows_[row.Key][col.Key];
                    var gocols = gorow.GetChildren();
                    float xpos = 0.0f;
                    if(gocols != null && gocols.GetCount() > 0) {
                        xpos = gocols.Sum((g) => g.GetSize().x);

                    }
                    var gocolpos = gocol.transform.localPosition;
                    gocol.transform.localPosition = new Vector3(
                        xpos, 0.0f, 0.0f
                    );

                    foreach(var spawnKey in rows[row.Key][col.Key]) {
                        if(scs.TryFind((sc) => sc.key == spawnKey, out var sc)) {
                            var go = sc.OnSpawn(
                                rows_,
                                new Vector2Int(col.Key, row.Key)
                            );
                            if(go) Instantiate(go, gocol.transform);

                        }

                    }

                }

            }

        }

    }

}