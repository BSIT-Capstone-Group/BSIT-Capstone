using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.Modules.CoreModule {
    public interface IController {
        void localize();
    }

    /// <summary>The base class for anything attachable to a <see cref="GameObject"/>.</summary>
    public class Controller : ExtendedMonoBehaviour, IController {
        [ContextMenu("Localize")]
        public virtual void localize() {}

    }

}
