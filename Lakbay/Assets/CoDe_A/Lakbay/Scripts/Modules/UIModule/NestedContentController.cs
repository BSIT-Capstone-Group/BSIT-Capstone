using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine;
using System;
using System.Linq;

namespace CoDe_A.Lakbay.Modules.UIModule {
    public class NestedContentController : MonoBehaviour {
        [HideInInspector]
        public NestedContent currentNestedContent = null;

        public bool setUpContentOnAwake = false;

        public GameObject nestedContentPanel;
        public GameObject nestedContentPathPanel;
        public GameObject nestedContentPathEntryPanel;
        public GameObject nestedContentEntryCategory;
        public GameObject nestedContentEntryInteractable;

        public GameObject openedNestedContentMainPanel;
        public GameObject openedNestedContentPanel;

        public NestedContent nestedContent;
        // public TextAsset nestedContentFile;
        // public List<NestedContentEntry> nestedContent = new List<NestedContentEntry>();
        public ContentController contentController;
        public VideoContentController videoContentController;

        public List<NestedContent> currentNestedContentParents {
            get {
                if(this.currentNestedContent == null) return new List<NestedContent>();

                List<NestedContent> parents = new List<NestedContent>();
                NestedContent nc = this.currentNestedContent;

                while(nc.parent != null) {
                    parents.Insert(0, nc.parent);
                    nc = nc.parent;

                }

                return parents;

            }
        }

        private void Awake() {
            if(this.setUpContentOnAwake) {
                this.setUpNestedContent(this.nestedContent);
                // if(this.nestedContentFile) {
                //     this.setUpNestedContent(nestedContentFile);
                // } else this.setUpNestedContent(this.nestedContent);
            }

        }

        // public void setUpNestedContent(TextAsset nestedContentFile) {
		// 	this.setUpNestedContent(nestedContentFile, false);

		// }

        // public void setUpNestedContent(TextAsset nestedContentFile, bool refresh) {
        //     if(!refresh) {
        //         if(this.nestedContent != null && this.nestedContent.Count != 0) return;

        //     }

		// 	this.setUpNestedContent(Utilities.Helper.parseYAML<List<NestedContentEntry>>(nestedContentFile.text));

		// }

        public void setCurrentNestedContent() {
            NestedContent p = this.currentNestedContent;
            if(p == null) return;

            while(p.parent != null) p = p.parent;
            this.setCurrentNestedContent(p);

        }

        public void setCurrentNestedContent(NestedContent nestedContent) {
            this.currentNestedContent = nestedContent;
            this.displayNestedContent();

        }

		public void setUpNestedContent(NestedContent nestedContent) {
			this.nestedContent = nestedContent;
            this.setUpNestedContentHierarchy();
			this.displayNestedContent();

		}

        public void setUpNestedContentHierarchy() {
            List<NestedContent> nestedContent = new List<NestedContent>();
            nestedContent.AddRange(this.nestedContent.children);

            foreach(var c in this.nestedContent.children) c.parent = this.nestedContent;

            while(nestedContent.Count != 0) {
                NestedContent nce = nestedContent[0];
                nestedContent.RemoveAt(0);

                foreach(var nce_ in nce.children) {
                    nce_.parent = nce;

                }

                nestedContent.AddRange(nce.children);

            }

            // this.currentNestedContent = this.nestedContent;
            this.setCurrentNestedContent(this.nestedContent);

        }

        public void openNestedContent(NestedContent nestedContent) {
            if(nestedContent.type == NestedContent.Type.FOLDER) return;

            if(nestedContent.type == NestedContent.Type.READABLE) {
                this.openedNestedContentMainPanel.SetActive(true);
                this.contentController.titleText.SetText(nestedContent.label);
                this.contentController.setUpContent(nestedContent.contentsFile, true);

            } else {
                this.videoContentController.gameObject.SetActive(true);
                this.videoContentController.setUpContent(nestedContent.videoFile, true);

            }

        }

        public void displayNestedContent() {
            if(this.currentNestedContent != null) {
                Utilities.Helper.destroyChildren(this.nestedContentPanel.transform);
                Utilities.Helper.destroyChildren(this.nestedContentPathPanel.transform);

                foreach(var nce in this.currentNestedContent.children) {
                    Image img = null;
                    TMP_Text text = null;
                    GameObject e = null;
                    Button btn = null;
                    Func<NestedContent, UnityAction> c = (x) => null;

                    if(nce.type == NestedContent.Type.FOLDER) {
                        e = Instantiate<GameObject>(this.nestedContentEntryCategory, this.nestedContentPanel.transform);

                    } else {
                        e = Instantiate<GameObject>(this.nestedContentEntryInteractable, this.nestedContentPanel.transform);

                    }

                    btn = e.GetComponent<Button>();
                    img = e.GetComponentInChildren<Image>();
                    text = e.GetComponentInChildren<TMP_Text>();

                    // img.sprite = 
                    text.SetText(nce.label);
                    
                    if(nce.type == NestedContent.Type.FOLDER) {
                        c = (x) => {
                            return () => this.setCurrentNestedContent(x);
                        };

                    } else {
                        c = (x) => {
                            return () => this.openNestedContent(x);
                        };

                    }

                    btn.onClick.AddListener(c(nce));

                }

                var ps = this.currentNestedContentParents;
                foreach(var p in ps) {
                    var e = Instantiate<GameObject>(this.nestedContentPathEntryPanel, this.nestedContentPathPanel.transform);
                    var img = e.GetComponentInChildren<Image>();
                    var text = e.GetComponentInChildren<TMP_Text>();
                    var sep = e.transform.Find("Separator Panel");
                    var btn = e.GetComponentInChildren<Button>();

                    sep.gameObject.SetActive(ps.IndexOf(p) != ps.Count - 1);
                    img.sprite = p.thumbnail;
                    text.SetText(ps.IndexOf(p) != ps.Count - 1 ? ps[ps.IndexOf(p) + 1].label : this.currentNestedContent.label);
                    
                    Func<NestedContent, UnityAction> c = (x) => {
                        return () => this.setCurrentNestedContent(x);
                    };

                    btn.onClick.AddListener(c(p));


                }

            }

        }

    }

}
