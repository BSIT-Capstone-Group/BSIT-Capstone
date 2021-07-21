/*
 * Date Created: Tuesday, July 20, 2021 9:02 PM
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


    public interface ITutorialController {
        ContentController contentController { get; set; }

        void Show(ITutorial tutorial);
        void Hide();
        
    }

    public class TutorialController : Controller, ITutorialController {
        [SerializeField]
        protected ContentController _contentController;
        public virtual ContentController contentController { get => _contentController; set => _contentController = value; }

        public virtual void Show(ITutorial tutorial) {
            contentController?.Show(tutorial.ViewTutorialContent());

        }

        public virtual void Hide() {
            contentController?.Hide();

        }
        
    }

}