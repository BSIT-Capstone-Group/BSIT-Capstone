/*
 * Date Created: Friday, July 23, 2021 7:28 AM
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


    public interface IFocusController {
        void Focus(IFocusable focusable);
        void Unfocus(IFocusable focusable);
        
    }

    public class FocusController : Controller, IFocusController {
        [SerializeField]
        protected ContentController _contentController;
        public virtual ContentController contentController { get => _contentController; set => _contentController = value; }


        public virtual void Focus(IFocusable focusable) {
            var d = focusable.OnFocus();
            var go = d.Item1;
            var fo = d.Item2;

            if(go.IsTwoDimensional()) {

            } else {
                var o = go.AddComponent<Outline>();
                o.OutlineMode = fo.mode;
                o.OutlineWidth = fo.width;
                o.OutlineColor = fo.color;

            }

        }

        public virtual void Unfocus(IFocusable focusable) {
            var go = focusable.OnUnfocus();
            go.TryDestroyComponent<Outline>();

        }
        
    }

}