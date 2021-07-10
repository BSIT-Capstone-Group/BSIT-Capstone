using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoDe_A_Old.Lakbay.Modules.UIModule {
    // [CreateAssetMenu(fileName="Nested Content", menuName="ScriptableObjects/NestedContent", order=1)]
    public class NestedContent : ScriptableObject {
        public enum Type {
            FOLDER, READABLE, WATCHABLE

        }

        [HideInInspector]
        public NestedContent parent;

        public List<NestedContent> children = new List<NestedContent>();

        public string label = "";
        public Sprite thumbnail;
        // public string thumbnail = "";

        public TextAsset contentsFile;
        // public List<Content> contents = new List<Content>();
        public TextAsset videoFile;

        public Type type {
            get {
                if(this.contentsFile != null) return Type.READABLE;
                if(this.videoFile != null) return Type.WATCHABLE;
                else return Type.FOLDER;

            }
        }
        
    }

}
