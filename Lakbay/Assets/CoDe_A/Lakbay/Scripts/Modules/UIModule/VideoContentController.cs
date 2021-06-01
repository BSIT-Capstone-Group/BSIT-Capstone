using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CoDe_A.Lakbay.Modules.UIModule {
    [System.Serializable]
    public class VideoContent {
        public string url = "";
        public string thumbnail = "";
        public string time = "";
        public string label = "";
        public string author = "";
        public string description = "";

    }

    public class VideoContentController : MonoBehaviour {
        public TMP_InputField urlText;
        public Image thumbnailImage;
        public TMP_Text timeText;
        public TMP_Text labelText;
        public TMP_Text authorText;
        public TMP_Text descriptionText;
        public Button playButton;

        public TextAsset videoFile;
        public VideoContent videoContent;

        public void setUpContent(TextAsset videoFile) {
			this.setUpContent(videoFile, false);

		}

        public void setUpContent(TextAsset videoFile, bool refresh) {
            if(!refresh) {
                if(this.videoContent != null) return;

            }

			this.setUpContent(Utilities.Helper.parseYAML<VideoContent>(videoFile.text));

		}

		public void setUpContent(VideoContent videoContent) {
			this.videoContent = videoContent;
			this.displayContent();

		}

        public void displayContent() {
            this.urlText.text = this.videoContent.url;
            // this.thumbnailImage.sprite = this.videoContent.url;
            this.timeText.SetText(this.videoContent.time);
            this.labelText.SetText(this.videoContent.label);
            this.authorText.SetText(this.videoContent.author);
            this.descriptionText.SetText(this.videoContent.description);

            this.playButton.onClick.RemoveAllListeners();
            this.playButton.onClick.AddListener(() => GameModule.GameController.openURL(this.videoContent.url));

        }

        public void clearContent() {
            // Utilities.Helper.destroyChildren(this.transform);
            this.videoContent = null;

        }

    }

}
