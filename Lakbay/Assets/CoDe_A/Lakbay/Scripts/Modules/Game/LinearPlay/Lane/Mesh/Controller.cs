using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.Modules.Game.LinearPlay.Lane.Mesh {
    public class Controller : Input.Controller {
        [BoxGroup("Mesh.Controller")]
        public Renderer sizeRenderer;
        public Vector3 size => sizeRenderer ? sizeRenderer.bounds.size : Vector3.zero;

    }

}