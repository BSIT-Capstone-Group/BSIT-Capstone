/*
 * Date Created: Friday, August 27, 2021 9:37 AM
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
using UnityEngine.UI;

using TMPro;

using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Behaviours {
    public class SectionController : Controller {
        public static int spawnCount = 0;
        public static int spawnCountLimit = 20;
        public static bool canSpawnNext = true;

        // public int safeSectionCount = 5;
        // protected int _currentSafeSectionCount = 0;
        protected bool _doneSpawningNext = false;
        protected List<SpawnController> _currentSpawns = new List<SpawnController>();
        public List<SpawnController> spawns = new List<SpawnController>();
        public List<GameObject> spawnAreas = new List<GameObject>();

        public override void Awake() {
            base.Awake();
            _doneSpawningNext = false;

        }

        public override void Update() {
            base.Update();
            if(gameObject.IsBoundsVisible() && !_doneSpawningNext) SpawnNext();

        }

        public override void OnBoundsInvisible() {
            base.OnBoundsInvisible();
            Destroy(gameObject);
            spawnCount--;

        }

        public virtual void SpawnNext() {
            if(spawnCount < spawnCountLimit && canSpawnNext) {
                var go = Instantiate(gameObject);
                go.name = gameObject.name;
                go.transform.SetParent(transform.parent);
                go.transform.Translate(Vector3.forward * gameObject.GetSize().z);
                spawnCount++;
                _doneSpawningNext = true;

            }

        } 

    }

}