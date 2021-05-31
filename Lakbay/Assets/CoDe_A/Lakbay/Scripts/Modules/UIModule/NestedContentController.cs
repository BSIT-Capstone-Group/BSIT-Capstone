using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Linq;

namespace CoDe_A.Lakbay.Modules.UIModule {
    [System.Serializable]
    public class NestedContentEntry {
        // public NestedContentEntry parent;

        public string path = "";
        public string name = "";
        public string thumbnail = "";

        // public List<NestedContentEntry> children = new List<NestedContentEntry>();

        // public bool isCategory => this.children.Count != 0;
        public bool isCategory => contents.Count == 0;

        public string contentPath = "";
        public List<Content> contents = new List<Content>();

    }

    public class NestedContentController : MonoBehaviour {
        [HideInInspector]
        public List<NestedContentEntry> currentNestedContent = null;

        public bool setUpContentOnAwake = false;

        public GameObject nestedContentPanel;
        public GameObject nestedContentPathPanel;
        public GameObject nestedContentEntryCategory;
        public GameObject nestedContentEntryInteractable;

        public TextAsset nestedContentFile;
        public List<NestedContentEntry> nestedContent = new List<NestedContentEntry>();

        private void Awake() {
            if(this.setUpContentOnAwake) {
                if(this.nestedContentFile) {
                    this.setUpNestedContent(nestedContentFile);
                } else this.setUpNestedContent(this.nestedContent);
                
                print("awake");
            }

        }

        public void setUpNestedContent(TextAsset nestedContentFile) {
			this.setUpNestedContent(nestedContentFile, false);

		}

        public void setUpNestedContent(TextAsset nestedContentFile, bool refresh) {
            if(!refresh) {
                if(this.nestedContent != null && this.nestedContent.Count != 0) return;

            }

			this.setUpNestedContent(Utilities.Helper.parseYAML<List<NestedContentEntry>>(nestedContentFile.text));

		}

		public void setUpNestedContent(List<NestedContentEntry> nestedContent) {
            print("222");
			this.nestedContent = nestedContent;
            print("22");
            this.setUpNestedContentHierarchy();
			this.displayNestedContent();

		}

        public void setUpNestedContentHierarchy() {
            print("1 " + this.nestedContent.Count);
            List<NestedContentEntry> nestedContent = new List<NestedContentEntry>();
            nestedContent.AddRange(this.nestedContent);
            print("2");

            while(nestedContent.Count != 0) {
                NestedContentEntry nce = nestedContent[0];
                nestedContent.RemoveAt(0);

                // List<string> paths = nce.name.Split('/').ToList();
                // NestedContentEntry nnce = null;
                // List<NestedContentEntry> nncec = nnce.children;

                // while(paths.Count != 0) {
                //     string path = paths[0];
                //     paths.RemoveAt(0);
                    
                //     bool found = false;
                //     foreach(var nnce_ in nncec) {
                //         if(path == nnce_.name) {
                //             nnce = nnce_;
                //             found = true;
                //             break;
                            
                //         }

                //     }

                //     if(!found) {
                //         if(nnce == null) break;
                //         else {
                //             var nnce_ = new NestedContentEntry();
                //             nnce_.parent = nnce;

                //         }

                //     } 

                // }

                // if(nnce != null) {
                //     nnce.children.Add(nce);

                // }

                if(!nce.isCategory && nce.contentPath.Length != 0) {
                    // TextAsset ta = GameModule.DatabaseController.loadAsset<TextAsset>(nce.contentPath);
                    // TextAsset ta = await GameModule.DatabaseController.loadAsset<TextAsset>(nce.contentPath);
                    // nce.content = 

                } 

                // foreach(var nce_ in nce.children) {
                //     // nce_.parent = nce;

                // }

                // nestedContent.AddRange(nce.children);

            }

            if(this.nestedContent.Count != 0) this.currentNestedContent = this.nestedContent;

        }

        public void displayNestedContent() {
            if(this.currentNestedContent != null) {
                Utilities.Helper.destroyChildren(this.nestedContentPanel.transform);
                Utilities.Helper.destroyChildren(this.nestedContentPathPanel.transform);

                foreach(var nce in this.currentNestedContent) {
                    Image img = null;
                    TMP_Text text = null;
                    GameObject e = null;

                    if(nce.isCategory) {
                        e = Instantiate<GameObject>(this.nestedContentEntryCategory, this.nestedContentPanel.transform);

                    } else {
                        e = Instantiate<GameObject>(this.nestedContentEntryInteractable, this.nestedContentPanel.transform);

                    }

                    img = e.GetComponentInChildren<Image>();
                    text = e.GetComponentInChildren<TMP_Text>();

                    // img.sprite = 
                    text.SetText(nce.name);

                }

            }

        }

    }

}
