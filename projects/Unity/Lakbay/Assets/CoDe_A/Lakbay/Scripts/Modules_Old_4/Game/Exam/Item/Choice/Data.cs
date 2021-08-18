/*
 * Date Created: Tuesday, July 13, 2021 9:41 PM
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

namespace CoDe_A.Lakbay.ModulesOld4.Game.Exam.Item.Choice {
    using Event = Utilities.Event;
    using Input = Utilities.Input;
    using BaseIData = Core.Interactable.IData<Controller>;
    using BaseData = Core.Interactable.Data<Controller>;


    public interface IData : BaseIData {
        bool correct { get; set; }
        Core.Content.Entry.Data entry { get; set; }

        Event.OnBoolChange<Controller> onCorrectChange { get; }
        Event.OnValueChange<Controller, Core.Content.Entry.Data> onEntryChange { get; }
        
    }

    [Serializable]
    public class Data : BaseData, IData {
        public new const string HeaderName = "Choice.Data";

        [Space(HeaderSpace), Header(HeaderName)]
        [SerializeField]
        protected bool _correct;
        public virtual bool correct {
            get => _correct;
            set  {
                var r = Helper.SetInvoke(controller, ref _correct, value, onCorrectChange);
                if(r.Item1) controller?.OnCorrectChange(r.Item2.Item1, r.Item2.Item2);
            
            }

        }
        [SerializeField]
        protected Core.Content.Entry.Data _entry;
        public virtual Core.Content.Entry.Data entry {
            get => _entry;
            set  {
                var r = Helper.SetInvoke(controller, ref _entry, value, onEntryChange);
                if(r.Item1) controller?.OnEntryChange(r.Item2.Item1, r.Item2.Item2);
            
            }

        }

        [SerializeField]
        protected Event.OnBoolChange<Controller> _onCorrectChange = new Event.OnBoolChange<Controller>();
        [YamlIgnore]
        public virtual Event.OnBoolChange<Controller> onCorrectChange => _onCorrectChange;
        [SerializeField]
        protected Event.OnValueChange<Controller, Core.Content.Entry.Data> _onEntryChange = new Event.OnValueChange<Controller, Core.Content.Entry.Data>();
        [YamlIgnore]
        public virtual Event.OnValueChange<Controller, Core.Content.Entry.Data> onEntryChange => _onEntryChange;


        public Data() => Create(instance: this);

        public static Data Create(
            bool correct=false,
            Core.Content.Entry.Data entry=null,
            BaseIData data=null,
            IData instance=null
        ) {
            instance ??= new Data();
            BaseData.Create(data, instance);
            instance.correct = correct;
            instance.entry = entry;

            return instance as Data;

        }

        public static Data Create(
            IData data,
            IData instance=null
        ) {
            data ??= new Data();
            return Create(
                data.correct,
                data.entry,
                data,
                instance
            );

        }

        public static Data Create(TextAsset textAsset, IData instance=null) {
            return Create(textAsset.Parse<Data>(), instance);

        }

    }

}