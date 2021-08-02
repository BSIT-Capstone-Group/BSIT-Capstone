/*
 * Date Created: Tuesday, July 20, 2021 6:55 PM
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

namespace CoDe_A.Lakbay.Modules.Game.LinearPlay {
    using Event = Utilities.Event;
    using Input = Utilities.Input;


    [Serializable]
    public struct Road {
        [SerializeField]
        private List<List2D<List2D<string>>> _rows;
        public List<List2D<List2D<string>>> rows { get => _rows; set => _rows = value; }


        public Road(int columnCount=0, int rowCount=0) {
            _rows = new List<List2D<List2D<string>>>();

            for(int i = 0; i < rowCount; i++) {
                rows.Add(new List2D<List2D<string>>(
                    from ii in Enumerable.Range(0, columnCount) select new List2D<string>()
                ));

            }

        }

    }

}