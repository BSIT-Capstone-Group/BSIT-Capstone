/*
 * Date Created: Tuesday, July 13, 2021 1:14 PM
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

namespace CoDe_A.Lakbay.Modules.Core.Minimal {
    using Event = Utilities.Event;
    using Input = Utilities.Input;


    public interface IController : Core.IController {
        void OnLabelChange(string old, string @new);
        void OnDescriptionChange(string old, string @new);
        
    }

    public interface IController<T> : Core.IController<T>, IController
        where T : IData {
        
    }

    public class Controller<T> : Core.Controller<T>, IController<T>
        where T : IData, new() {
        public virtual void OnDescriptionChange(string old, string @new) {
            

        }

        public virtual void OnLabelChange(string old, string @new) {


        }

    }

}