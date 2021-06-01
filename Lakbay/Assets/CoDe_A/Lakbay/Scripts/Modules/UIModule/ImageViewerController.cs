using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace CoDe_A.Lakbay.Modules.UIModule {
    public class ImageViewerController : MonoBehaviour {
        public Image image;
        public TMP_Text descriptionText;
        public GameObject descriptionTextPanel;

        public void clear() {
            this.image.sprite = null;
            if(this.descriptionText) {
                this.descriptionText.SetText("");
                this.descriptionTextPanel.gameObject.SetActive(false);

            }

        }

        public void hide() {
            this.gameObject.SetActive(false);
            this.clear();

        }

        public void show(Content.Image contentImage) {
            // Sprite sprite = contentImage.path
            this.show(null, contentImage.description);

        }

        public void show(Sprite sprite, string description) {
            this.clear();
            this.gameObject.SetActive(true);

            if(sprite) this.image.sprite = sprite;
            if(description.Length != 0) {
                if(this.descriptionText) {
                    this.descriptionTextPanel.gameObject.SetActive(true);
                    this.descriptionText.SetText(description);

                }
            
            }

        }

        public void show(Sprite sprite) {
            this.show(sprite, "");
        
        }

        public void show(Image image) {
            this.show(image.sprite);

        }

    }

}
