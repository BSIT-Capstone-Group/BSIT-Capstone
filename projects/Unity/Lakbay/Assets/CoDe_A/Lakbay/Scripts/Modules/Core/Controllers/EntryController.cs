/*
 * Date Created: Wednesday, July 21, 2021 6:49 AM
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


    public interface IEntryController {
        TextController textController { get; set; }
        ImageController imageController { get; set; }

        void Show(Entry entry);
        void Hide();
        
    }

    public class EntryController : Controller, IEntryController {
        [SerializeField]
        protected TextController _textController;
        public virtual TextController textController { get => _textController; set => _textController = value; }
        [SerializeField]
        protected ImageController _imageController;
        public virtual ImageController imageController { get => _imageController; set => _imageController = value; }


        public virtual void Show(Entry entry) {
            gameObject.DestroyChildren();

            if(entry.type == Entry.Type.Text) {
                var controller = Instantiate(textController, transform);
                controller.component.SetText(entry.text.value);

            } else if(entry.type == Entry.Type.Image) {
                foreach(var image in entry.images) {
                    var controller = Instantiate(imageController, transform);
                    controller.component.sprite = image.asset;

                }
                
            }

        }

        public virtual void Hide() {
            

        }
    }

}