/*
 * Date Created: Tuesday, June 29, 2021 10:52 AM
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
    public interface IData : Core.IData {

    }

    public class Data : Core.Data, IData {
        public Data() : this(default(Core.Data)) {}

        public Data(
            Core.Data data=null
        ) : base((Core.Data) (data ?? new Core.Data())) {

        }

        public Data(Data data) : this((Core.Data) data) {}

        public Data(Controller controller) : this(
            (controller as Core.IController).data
        ) {}

    }

}