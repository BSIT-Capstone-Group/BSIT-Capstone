/*
 * Date Created: Tuesday, July 13, 2021 8:58 PM
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

namespace CoDe_A.Lakbay.ModulesOld4.Core.Content.Entry {
    using Event = Utilities.Event;
    using Input = Utilities.Input;
    using BaseIController = Core.Interactable.IController<Data>;
    using BaseController = Core.Interactable.Controller<Data>;


    public interface IController : BaseIController {
        void OnTextChange(string old, string @new);
        
    }

    public class Controller : BaseController, IController {
        public new const string BoxGroupName = "Entry.Controller";

        public virtual void OnTextChange(string old, string @new) {
            

        }

    }

}