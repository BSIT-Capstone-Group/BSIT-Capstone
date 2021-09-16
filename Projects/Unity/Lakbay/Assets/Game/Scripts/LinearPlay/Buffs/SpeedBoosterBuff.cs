/*
 * Date Created: Wednesday, September 8, 2021 6:30 AM
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

namespace Ph.CoDe_A.Lakbay.LinearPlay.Buffs {
    public class SpeedBoosterBuff : Buff {
        protected IBuffable _buffable;

        public virtual SpeedBoosterBuffTrigger[] triggers {
            get => GetComponentsInChildren<SpeedBoosterBuffTrigger>();

        }

        public virtual SpeedBoosterBuffInnerTrigger innerTrigger {
            get => triggers.Find(
                (t) => t.IsInstance(typeof(SpeedBoosterBuffInnerTrigger))
            ) as SpeedBoosterBuffInnerTrigger;

        }

        public virtual SpeedBoosterBuffOuterTrigger[] outerTriggers {
            get => triggers.FindAll(
                (t) => t.IsInstance(typeof(SpeedBoosterBuffOuterTrigger))
            ).Cast<SpeedBoosterBuffOuterTrigger>().ToArray();

        }

        public override void OnAdd(IBuffable buffable) {
            base.OnAdd(buffable);
            _buffable = buffable;
            var player = buffable as Player;
            player.travelSpeed *= 2;
            // player.slideSpeed *= 2;

        }

        public override void OnRemove(IBuffable buffable) {
            base.OnRemove(buffable);
            _buffable = null;
            var player = buffable as Player;
            player.travelSpeed /= 2;
            // player.slideSpeed /= 2;

        }

        public override void OnTriggerEnter(Collider collider) {
            base.OnTriggerEnter(collider);

            if(innerTrigger && outerTriggers != null) {
                if(innerTrigger.triggered) {
                    foreach(var outerTrigger in outerTriggers) {
                        if(!outerTrigger.triggered) {
                            var otxpos = outerTrigger.transform.position.x;
                            var xpos = transform.position.x;
                            var player = _buffable as Player;

                            if(otxpos > xpos) {
                                if(player.slideIndex < player.slideMaxIndex) {
                                    player.SlideRight();

                                } else player.SlideLeft();

                            } else {
                                if(player.slideIndex > player.slideMinIndex) {
                                    player.SlideLeft();

                                } else player.SlideRight();

                            }

                        }

                    }

                }

            }

        }

    }

}