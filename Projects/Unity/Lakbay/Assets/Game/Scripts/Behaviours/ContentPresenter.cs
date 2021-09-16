/*
 * Date Created: Thursday, September 16, 2021 3:40 AM
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

    [RequireComponent(typeof(RectTransform))]
    public class ContentPresenter : Presenter {
        protected string _recentContent = "";
        public List<EntryPresenter> entryPresenters; 
        public Content content = new Content();

        protected string _ToString(IEnumerable<Entry> content) {
            return content.Join("\n");

        }

        public override void Update() {
            base.Update();
            string newContent = _ToString(content);
            if(_recentContent != newContent) {
                Display(content);
                _recentContent = newContent;

            }

        }

        public virtual void Display(IEnumerable<Entry> content) {
            Clear();
            foreach(var entry in content) {
                var entryPresenter = entryPresenters.Find((e) => e.entryType.HasFlag(entry.type));
                if(!entryPresenter) entryPresenter = entryPresenters.First();
                Display(Instantiate(entryPresenter), (e) => e.entry = entry);

            }

        }

    }

}