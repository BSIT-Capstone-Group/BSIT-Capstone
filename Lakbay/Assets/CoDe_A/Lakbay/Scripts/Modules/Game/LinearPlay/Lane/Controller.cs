/*
 * Date Created: Saturday, June 26, 2021 6:28 AM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.Modules.Game.LinearPlay.Lane {
    public class Controller : Input.Controller {
        protected List<Mesh.Controller> _meshControllers = new List<Mesh.Controller>();
        [BoxGroup("Lane.Controller")]
        public Data data;
        [BoxGroup("Lane.Controller")]
        public GameObject meshHolder;
        [BoxGroup("Lane.Controller")]
        public Mesh.Controller meshController;
        public Vector3 startPosition {
            get {
                if(_meshControllers.Count == 0) return Vector3.zero;
                return _meshControllers[0].transform.position;

            }

        }
        public Vector3 endPosition {
            get {
                int c = _meshControllers.Count;
                if(c == 0) return Vector3.zero;
                return _meshControllers[c - 1].transform.position;

            }

        }

        protected void FillMeshControllersWithNulls() {
            _meshControllers.Clear();
            for(int i = 0; i < data.length; i++) _meshControllers.Add(null);

        }

        public void Populate(int index) {
            Transform transform = meshHolder.transform;
            if(_meshControllers.Count != data.length) FillMeshControllersWithNulls();
            if(index < 0 || index >= data.length) return;
            if(_meshControllers[index]) Remove(index);
            var go = Instantiate<GameObject>(meshController.gameObject, transform);
            var mc = go.GetComponent<Mesh.Controller>();
            var pos = transform.position;
            mc.transform.position = pos + new Vector3(0, 0, mc.size.z * index); 
            _meshControllers[index] = mc;

        }

        public void Populate() {
            for(int i = 0; i < data.length; i++) Populate(i);
            
        }

        public void Remove(int index) {
            if(_meshControllers.Count != data.length) FillMeshControllersWithNulls();
            if(index < 0 || index >= data.length) return;
            var mc = Helper.Replace<Mesh.Controller>(_meshControllers, index);
            Destroy(mc.gameObject);

        }

        public void Remove() {
            for(int i = data.length - 1; i >= 0; i--) Remove(i);

        }

    }

}