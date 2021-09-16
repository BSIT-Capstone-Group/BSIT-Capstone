/*
 * Date Created: Thursday, September 9, 2021 7:08 AM
 * Author: NI.L.A
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

using TMPro;
using YamlDotNet.Serialization;

using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Behaviours {
    using Content = List<Entry>;

    [Serializable]
    public struct Question {
        private List<int> _answeredChoiceIndices;
        [YamlIgnore]
        public List<int> answeredChoiceIndices {
            get {
                if(_answeredChoiceIndices == null)
                    _answeredChoiceIndices = new List<int>();

                return _answeredChoiceIndices;

            }

        }
        // If true, the question evaluates as correct if all of the choices marked
        // as correct are answered.
        [SerializeField]
        private bool _strict;
        public bool strict { get => _strict; set => _strict = value; }
        // If true, the question evaluates as correct if the order of answered
        // choices match the order set in the choice.
        [SerializeField]
        private bool _ordered;
        public bool ordered { get => _ordered; set => _ordered = value; }
        [SerializeField]
        private Content _content;
        public Content content { get => _content; set => _content = value; }
        [SerializeField]
        private List<Choice> _choices;
        public List<Choice> choices { get => _choices; set => _choices = value; }
        [YamlIgnore]
        public Choice[] correctChoices => choices.Where(
            (c) => c.correct).OrderBy((c) => c.order).ToArray();
        [YamlIgnore]
        public Choice[] answeredChoices {
            get {
                var choices = new List<Choice>();
                foreach(var index in answeredChoiceIndices) {
                    if(this.choices.Has(index)) choices.Add(this.choices[index]);

                }
                
                return choices.ToArray();

            }

        }
        [YamlIgnore]
        public bool answered => answeredChoices.Length > 0;
        [YamlIgnore]
        public bool correct => Check(answeredChoices);

        public Question(
            Content content =null, List<Choice> choices=null,
            bool strict=false, bool ordered=false
        ) {
            _answeredChoiceIndices = new List<int>();
            _content = content ?? new Content();
            _choices = choices ?? new List<Choice>();
            _strict = strict;
            _ordered = ordered;

        }

        public bool Check(params Choice[] choices) {
            var conditions = new List<bool>();
            var cchoices = correctChoices;

            if(strict) {
                conditions.Add(cchoices.Select((c) => choices.Contains(c)).All());

            }

            if(ordered) {
                conditions.Add(cchoices.SequenceEqual(choices));

            }

            return !choices.IsEmpty() && conditions.All();

        }

        public bool Check(params int[] choiceIndices) {
            var choices = new List<Choice>();
            foreach(var index in choiceIndices) {
                if(this.choices.Has(index)) choices.Add(this.choices[index]);

            }

            return Check(choices.ToArray());

        }

        public void Answer(params Choice[] choices) {
            answeredChoiceIndices.Clear();
            AddAnswer(choices);

        }

        public void Answer(params int[] choiceIndices) {
            var choices = new List<Choice>();
            foreach(var index in choiceIndices)
                if(choices.Has(index)) choices.Add(choices[index]);

            Answer(choices.ToArray());

        }

        public void Unanswer() => Answer(new int[] {});

        public void AddAnswer(params Choice[] choices) {
            var indices = new List<int>();
            foreach(var choice in choices) {
                if(this.choices.Contains(choice)) {
                    indices.Add(this.choices.IndexOf(choice));

                }

            }

            answeredChoiceIndices.AddRange(indices);

        }

        public void AddAnswer(params int[] choiceIndices) {
            var choices = new List<Choice>();
            foreach(var index in choiceIndices)
                if(choices.Has(index)) choices.Add(choices[index]);

            AddAnswer(choices.ToArray());

        }

        public void RemoveAnswer(params Choice[] choices) {
            foreach(var choice in choices) {
                if(answeredChoices.Contains(choice)) {
                    int index = this.choices.IndexOf(choice);
                    while(answeredChoiceIndices.Contains(index)) {
                        answeredChoiceIndices.Remove(index);

                    }

                }

            }

        }

        public void RemoveAnswer(params int[] choiceIndices) {
            var choices = this.choices;
            RemoveAnswer(
                choiceIndices.Where((i) => choices.Has(i))
                .Select((i) => choices[i]).ToArray()
            );

        }
    
    }

    [Serializable]
    public struct Choice {
        [SerializeField]
        private bool _correct;
        public bool correct { get => _correct; set => _correct = value; }
        [SerializeField]
        private int _order;
        public int order { get => _order; set => _order = value; }
        [SerializeField]
        private Content _content;
        public Content content { get => _content; set => _content = value; }

        public Choice(bool correct=false, Content content =null, int order=0) {
            _correct = correct;
            _content = content ?? new Content();
            _order = order;

        }

    }

}