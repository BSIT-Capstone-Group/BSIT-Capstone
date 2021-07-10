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

namespace CoDe_A.Lakbay.ModulesOld2.Core.Asset {
    using Event = Utilities.Event;

    public interface IController<T0, T1> : Core.IController {
        void OnPathChange(string old, string @new);
        void OnComponentChange(T1 old, T1 @new);
        void OnAssetChange(T0 old, T0 @new);

    }

    public interface IController<T0, T1, T2> : Core.IController<T0>, IController<T1, T2>
        where T0 : IData<T1, T2> {

    }

    public class Controller<T0, T1, T2> : Core.Controller<T0>, IController<T0, T1, T2>
        where T0 : class, IData<T1, T2>, new()
        where T1 : UnityEngine.Object
        where T2 : MonoBehaviour {
        public override T0 data {
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

        public virtual void OnComponentChange(T2 old, T2 @new) {

        }

        public virtual void OnAssetChange(T1 old, T1 @new) {

        }

        public virtual void OnPathChange(string old, string @new) {


        }

    }

}