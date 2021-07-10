/*
 * Date Created: Saturday, June 26, 2021 6:28 AM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

using Ph.Com.CoDe_A.Lakbay.Utilities;

namespace Ph.Com.CoDe_A.Lakbay.Modules.Game.LinearPlay.Player {
    // [CreateAssetMenu(fileName="Player", menuName="ScriptableObjects/SpawnManagerScriptableObject")]
    public interface IData : Core.IData {


    }

    [Serializable]
    public class Data : Core.Data, IData {
        public bool canReceiveSlideInput;
        public bool canReceiveTravelInput;
        public float travelDuration;
        public float travelSpeed;
        public float slideDuration;
        public float slideSpeed;
        public bool isTraveling;
        public int laneIndex;

        public Data() : this(true) {}

        public Data(
            bool canReceiveSlideInput=true,
            bool canReceiveTravelInput=true,
            float travelDuration=60.0f,
            float travelSpeed=1.0f,
            float slideDuration=0.15f,
            float slideSpeed=1.0f,
            bool isTraveling=false,
            int laneIndex=0,
            Core.Data data=null
        ) : base((Core.Data) (data ?? new Core.Data())) {
            this.canReceiveSlideInput = canReceiveSlideInput;
            this.canReceiveTravelInput = canReceiveTravelInput;
            this.travelDuration = travelDuration;
            this.travelSpeed = travelSpeed;
            this.slideDuration = slideDuration;
            this.slideSpeed = slideSpeed;
            this.isTraveling = isTraveling;
            this.laneIndex = laneIndex;

        }

        public Data(Data data) : this(
            data.canReceiveSlideInput,
            data.canReceiveTravelInput,
            data.travelDuration,
            data.travelSpeed,
            data.slideDuration,
            data.slideSpeed,
            data.isTraveling,
            data.laneIndex,
            data
        ) {}


        public Data(Controller controller) : this(
            controller.canReceiveSlideInput,
            controller.canReceiveTravelInput,
            controller.travelDuration,
            controller.travelSpeed,
            controller.slideDuration,
            controller.slideSpeed,
            controller.isTraveling,
            controller.laneIndex,
            (controller as Core.IController).data
        ) {}

    }

}
