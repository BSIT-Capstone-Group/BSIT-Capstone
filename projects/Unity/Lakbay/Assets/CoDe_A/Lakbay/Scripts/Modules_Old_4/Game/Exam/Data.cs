/*
 * Date Created: Tuesday, July 13, 2021 10:11 PM
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

namespace CoDe_A.Lakbay.ModulesOld4.Game.Exam {
    using Event = Utilities.Event;
    using Input = Utilities.Input;
    using BaseIData = Core.Interactable.IData<Controller>;
    using BaseData = Core.Interactable.Data<Controller>;


    public interface IData : BaseIData {
        List<int> missedItemsIndices { get; }
        List<int> correctItemsIndices { get; }
        int correctItemsCount { get; }
        int totalItemsCount { get; }

        List<Item.Data> items { get; set; }

        Event.OnValueChange<Controller, List<Item.Data>> onItemsChange { get; }
        
    }

    [Serializable]
    public class Data : BaseData, IData {
        public new const string HeaderName = "Exam.Data";

        [YamlIgnore]
        public virtual List<int> missedItemsIndices {
            get {
                var l = new List<int>();
                if(items != null) {
                    items.ForEach((i) => { if(i.missed) l.Add(items.IndexOf(i)); });    

                }

                return l;

            }

        }
        [YamlIgnore]
        public virtual List<int> correctItemsIndices {
            get {
                var l = new List<int>();
                if(items != null) {
                    items.ForEach((i) => { if(i.correct) l.Add(items.IndexOf(i)); });    

                }

                return l;

            }

        }
        [YamlIgnore]
        public virtual int correctItemsCount => correctItemsIndices.Count;
        [YamlIgnore]
        public virtual int totalItemsCount => items != null ? items.Count : 0;

        [Space(HeaderSpace), Header(HeaderName)]
        [SerializeField]
        protected List<Item.Data> _items;
        public virtual List<Item.Data> items {
            get => _items;
            set  {
                var r = Helper.SetInvoke(controller, ref _items, value, onItemsChange);
                if(r.Item1) controller?.OnItemsChange(r.Item2[0], r.Item2[1]);
            
            }

        }

        [SerializeField]
        protected Event.OnValueChange<Controller, List<Item.Data>> _onItemsChange = new Event.OnValueChange<Controller, List<Item.Data>>();
        [YamlIgnore]
        public virtual Event.OnValueChange<Controller, List<Item.Data>> onItemsChange => _onItemsChange;


        public Data() => Create(instance: this);

        public static Data Create(
            List<Item.Data> items=null,
            BaseIData data=null,
            IData instance=null
        ) {
            instance ??= new Data();
            BaseData.Create(data, instance);
            instance.items = items ?? new List<Item.Data>();

            return instance as Data;

        }

        public static Data Create(
            IData data,
            IData instance=null
        ) {
            data ??= new Data();
            return Create(
                data.items,
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