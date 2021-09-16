/*
 * Date Created: Thursday, September 16, 2021 9:58 PM
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
    [RequireComponent(typeof(RectTransform))]
    public class ContentPresenter : Presenter {
        protected string _recentContent = "";
        public List<SectionPresenter> sectionPresenters = new List<SectionPresenter>();
        public List<string> content = new List<string>();

        public override void Update() {
            base.Update();
            string newContent = content.Join("\n");
            if(_recentContent != newContent) {
                Display(content);
                _recentContent = newContent;

            }

        }

        public virtual void Display(IEnumerable<string> content) {
            Clear();
            var sections = Parse(content);
            foreach(var section in sections) {
                var section_ = this.sectionPresenters.Find((s) => s.entryType == section.Key);
                Display(Instantiate(section_), (s) => s.section = section.Value);

            }

        }

        public static List<KeyValuePair<Entry.Type, List<string>>> Parse(IEnumerable<string> content) {
            var sections = new List<KeyValuePair<Entry.Type, List<string>>>();
            foreach(var entry in content) {
                Entry.Parse(entry, out var type, out var value);
                if(sections.Count == 0 || (sections.Count > 0 && sections.Last().Key != type)) {
                    sections.Add(new KeyValuePair<Entry.Type, List<string>>(
                        type, new List<string>()
                    ));

                }

                var section = sections.Last();
                section.Value.Add(value);

            }

            return sections;

        }

    }

}