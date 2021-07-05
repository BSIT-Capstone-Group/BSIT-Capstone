/*
 * Date Created: Monday, July 5, 2021 4:57 AM
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

using NaughtyAttributes;
using TMPro;
using YamlDotNet.Serialization;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.Modules.Core.Asset {
    using Event = Utilities.Event;

    public interface IController : Core.IController {
        void OnPathChange(string old, string @new);

    }

    public interface IController<T> : Core.IController<T>, IController
        where T : IData {

    }

    public class Controller<T> : Core.Controller<T>, IController<T>
        where T : class, IData, new() {
        public override T data {
            get => base.data;
            set {
                if(value == data) return;
                base.data = value;
                var o = data; var n = value;
                // if(o != null) o.controller = null;
                // if(n != null) n.controller = this;

            }

        }


        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            
        }

        public virtual void OnPathChange(string old, string @new) {


        }

    }

}