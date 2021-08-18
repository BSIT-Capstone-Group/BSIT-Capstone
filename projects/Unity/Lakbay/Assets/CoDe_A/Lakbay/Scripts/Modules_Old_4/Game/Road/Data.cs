/*
 * Date Created: Tuesday, July 13, 2021 8:40 PM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CoDe_A.Lakbay.ModulesOld4.Game.Road {
    using Event = Utilities.Event;
    using Input = Utilities.Input;
    using BaseIData = Core.Interactable.IData<Controller>;
    using BaseData = Core.Interactable.Data<Controller>;
    using Columnn = List<string[]>;
    using Row = List<List<string[]>>;


    public interface IData : BaseIData {
        List<Row> rows { get; set; }

        Event.OnValueChange<Controller, List<Spawn.Data>> onSpawnsChange { get; }
        Event.OnValueChange<Controller, List<Row>> onRowsChange { get; }
        
    }

    [Serializable]
    public class Data : BaseData, IData {
        public new const string HeaderName = "Road.Data";


        [Space(HeaderSpace), Header(HeaderName)]
        [SerializeField]
        protected List<Row> _rows;
        // [YamlIgnore]
        public virtual List<Row> rows {
            get => _rows;
            set {
                var r = Helper.SetInvoke(controller, ref _rows, value, onRowsChange);
                // if(r.Item1) controller?.OnRowsChange(r.Item2.Item1, r.Item2.Item2);

            }

        }

        // [SerializeField]
        // protected Event.OnValueChange<Controller, List<Spawnn>> _onSpawnsChange = new Event.OnValueChange<Controller, List<Spawnn>>();
        // [YamlIgnore]
        // public virtual Event.OnValueChange<Controller, List<Spawnn>> onSpawnsChange => _onSpawnsChange;
        [SerializeField]
        protected Event.OnValueChange<Controller, List<Row>> _onRowsChange = new Event.OnValueChange<Controller, List<Row>>();
        [YamlIgnore]
        public virtual Event.OnValueChange<Controller, List<Row>> onRowsChange => _onRowsChange;

        Event.OnValueChange<Controller, List<Spawn.Data>> IData.onSpawnsChange => throw new NotImplementedException();

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
            Vector2Int size,
            List<Spawn.Controller> spawnControllers=null
        ) {
            var rows = new List<Row>(
                from i in Enumerable.Range(0, size.y) select
                new Row(
                    from ii in Enumerable.Range(0, size.x) select
                    new List<string[]>()
                )
            );
            spawnControllers ??= new List<Spawn.Controller>();

            foreach(var row in rows.Enumerate()) {
                foreach(var col in row.Value.Enumerate()) {
                    float chance = UnityEngine.Random.value;

                    foreach(var sc in spawnControllers) {
                        col.Value.Add(sc.OnPlot(
                            rows, new Vector2Int(col.Key, row.Key), chance
                        ) ?? new string[] {});

                    }

                }

            }

            return rows;

        }

        public static string GenerateRowsAsYaml(List<Row> rows) {
            var str = "";
            
            foreach(var row in rows) {
                var srow = "- [{0}]\n";

                var scols = new List<string>();
                foreach(var col in row) {
                    var scol = "[{0}]";
                    var skeys = from keys in col select (
                        from k in keys select $"\"{k}\""
                    ).ToString(true);
                    scol = string.Format(scol, skeys.ToString(true));
                    scols.Add(scol);

                }

                str += string.Format(srow, string.Join(", ", scols));

            }

            return str;

        }

        public static string GenerateRowsAsYaml(
            Vector2Int size,
            List<Spawn.Controller> spawnControllers=null
        ) {
            return GenerateRowsAsYaml(GenerateRows(size, spawnControllers));

        }

    }

}