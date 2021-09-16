/*
 * Date Created: Thursday, September 16, 2021 9:20 PM
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

namespace Ph.CoDe_A.Lakbay.Core.UI {
    [RequireComponent(typeof(RectTransform))]
    public class SectionPresenter : Presenter {
        protected string _recentSection = "";
        public Entry.Type entryType;
        public Entry entry;
        public List<string> section;

        public override void Update() {
            base.Update();
            string newSection = section.Join("\n");
            if(_recentSection != newSection) {
                Display(section);
                _recentSection = newSection;

            }

        }

        public virtual void Display(IEnumerable<string> section) {
            Clear();
            var repo = Game.repository;

            foreach(var value in section) {
                switch(entryType) {
                    case Entry.Type.Text:
                        Display(
                            Instantiate(entry as TextEntry),
                            (c) => c.component.SetText(value)
                        );

                        break;

                    case Entry.Type.Image:
                        Display(
                            Instantiate(entry as ImageEntry),
                            (c) => c.component.sprite = repo.GetAsset<Sprite>(value)
                        );

                        break;

                    case Entry.Type.Document:
                        Display(
                            Instantiate(entry as TextEntry),
                            (c) => c.component.SetText(
                                repo.GetAsset<TextAsset>(value).text
                            )
                        );

                        break;

                    default: break;

                }

            }

        }

    }

}