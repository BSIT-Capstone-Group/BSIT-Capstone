/*
 * Date Created: Tuesday, July 13, 2021 8:58 PM
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

namespace CoDe_A.Lakbay.ModulesOld4.Core.Content.Entry {
    using Event = Utilities.Event;
    using Input = Utilities.Input;
    using BaseIData = Core.Interactable.IData<Controller>;
    using BaseData = Core.Interactable.Data<Controller>;


    public enum Type { Text, Image }

    public interface IData : BaseIData {
        Type type { get; }

        string text { get; set; }

        Event.OnStringChange<Controller> onTextChange { get; }
        
    }

    [Serializable]
    public class Data : BaseData, IData {
        public new const string HeaderName = "Entry.Data";

        [YamlIgnore]
        public virtual Type type {
            get {
                return Type.Text;

            }

        }

        [Space(HeaderSpace), Header(HeaderName)]
        [SerializeField]
        protected string _text;
        public virtual string text {
            get => _text;
            set  {
                var r = Helper.SetInvoke(controller, ref _text, value, onTextChange);
                if(r.Item1) controller?.OnTextChange(r.Item2.Item1, r.Item2.Item2);
            
            }

        }

        [SerializeField]
        protected Event.OnStringChange<Controller> _onTextChange = new Event.OnStringChange<Controller>();
        [YamlIgnore]
        public virtual Event.OnStringChange<Controller> onTextChange => _onTextChange;


        public Data() => Create(instance: this);

        public static Data Create(
            string text=" ",
            BaseIData data=null,
            IData instance=null
        ) {
            instance ??= new Data();
            BaseData.Create(data, instance);
            instance.text = text;

            return instance as Data;

        }

        public static Data Create(
            IData data,
            IData instance=null
        ) {
            data ??= new Data();
            return Create(
                data.text,
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