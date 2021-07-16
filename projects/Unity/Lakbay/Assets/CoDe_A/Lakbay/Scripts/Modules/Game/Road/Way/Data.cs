/*
 * Date Created: Tuesday, July 13, 2021 6:05 PM
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

namespace CoDe_A.Lakbay.Modules.Game.Road.Way {
    using Event = Utilities.Event;
    using Input = Utilities.Input;
    using BaseIData = Core.Interactable.IData<Controller>;
    using BaseData = Core.Interactable.Data<Controller>;


    public interface IData : BaseIData {
        Vector3 groundSize { get; }
        List<int> spawnedSpawnsIndices { get; }

        int length { get; set; }
        GameObject ground { get; set; }
        List<GameObject> spawns { get; set; }

        Event.OnIntChange<Controller> onLengthChange { get; }
        Event.OnValueChange<Controller, List<GameObject>> onSpawnsChange { get; }
        
    }

    [Serializable]
    public class Data : BaseData, IData {
        [YamlIgnore]
        public virtual Vector3 groundSize {
            get {
                if(ground && controller && controller.transform.childCount != 0) {
                    var m = controller.transform.GetChild(0);
                    return m.gameObject.GetSize();

                }
                
                return Vector3.zero;
            }

        }
        // [SerializeField]
        protected List<int> _spawnedSpawnsIndices = new List<int>();
        [YamlIgnore]
        public virtual List<int> spawnedSpawnsIndices => _spawnedSpawnsIndices;

        [SerializeField]
        protected int _length;
        public virtual int length {
            get => _length;
            set  {
                var r = Helper.SetInvoke(controller, ref _length, value, onLengthChange);
                if(r.Item1) controller?.OnLengthChange(r.Item2[0], r.Item2[1]);
                
            }

        }
        [SerializeField]
        protected GameObject _ground;
        [YamlIgnore]
        public virtual GameObject ground {
            get => _ground;
            set => Helper.SetInvoke(controller, ref _ground, value);

        }
        [SerializeField]
        protected List<GameObject> _spawns = new List<GameObject>();
        [YamlIgnore]
        public virtual List<GameObject> spawns {
            get => _spawns;
            set  {
                var r = Helper.SetInvoke(controller, ref _spawns, value, onSpawnsChange);
                if(r.Item1) controller?.OnSpawnsChange(r.Item2[0], r.Item2[1]);
                
            }

        }

        [SerializeField]
        protected Event.OnIntChange<Controller> _onLengthChange = new Event.OnIntChange<Controller>();
        [YamlIgnore]
        public virtual Event.OnIntChange<Controller> onLengthChange => _onLengthChange;
        [SerializeField]
        protected Event.OnValueChange<Controller, List<GameObject>> _onSpawnsChange = new Event.OnValueChange<Controller, List<GameObject>>();
        [YamlIgnore]
        public virtual Event.OnValueChange<Controller, List<GameObject>> onSpawnsChange => _onSpawnsChange;


        public Data() => Create(instance: this);

        public static Data Create(
            int length=10,
            BaseIData data=null,
            IData instance=null
        ) {
            instance ??= new Data();
            BaseData.Create(data, instance);
            instance.length = length;

            return instance as Data;

        }

        public static Data Create(
            IData data,
            IData instance=null
        ) {
            data ??= new Data();
            return Create(
                data.length,
                data,
                instance
            );

        }

        public static Data Create(TextAsset textAsset, IData instance=null) {
            return Create(textAsset.Parse<Data>(), instance);

        }

        public override void Set(TextAsset textAsset) => Create(textAsset, this);

    }

}