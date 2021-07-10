/*
 * Date Created: Saturday, June 26, 2021 6:28 AM
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

namespace Ph.Com.CoDe_A.Lakbay.Modules.Core {
    public interface IData {

    }

    [Serializable]
    public class Data : IData {
        public bool highlighted;
        public bool canBeCollided;
        public bool oneTimeCollision;
        public bool canReceiveInput;
        public List<Game.UI.Content.Data> tutorialContents;

        
        public Data() : this(false) {}
        
        public Data(
            bool highlighted=false, 
            bool canBeCollided=true, 
            bool oneTimeCollision=false,
            bool canReceiveInput=true,
            List<Game.UI.Content.Data> tutorialContents=null) {
            this.highlighted = highlighted;
            this.canBeCollided = canBeCollided;
            this.oneTimeCollision = oneTimeCollision;
            this.canReceiveInput = canReceiveInput;
            this.tutorialContents = tutorialContents ?? new List<Game.UI.Content.Data>();

        }

        public Data(Data data) : this(
            data.highlighted,
            data.canBeCollided,
            data.oneTimeCollision,
            data.canReceiveInput,
            data.tutorialContents) {}

        public Data(Controller controller) : this(
            controller.highlighted,
            controller.canBeCollided,
            controller.oneTimeCollision,
            controller.canReceiveInput,
            controller.tutorialContents) {

        }

    }

}