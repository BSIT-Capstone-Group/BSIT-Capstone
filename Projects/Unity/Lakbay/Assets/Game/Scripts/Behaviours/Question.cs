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

using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Behaviours {
    public struct Question {
        private List<Entry> _content;
        public List<Entry> content { get => _content; set => _content = value; }
        private List<Choice> _choices;
        public List<Choice> choices { get => _choices; set => _choices = value; }

        public Question(List<Entry> content=null, List<Choice> choices=null) {
            _content = content ?? new List<Entry>();
            _choices = choices ?? new List<Choice>();

        }

        public bool Check(Choice choice) {
            return choices.Contains(choice) && choice.correct;

        }

        public bool Check(int index) {
            return choices.Have(index) && Check(choices[index]);

        }

    }

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