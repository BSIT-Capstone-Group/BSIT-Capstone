/*
 * Date Created: Thursday, September 16, 2021 4:07 AM
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
    public class EntryPresenter : Presenter {
        protected string _recentEntry = "";
        public TextPresentable textPresentable;
        public ImagePresentable imagePresentable;
        public Entry.Type entryType;
        public Entry entry;

        public override void Update() {
            base.Update();
            string newEntry = entry.ToString();
            if(_recentEntry != newEntry) {
                Display(entry);
                _recentEntry = newEntry;

            }

        }

        [Flags]
        public enum Sample1 { A, B, C}
        public enum Sample2 { A, B, C}

        public virtual void Display(Entry entry) {
            Clear();
            var repo = Game.repository;
            
            switch(entry.type) {
                case Entry.Type.Text:
                    foreach(var item in entry.items) Display(
                        Instantiate(textPresentable),
                        (c) => c.component.SetText(item)
                    );

                    break;

                case Entry.Type.Image:
                    foreach(var item in entry.items) Display(
                        Instantiate(imagePresentable),
                        (c) => c.component.sprite = repo.GetAsset<Sprite>(item)
                    );

                    break;

                case Entry.Type.Document:
                    foreach(var item in entry.items) Display(
                        Instantiate(textPresentable),
                        (c) => c.component.SetText(repo.GetAsset<TextAsset>(item).text)
                    );

                    break;

                default: break;

            }

        }

    }

}