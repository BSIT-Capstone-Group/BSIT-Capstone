using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

namespace CoDe_A.Lakbay.Modules.UIModule {
    [System.Serializable]
    public class Content {
        public string text = "";
        public List<string> images = new List<string>();

        public bool isImageContent => images.Count != 0;

    }

    public class ContentController : MonoBehaviour {
        public bool setUpContentOnAwake = false;

        public GameObject textContent;
        public GameObject imageContent;
        public GameObject imageContentImageButton;

        public ToggleGroup toggleGroup;
        public ImageViewerController imageViewerController;

        public TextAsset contentFile;
        public List<Content> contents = null;

        private void Awake() {
            if(this.setUpContentOnAwake) this.setUpContent(this.contentFile);

        }

        public void setUpContent(TextAsset contentFile) {
			this.setUpContent(contentFile, false);

		}

        public void setUpContent(TextAsset contentFile, bool refresh) {
            if(!refresh) {
                if(this.contents != null && this.contents.Count != 0) return;

            }

			this.setUpContent(Utilities.Helper.parseYAML<List<Content>>(contentFile.text));

		}

		public void setUpContent(List<Content> contents) {
			this.contents = contents;
			this.displayContent();

		}

        public void displayContent() {
            Utilities.Helper.destroyChildren(this.transform);

            foreach(Content content in this.contents) {
                if(content.isImageContent) {
                    if(this.imageContent && this.imageContentImageButton) {
                        GameObject imageContent = Instantiate<GameObject>(this.imageContent, this.transform);

                        foreach(string image in content.images) {
                            GameObject imgObj = Instantiate<GameObject>(this.imageContentImageButton, imageContent.transform);
                            Image img = imgObj.GetComponent<Image>();
                            Toggle toggle = imgObj.GetComponent<Toggle>();
                            toggle.group = this.toggleGroup;

                            // img.sprite = GameModule.;

                        }

                    }

                } else {
                    GameObject textContent = Instantiate<GameObject>(this.textContent, this.transform);
                    TMP_Text text = textContent.GetComponent<TMP_Text>();
                    text.SetText(content.text);

                }

            }

        }

        public void clearContent() {
            Utilities.Helper.destroyChildren(this.transform);
            this.contents = null;

        }

    }

}
