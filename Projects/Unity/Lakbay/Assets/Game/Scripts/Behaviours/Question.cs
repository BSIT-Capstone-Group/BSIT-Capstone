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
    [Serializable]
    public struct Question {
        private int _answeredChoiceIndex;
        private List<Entry> _content;
        public List<Entry> content { get => _content; set => _content = value; }
        private List<Choice> _choices;
        public List<Choice> choices { get => _choices; set => _choices = value; }
        [YamlIgnore]
        public Choice answeredChoice {
            get {
                if(choices.Has(_answeredChoiceIndex)) return choices[_answeredChoiceIndex];
                return default;

            }

        }
        [YamlIgnore]
        public bool answered => !answeredChoice.Equals(default(Choice));

        public Question(List<Entry> content=null, List<Choice> choices=null) {
            _answeredChoiceIndex = -1;
            _content = content ?? new List<Entry>();
            _choices = choices ?? new List<Choice>();

        }

        public bool Check(Choice choice) {
            return choices.Contains(choice) && choice.correct;

        }

        public bool Check(int choiceIndex) {
            return choices.Has(choiceIndex) && Check(choices[choiceIndex]);

        }

        public void Answer(Choice choice) {
            if(choices.Contains(choice)) _answeredChoiceIndex = choices.IndexOf(choice);
            else _answeredChoiceIndex = -1;

        }

        public void Answer(int choiceIndex) {
            if(choices.Has(choiceIndex)) Answer(choices[choiceIndex]);

        }

    }

    [Serializable]
    public struct Choice {
        private bool _correct;
        public bool correct { get => _correct; set => _correct = value; }
        private List<Entry> _content;
        public List<Entry> content { get => _content; set => _content = value; }

        public Choice(bool correct=false, List<Entry> content=null) {
            _correct = correct;
            _content = content ?? new List<Entry>();

        }

    }

}