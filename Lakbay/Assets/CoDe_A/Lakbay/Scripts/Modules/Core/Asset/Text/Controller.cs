/*
 * Date Created: Monday, July 5, 2021 4:42 PM
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

namespace CoDe_A.Lakbay.Modules.Core.Asset.Text {
    using Event = Utilities.Event;

    public interface IController : Asset.IController {

    }
    
    public interface IController<T> : Asset.IController<T>, IController
        where T : IData {
            
    }

    public class Controller : Asset.Controller<Data>, IController<Data> {
        public new const string HeaderName = "Text.Data";
        
        public override Data data {
            get => base.data;
            set {
                if(value == data) return;
                base.data = value;
                var o = data; var n = value;
                if(o != null) o.controller = null;
                if(n != null) n.controller = this;

            }

        }


        [ContextMenu("Localize")]
        public override void Localize() => base.Localize();

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            
        }

    }

}