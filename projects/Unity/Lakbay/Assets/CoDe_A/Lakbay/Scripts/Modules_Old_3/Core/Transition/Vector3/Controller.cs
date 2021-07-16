/*
 * Date Created: Wednesday, July 7, 2021 12:42 PM
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

namespace CoDe_A.Lakbay.ModulesOld3.Core.Transition.Vector3 {
    using Event = Utilities.Event;
    using Vector3_ = UnityEngine.Vector3;

    public interface IController : Core.Transition.IController<Vector3_>, IPropertyEvent {

    }

    [ExecuteAlways]
    public class Controller : Core.Transition.Controller<Vector3_>, IController {
        public new const string BoxGroupName = "Vector3.Controller";

        [ContextMenu("Set Data")]
        public override void SetData() {
            Data.Create(dataTextAsset, this);
            
        }

        public override void Update() {
            base.Update();

        }

        
        // [ContextMenu("hehe")]
        // public override void hehe() => print("hoho");

    }

}