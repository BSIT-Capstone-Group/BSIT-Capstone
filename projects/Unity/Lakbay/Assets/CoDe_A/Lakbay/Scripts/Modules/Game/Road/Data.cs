/*
 * Date Created: Tuesday, July 13, 2021 8:40 PM
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
using RotaryHeart.Lib.SerializableDictionary;
using YamlDotNet.Serialization;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.Modules.Game.Road {
    using Event = Utilities.Event;
    using Input = Utilities.Input;
    using BaseIData = Core.Interactable.IData<Controller>;
    using BaseData = Core.Interactable.Data<Controller>;
    using Row = List<Spawn>;
    using SpawnGameObjectsList = List<SpawnGameObject>;

    [Serializable, Flags]
    public enum Spawn {
        None = 1,
        Ground = 2,
        Obstacle = 4,
        PowerUp = 8,
        ExamItem = 16,
        
    }

    [Serializable]
    public struct SpawnGameObject {
        public Spawn type;
        public List<GameObject> gameObjects;

    }

    public interface IData : BaseIData {
        Vector3 rowSize { get; }
        Vector3 spawnSize { get; }

        SpawnGameObjectsList spawnGameObjects { get; set; }
        List<Row> rows { get; set; }

        Event.OnValueChange<Controller, List<Row>> onRowsChange { get; }
        
    }

    [Serializable]
    public class Data : BaseData, IData {
        [YamlIgnore]
        public virtual Vector3 rowSize {
            get {
                if(controller && controller.transform.childCount > 0) {
                    var c = controller.transform.GetChild(0);
                    return c.gameObject.GetSize();

                }

                return Vector3.zero;

            }

        }
        [YamlIgnore]
        public virtual Vector3 spawnSize {
            get {
                if(controller && controller.transform.childCount > 0) {
                    var c = controller.transform.GetChild(0);
                    if(c && c.childCount > 0) {
                        var s = c.GetChild(0);
                        return s.gameObject.GetSize();

                    }

                }

                return Vector3.zero;

            }

        }

        [SerializeField]
        protected SpawnGameObjectsList _spawnGameObjects = new SpawnGameObjectsList();
        [YamlIgnore]
        public virtual SpawnGameObjectsList spawnGameObjects {
            get => _spawnGameObjects;
            set => Helper.SetInvoke(controller, ref _spawnGameObjects, value);

        }
        [SerializeField]
        protected List<Row> _rows;
        // [YamlIgnore]
        public virtual List<Row> rows {
            get => _rows;
            set {
                var r = Helper.SetInvoke(controller, ref _rows, value, onRowsChange);
                if(r.Item1) controller?.OnRowsChange(r.Item2[0], r.Item2[1]);

            }

        }

        [SerializeField]
        protected Event.OnValueChange<Controller, List<Row>> _onRowsChange = new Event.OnValueChange<Controller, List<Row>>();
        [YamlIgnore]
        public virtual Event.OnValueChange<Controller, List<Row>> onRowsChange => _onRowsChange;


        public Data() => Create(instance: this);

        public static Data Create(
            List<Row> rows=null,
            BaseIData data=null,
            IData instance=null
        ) {
            instance ??= new Data();
            BaseData.Create(data, instance);
            instance.rows = rows ?? new List<Row>();

            return instance as Data;

        }

        public static Data Create(
            IData data,
            IData instance=null
        ) {
            data ??= new Data();
            return Create(
                data.rows,
                data,
                instance
            );

        }

        public static Data Create(TextAsset textAsset, IData instance=null) {
            return Create(textAsset.Parse<Data>(), instance);

        }

        public override void Set(TextAsset textAsset) => Create(textAsset, this);

        public static List<Row> GenerateRows(
            int columnCount,
            int rowCount,
            Func<List<Row>, int, int, Row> rowGenerator,
            int safeRowCount=5,
            Spawn safeRowSpawn=Spawn.Ground
        ) {
            var rows = new List<Row>();

            for(int i = 0; i < rowCount; i++) {
                var row = rowGenerator.Invoke(new List<Row>(rows), columnCount, rowCount);
                row ??= new Row();
                while(row.Count != columnCount) row.Add(Spawn.None);
                rows.Add(row);

            }

            var safeRows = from i in Enumerable.Range(0, safeRowCount)
                select (
                    from i_ in Enumerable.Range(0, columnCount) select safeRowSpawn
                ).ToList();

            rows.InsertRange(0, safeRows);
            rows.AddRange(safeRows);

            return rows;

        }

        public static string GenerateRowsAsYaml(
            int columnCount,
            int rowCount,
            Func<List<Row>, int, int, Row> rowGenerator,
            int safeRowCount=5,
            Spawn safeRowSpawn=Spawn.Ground
        ) {
            return GenerateRowsAsYaml(GenerateRows(
                columnCount,
                rowCount,
                rowGenerator,
                safeRowCount,
                safeRowSpawn
            ));
                
        }

        public static string GenerateRowsAsYaml(List<Row> rows) {
            var s = "";
            
            foreach(var row in rows) {
                var rf = "- [{0}]\n";
                var ris = "";

                int ii = 0;
                foreach(var i in row) {
                    ii++;

                    ris += ((int) i).ToString().PadLeft(2, '0');
                    ris += ii != row.Count ? ", " : "";

                }

                s += string.Format(rf, ris);

            }

            return s;

        }

        public static List<Row> GenerateRows(
            int columnCount,
            int rowCount,
            int examItemsCount=20,
            int examItemsRowGap=0,
            float obstaclesChance=0.75f,
            int obstaclesRowGap=3,
            float powerUpsChance=0.20f,
            int powerUpsGap=4
        ) {
            return GenerateRows(
                columnCount, rowCount,
                (rs, c, r) => _RowGenerator(
                    rs, c, r,
                    examItemsCount,
                    examItemsRowGap,
                    obstaclesChance,
                    obstaclesRowGap,
                    powerUpsChance,
                    powerUpsGap
                )
            );

        }
        
        public static string GenerateRowsAsYaml(
            int columnCount,
            int rowCount,
            int examItemsCount=20,
            int examItemsRowGap=0,
            float obstaclesChance=0.75f,
            int obstaclesRowGap=3,
            float powerUpsChance=0.20f,
            int powerUpsGap=4
        ) {
            return GenerateRowsAsYaml(
                GenerateRows(
                    columnCount,
                    rowCount,
                    examItemsCount,
                    examItemsRowGap,
                    obstaclesChance,
                    obstaclesRowGap,
                    powerUpsChance,
                    powerUpsGap
                )
            );
        }

        private static Row _RowGenerator(
            List<Row> rows,
            int columnCount,
            int rowCount,
            int examItemsCount=20,
            int examItemsGap=0,
            float obstaclesChance=0.75f,
            int obstaclesGap=3,
            float powerUpsChance=0.20f,
            int powerUpsGap=4
        ) {
            var row = new Row();
            for(int i = 0; i < columnCount; i++) row.Add(Spawn.Ground);

            var index = rows.Count;

            examItemsGap = examItemsGap <= 0 ?
                Mathf.FloorToInt(rowCount / examItemsCount) : examItemsGap;
            obstaclesGap = Mathf.Max(obstaclesGap, 1);

            var oindices = Enumerable.Range(0, columnCount).ToArray();

            var groundWithExamItem = Spawn.Ground | Spawn.ExamItem;
            var groundWithObstacle = Spawn.Ground | Spawn.Obstacle;
            var groundWithPowerUp = Spawn.Ground | Spawn.PowerUp;

            var oindices_ = oindices.ToList();
            var oindex = 0;

            if(index % examItemsGap == 0 &&
                rows.Sum((r) =>
                r.Count((i) => i == groundWithExamItem)) != examItemsCount
            ) {
                oindex = oindices_.PickRandomly();
                row[oindex] = groundWithExamItem;
                oindices_.RemoveAt(oindices_.IndexOf(oindex));

            }
            
            while(oindices_.Count != 0) {
                oindex = oindices_.PickRandomly();
                if(oindices_.Count != columnCount) {
                    var chance = UnityEngine.Random.value;
                    if(
                        index % powerUpsGap == 0 &&
                        chance <= powerUpsChance
                    ) {
                        row[oindex] = groundWithPowerUp;

                    } else if(index % obstaclesGap == 0 &&
                        chance <= obstaclesChance
                    ) {
                        row[oindex] = groundWithObstacle;

                    }

                }

                oindices_.RemoveAt(oindices_.IndexOf(oindex));

            }

            return row;

        }

    }

}