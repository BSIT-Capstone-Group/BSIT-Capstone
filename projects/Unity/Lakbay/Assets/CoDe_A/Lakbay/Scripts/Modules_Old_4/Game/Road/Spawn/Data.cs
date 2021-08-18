/*
 * Date Created: Monday, July 19, 2021 7:17 PM
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

namespace CoDe_A.Lakbay.ModulesOld4.Game.Road.Spawn {
    using Event = Utilities.Event;
    using Input = Utilities.Input;
    using BaseIData = Core.Interactable.IData<Controller>;
    using BaseData = Core.Interactable.Data<Controller>;
    using Base = Core.Interactable;


    [Serializable]
    public struct AxisInfo {
        [SerializeField]
        int _max;
        public int max { get => _max; set => _max = value; }
        [SerializeField]
        List<int> _range;
        public List<int> range { get => _range; set => _range = value; }

        
        public AxisInfo(int max=-1, List<int> range=null) {
            this._max = max;
            this._range = range;

        }

    }

    public interface IData : Base.IData {
        float chance { get; set; }
        AxisInfo column { get; set; }
        AxisInfo row { get; set; }

    }

    public class Data : IData {
        public new const string HeaderName = "Spawn.Data";

        // [Space(HeaderSpace), Header(HeaderName)]
        [SerializeField]
        protected float _chance;
        public virtual float chance {
            get => _chance;
            set {
                // var r = Helper.SetInvoke(controller, ref _chance, value);
                // if(r.Item1) controller?.OnRowsChange(r.Item2.Item1, r.Item2.Item2);

            }

        }
        [SerializeField]
        protected AxisInfo _column;
        public virtual AxisInfo column {
            get => _column;
            set {
                // var r = Helper.SetInvoke(controller, ref _column, value);
                // if(r.Item1) controller?.OnRowsChange(r.Item2.Item1, r.Item2.Item2);

            }

        }
        [SerializeField]
        protected AxisInfo _row;
        public virtual AxisInfo row {
            get => _row;
            set {
                // var r = Helper.SetInvoke(controller, ref _row, value);
                // if(r.Item1) controller?.OnRowsChange(r.Item2.Item1, r.Item2.Item2);

            }

        }

        public bool playing { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool handlingInputs { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int maxCollisionCount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int collisionCount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string label { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Set(TextAsset textAsset)
        {
            throw new NotImplementedException();
        }

        public void SetController(Core.Controller controller)
        {
            throw new NotImplementedException();
        }
    }

    public interface IData<T> : Base.IData<T>, IData
        where T : IController {
        
    }

    [Serializable]
    public class Data<T> : Base.Data<T>, IData<T>
        where T : class, IController {


        public Data() => Create(instance: this);

        public float chance { get; set; }
        public AxisInfo column { get; set; }
        public AxisInfo row { get; set; }

        public static Data Create(
            float chance=1.0f,
            AxisInfo column=default,
            AxisInfo row=default,
            Base.IData<T> data=null,
            IData<T> instance=null
        ) {
            instance ??= new Data<T>();
            Base.Data<T>.Create(data, instance);
            instance.chance = chance;
            instance.column = column.Equals(default(AxisInfo)) ? new AxisInfo(-1) : column;
            instance.row = row.Equals(default(AxisInfo)) ? new AxisInfo(-1) : row;

            return instance as Data;

        }

        public static Data Create(
            IData<T> data,
            IData<T> instance=null
        ) {
            return Create(
                data.chance,
                data.column,
                data.row,
                data,
                instance
            );

        }

        public static Data Create(TextAsset textAsset, IData<T> instance=null) {
            return Create(textAsset.Parse<Data<T>>(), instance);

        }

        public override void Set(TextAsset textAsset) => Create(textAsset, this);

    }

}