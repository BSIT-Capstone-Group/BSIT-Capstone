/*
 * Date Created: Sunday, July 25, 2021 5:04 AM
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
using UnityEngine.InputSystem;

using NaughtyAttributes;
using TMPro;
using YamlDotNet.Serialization;

using CoDe_A.Lakbay.Utilities;
using CoDe_A.Lakbay.Modules.Core;

namespace CoDe_A.Lakbay.Modules.Game {
    using Event = Utilities.Event;
    using Input = Utilities.Input;
    using LinearPlay.Controllers.Spawners;
    using Core.Controllers;


    public interface IPlayerController : Core.IMoveable {
        int laneIndex { get; set; }
        Vector3 from { get; set; }
        Vector3 to { get; set; }
        RoadController roadController { get; set; }
        
    }

    public class PlayerController : Core.Controller, IPlayerController {
        [SerializeField]
        protected int _laneIndex = -1;
        public virtual int laneIndex {
            get => _laneIndex;
            set {
                if(!value.Within(
                    roadController.playableColumn.startIndex,
                    roadController.playableColumn.endIndex
                )) return;
                if(value == _laneIndex) return;
                var oldIndex = _laneIndex;
                _laneIndex = value;
                
                from = oldIndex.Within(
                    roadController.playableColumn.startIndex,
                    roadController.playableColumn.endIndex
                ) ? roadController.columnPositions[oldIndex] : transform.position;
                to = roadController.columnPositions[laneIndex];

                // print(MoveController.instance);
                MoveController.instance.Move(this as Core.IMoveable);
                
            }

        }
        [SerializeField]
        protected Vector3 _from;
        public virtual Vector3 from {
            get => _from; set => _from = value;

        }
        [SerializeField]
        protected Vector3 _to;
        public virtual Vector3 to {
            get => _to; set => _to = value;

        }
        [SerializeField]
        protected RoadController _roadController;
        public virtual RoadController roadController {
            get => _roadController; set => _roadController = value;

        }


        public Tuple<GameObject, Move> OnMove() {
            return new Tuple<GameObject, Move>(
                gameObject,
                new Move(
                    from, to,
                    speed: 1.0f,
                    duration: 0.15f
                )
            );
            
        }

        public void OnMoving(GameObject gameObject, in Move move) {
            
        }

        public override void Start() {
            base.Start();
            laneIndex = roadController.playableColumn.startIndex;

        }

        public override void Update() {
            base.Update();
            if(Input.keyboard.leftArrowKey.wasPressedThisFrame) laneIndex--;
            if(Input.keyboard.rightArrowKey.wasPressedThisFrame) laneIndex++;

        }

    }

}