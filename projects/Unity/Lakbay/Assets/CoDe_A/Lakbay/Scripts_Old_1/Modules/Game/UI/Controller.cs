/*
 * Date Created: Sunday, June 27, 2021 11:55 AM
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

using Ph.Com.CoDe_A.Lakbay.Utilities;

namespace Ph.Com.CoDe_A.Lakbay.Modules.Game.UI {
    public interface IController : Core.IController {
        new Data data { get; set; }


    }

    public class Controller : Core.Controller, IController {
        Data IController.data {
            get => new Data(this); set {
                if(value == null) value = new Data();
                (this as Core.IController).data = value;

            }

        }

        public override void Awake() {
            base.Awake();
            (this as IController).data = new Data();
            
        }

        public override void OnHasUpdate() {
            base.OnHasUpdate();


        }

        public override void SetData(TextAsset data) => (this as IController).data = Helper.Parse<Data>(data);

    }

}