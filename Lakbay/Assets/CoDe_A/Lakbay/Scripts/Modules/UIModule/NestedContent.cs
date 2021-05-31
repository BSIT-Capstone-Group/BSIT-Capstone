using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoDe_A.Lakbay.Modules.UIModule {
    [CreateAssetMenu(fileName="Nested Content", menuName="ScriptableObjects/NestedContent", order=1)]
    public class NestedContent : ScriptableObject {
        [HideInInspector]
        public NestedContent parent;

        public List<NestedContent> children = new List<NestedContent>();

        public string label = "";
        public Sprite thumbnail;
        // public string thumbnail = "";

        public TextAsset contentsFile;
        // public List<Content> contents = new List<Content>();

        public bool isFolder => this.contentsFile == null;
        
    }

}
