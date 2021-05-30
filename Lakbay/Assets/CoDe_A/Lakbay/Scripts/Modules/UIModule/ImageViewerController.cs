using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace CoDe_A.Lakbay.Modules.UIModule {
    public class ImageViewerController : MonoBehaviour {
        public Image image;

        public void clear() {
            this.image.sprite = null;

        }

        public void hide() {
            this.gameObject.SetActive(false);
            this.clear();

        }

        public void show(Sprite sprite) {
            this.clear();
            this.gameObject.SetActive(true);

            if(sprite) this.image.sprite = sprite;

        }

        public void show(Image image) {
            this.show(image.sprite);

        }

    }

}
