/*
 * Date Created: Tuesday, July 13, 2021 9:36 PM
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

namespace CoDe_A.Lakbay.ModulesOld4.Game.Exam.Item {
    using Event = Utilities.Event;
    using Input = Utilities.Input;
    using BaseIData = Core.Interactable.IData<Controller>;
    using BaseData = Core.Interactable.Data<Controller>;


    public interface IData : BaseIData {
        float time { get; }
        float minTime { get; set; }
        float maxTime { get; set; }
        bool missed { get; set; }
        bool correct { get; set; }
        Core.Content.Data question { get; set; }
        List<Choice.Data> choices { get; set; }

        Event.OnBoolChange<Controller> onMissedChange { get; }
        Event.OnBoolChange<Controller> onCorrectChange { get; }
        Event.OnValueChange<Controller, Core.Content.Data> onQuestionChange { get; }
        Event.OnValueChange<Controller, List<Choice.Data>> onChoicesChange { get; }
        
    }

    [Serializable]
    public class Data : BaseData, IData {
        public new const string HeaderName = "Item.Data";

        [YamlIgnore]
        public virtual float time {
            get {
                int chs = 0;
                if(question != null) question.entries.ForEach((e) => chs += e.text.Trim().Length);
                if(choices != null) choices.ForEach((c) => chs += c.entry.text.Trim().Length);

                return Mathf.Clamp(chs / Helper.ReadingCharacterPerSecond, minTime, maxTime);

            }

        }

        [Space(HeaderSpace), Header(HeaderName)]
        [SerializeField]
        protected float _minTime;
        public virtual float minTime {
            get => _minTime;
            set  {
                var r = Helper.SetInvoke(controller, ref _minTime, value);
                // if(r.Item1) controller?.OnMissedChange(r.Item2[0], r.Item2[1]);
            
            }

        }
        [SerializeField]
        protected float _maxTime;
        public virtual float maxTime {
            get => _maxTime;
            set  {
                var r = Helper.SetInvoke(controller, ref _maxTime, value);
                // if(r.Item1) controller?.OnMissedChange(r.Item2[0], r.Item2[1]);
            
            }

        }
        [SerializeField]
        protected bool _missed;
        public virtual bool missed {
            get => _missed;
            set  {
                var r = Helper.SetInvoke(controller, ref _missed, value, onMissedChange);
                if(r.Item1) controller?.OnMissedChange(r.Item2[0], r.Item2[1]);
            
            }

        }
        [SerializeField]
        protected bool _correct;
        public virtual bool correct {
            get => _correct;
            set  {
                var r = Helper.SetInvoke(controller, ref _correct, value, onCorrectChange);
                if(r.Item1) controller?.OnCorrectChange(r.Item2[0], r.Item2[1]);
            
            }

        }
        [SerializeField]
        protected Core.Content.Data _question;
        public virtual Core.Content.Data question {
            get => _question;
            set  {
                var r = Helper.SetInvoke(controller, ref _question, value, onQuestionChange);
                if(r.Item1) controller?.OnQuestionChange(r.Item2[0], r.Item2[1]);
            
            }

        }
        [SerializeField]
        protected List<Choice.Data> _choices;
        public virtual List<Choice.Data> choices {
            get => _choices;
            set  {
                var r = Helper.SetInvoke(controller, ref _choices, value, onChoicesChange);
                if(r.Item1) controller?.OnChoicesChange(r.Item2[0], r.Item2[1]);
            
            }

        }

        [SerializeField]
        protected Event.OnBoolChange<Controller> _onMissedChange = new Event.OnBoolChange<Controller>();
        [YamlIgnore]
        public virtual Event.OnBoolChange<Controller> onMissedChange => _onMissedChange;
        [SerializeField]
        protected Event.OnBoolChange<Controller> _onCorrectChange = new Event.OnBoolChange<Controller>();
        [YamlIgnore]
        public virtual Event.OnBoolChange<Controller> onCorrectChange => _onCorrectChange;
        [SerializeField]
        protected Event.OnValueChange<Controller, Core.Content.Data> _onQuestionChange = new Event.OnValueChange<Controller, Core.Content.Data>();
        [YamlIgnore]
        public virtual Event.OnValueChange<Controller, Core.Content.Data> onQuestionChange => _onQuestionChange;
        [SerializeField]
        protected Event.OnValueChange<Controller, List<Choice.Data>> _onChoicesChange = new Event.OnValueChange<Controller, List<Choice.Data>>();
        [YamlIgnore]
        public virtual Event.OnValueChange<Controller, List<Choice.Data>> onChoicesChange => _onChoicesChange;


        public Data() => Create(instance: this);

        public static Data Create(
            float minTime=7.0f,
            float maxTime=20.0f,
            bool missed=true,
            bool correct=false,
            Core.Content.Data question=null,
            List<Choice.Data> choices=null,
            BaseIData data=null,
            IData instance=null
        ) {
            instance ??= new Data();
            BaseData.Create(data, instance);
            instance.minTime = minTime;
            instance.maxTime = maxTime;
            instance.missed = missed;
            instance.correct = correct;
            instance.question = question ?? new Core.Content.Data();
            instance.choices = choices ?? new List<Choice.Data>();

            return instance as Data;

        }

        public static Data Create(
            IData data,
            IData instance=null
        ) {
            data ??= new Data();
            return Create(
                data.minTime,
                data.maxTime,
                data.missed,
                data.correct,
                data.question,
                data.choices,
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