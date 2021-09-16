/*
 * Date Created: Friday, September 3, 2021 9:33 AM
 * Author: NI.L.A
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

using TMPro;

using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Behaviours {
    public interface IBuffable {
        void AddBuff(params Buff[] buffs);
        void RemoveBuff(params Buff[] buffs);

    }

    public class Buff : Controller {
        public readonly bool Stackable = false;

        public float _duration = -1.0f;
        public virtual float duration { get => _duration; set => _duration = value; }
        public virtual bool permanent => duration < 0.0f;

        public virtual void OnAdd(IBuffable buffable) {
            
        }

        public virtual void OnLinger(IBuffable buffable) {

        }

        public virtual void OnRemove(IBuffable buffable) {

        }

    }

}