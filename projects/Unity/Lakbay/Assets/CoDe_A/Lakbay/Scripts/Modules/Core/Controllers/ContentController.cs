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
        void Show(List<Entry> content);
        void Hide();
        
    }

    public class ContentController : Controller, IContentController {
        public virtual void Show(List<Entry> content) {
        

        }
        
        public virtual void Hide() {
            

        }

    }

}