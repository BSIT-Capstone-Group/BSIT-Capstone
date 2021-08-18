/*
 * Date Created: Tuesday, July 13, 2021 9:28 PM
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

namespace CoDe_A.Lakbay.ModulesOld4.Core.Content {
    using Event = Utilities.Event;
    using Input = Utilities.Input;
    using BaseIData = Core.Interactable.IData<Controller>;
    using BaseData = Core.Interactable.Data<Controller>;


    public interface IData : BaseIData {
        List<Entry.Data> entries { get; set; }

        Event.OnValueChange<Controller, List<Entry.Data>> onEntriesChange { get; }
        
    }

    [Serializable]
    public class Data : BaseData, IData {
        public new const string HeaderName = "Content.Data";

        [Space(HeaderSpace), Header(HeaderName)]
        [SerializeField]
        protected List<Entry.Data> _entries;
        public virtual List<Entry.Data> entries {
            get => _entries;
            set  {
                var r = Helper.SetInvoke(controller, ref _entries, value, onEntriesChange);
                if(r.Item1) controller?.OnEntriesChange(r.Item2.Item1, r.Item2.Item2);
            
            }

        }

        [SerializeField]
        protected Event.OnValueChange<Controller, List<Entry.Data>> _onEntriesChange = new Event.OnValueChange<Controller, List<Entry.Data>>();
        [YamlIgnore]
        public virtual Event.OnValueChange<Controller, List<Entry.Data>> onEntriesChange => _onEntriesChange;


        public Data() => Create(instance: this);

        public static Data Create(
            List<Entry.Data> entries=null,
            BaseIData data=null,
            IData instance=null
        ) {
            instance ??= new Data();
            BaseData.Create(data, instance);
            instance.entries = entries ?? new List<Entry.Data>();

            return instance as Data;

        }

        public static Data Create(
            IData data,
            IData instance=null
        ) {
            data ??= new Data();
            return Create(
                data.entries,
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