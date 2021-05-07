using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CoDeA.Lakbay.Modules.LinearPlayModule.QuestionModule {
    [System.Serializable]
    public class Set {
        public string name = "";

        public List<Item> items = new List<Item>();

    }

    public class SetController : MonoBehaviour {
        public TextAsset setFile;
        public Set set;

        public List<ItemController> itemControllers = new List<ItemController>();

        public float score {
            get => this.itemControllers.Select<ItemController, float>(
                (ic) => ic.answeredCorrectly ? 1.0f : 0.0f
            ).Sum();
        }

        public float maxScore { get => this.itemControllers.Count; }

    }

}
