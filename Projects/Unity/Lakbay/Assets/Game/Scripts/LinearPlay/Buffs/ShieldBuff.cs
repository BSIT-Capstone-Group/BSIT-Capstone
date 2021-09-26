/*
 * Date Created: Friday, September 3, 2021 1:27 PM
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
    public class ShieldBuff : Buff {
        protected readonly List<Material> _originalMaterials = new List<Material>();
        protected readonly List<MeshRenderer> _renderers = new List<MeshRenderer>(); 
        public Material shield;

        public override void OnAdd(IBuffable buffable) {
            base.OnAdd(buffable);
            var player = buffable as Player;
            if(player) ApplyMaterial(player.vehicle.gameObject);
            
        }

        public override void OnLinger(IBuffable buffable) {
            base.OnLinger(buffable);

        }

        public override void OnRemove(IBuffable buffable) {
            base.OnRemove(buffable);
            var player = buffable as Player;
            if(player) RevertMaterial();

        }

        public override void OnTriggerEnter(Collider collider) {
            base.OnTriggerEnter(collider);
            var obstacle = collider.GetComponentInParent<Spawns.ObstacleSpawn>();
            if(obstacle) {
                obstacle.Break();
                obstacle.damaging = false;

            }

        }

        public virtual void ApplyMaterial(GameObject gameObject) {
            _renderers.Clear();
            _originalMaterials.Clear();
            var renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach(var renderer in renderers) {
                _renderers.Add(renderer);
                _originalMaterials.Add(renderer.material);
                renderer.material = shield;

            }

        }

        public virtual void RevertMaterial() {
            foreach(var renderer in _renderers.Enumerate()) {
                renderer.Value.material = _originalMaterials[renderer.Key];

            }

        }

    }

}