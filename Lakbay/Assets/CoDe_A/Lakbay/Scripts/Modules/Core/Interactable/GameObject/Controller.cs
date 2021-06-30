/*
 * Date Created: Wednesday, June 30, 2021 9:52 AM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using NaughtyAttributes;

using Code_A.Lakbay.Utilities;

namespace Code_A.Lakbay.Modules.Core.Interactable.GameObject {
    public interface IController : Interactable.IController {
        new Data data { get; set; }

    }

    public class Controller : Interactable.Controller, IController {
        Data IController.data {
            get => new Data(this); set {
                if(value == null) value = new Data();
                (this as Interactable.IController).data = value;

            }

        }


        public Controller() : base() {
            (this as IController).data = null;

        }

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();

        }

        public override void SetData(TextAsset textAsset) => (this as IController).data = new Data(textAsset);

    }

}