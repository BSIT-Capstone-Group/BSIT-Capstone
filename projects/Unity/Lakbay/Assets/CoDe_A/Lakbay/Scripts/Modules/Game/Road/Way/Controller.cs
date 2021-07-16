/*
 * Date Created: Tuesday, July 13, 2021 6:04 PM
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

namespace CoDe_A.Lakbay.Modules.Game.Road.Way {
    using Event = Utilities.Event;
    using Input = Utilities.Input;


    public interface IController : Core.Interactable.IController<Data> {
        void OnLengthChange(int old, int @new);
        void OnSpawnsChange(List<GameObject> old, List<GameObject> @new);

        void Spawn(int index, int spawnIndex=-1);
        void Spawn();
        
    }

    public class Controller : Core.Interactable.Controller<Data>, IController {
        public virtual void OnLengthChange(int old, int @new) {
            // GetComponent<Renderer>().bounds.s

        }

        public virtual void OnSpawnsChange(List<GameObject> old, List<GameObject> @new) {
            

        }
        
        public virtual void Spawn(int index, int spawnIndex=-1) {
            var pos = transform.position;
            pos.z = index * data.groundSize.z;
            var g = Instantiate(data.ground, transform);
            g.transform.position = pos;

            if(spawnIndex >= 0 && (data.spawns != null && spawnIndex < data.spawns.Count)) {
                if(!data.spawnedSpawnsIndices.Contains(spawnIndex)) {
                    var spawn = data.spawns[spawnIndex];
                    var s = Instantiate(spawn, g.transform);

                    data.spawnedSpawnsIndices.Add(spawnIndex);

                }

            }

        }

        public virtual void Spawn() {
            gameObject.DestroyChildren();
            data.spawnedSpawnsIndices.Clear();

            for(int i = 0; i < data.length; i++) {
                Spawn(i);

            }

        }

        public override void Update() {
            base.Update();

            // if(data.playing) {
            //     if(transform.childCount != data.length) {
            //         Spawn();
                    
            //     }

            // }

        }

    }

}