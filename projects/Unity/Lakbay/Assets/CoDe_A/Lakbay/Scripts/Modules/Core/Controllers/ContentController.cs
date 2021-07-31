/*
 * Date Created: Tuesday, July 20, 2021 9:10 PM
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

namespace CoDe_A.Lakbay.Modules.Core.Controllers {
    using Event = Utilities.Event;
    using Input = Utilities.Input;


    public interface IContentController {
        EntryController textEntryController { get; set; }
        EntryController imageEntryController { get; set; }

        void Show(List<Entry> content);
        void Hide();
        
    }

    public class ContentController : Controller, IContentController {
        [SerializeField]
        protected EntryController _textEntryController;
        public virtual EntryController textEntryController { get => _textEntryController; set => _textEntryController = value; }
        [SerializeField]
        protected EntryController _imageEntryController;
        public virtual EntryController imageEntryController { get => _imageEntryController; set => _imageEntryController = value; }


        public virtual void Show(List<Entry> content) {
            gameObject.DestroyChildren();

            foreach(var entry in content) {
                EntryController entryController = null;
                if(entry.type == Entry.Type.Text) {
                    entryController = Instantiate(textEntryController, transform);

                } else if(entry.type == Entry.Type.Image) {
                    entryController = Instantiate(imageEntryController, transform);

                }

                entryController.Show(entry);

            }

        }
        
        public virtual void Hide() {
            foreach(var c in gameObject.GetChildren()) {
                c.SetActive(false);

            }

        }

    }

}