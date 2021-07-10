using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;   
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

namespace CoDe_A_Old.Lakbay.Modules.WikiModule {

    [System.Serializable]
    public class Wiki {
        [System.Serializable]
        public class Tab {
            [System.Serializable]
            public class Category {
                [System.Serializable]
                public class Lesson {
                    [System.Serializable]
                    public class Entry {
                        public string text = "";
                        public List<string> images = new List<string>();

                    }

                    public string name;
                    public string thumbnail;

                    public List<Entry> entries = new List<Entry>();

                }

                public string name;
                public List<Lesson> lessons = new List<Lesson>();

            }

            public List<Category> categories = new List<Category>();
        
        }

        public List<Tab> tabs = new List<Tab>();

    }

    public class WikiController : MonoBehaviour {
        public ScrollRect scrollRect;

        public TextAsset wikiFile;
        public Wiki wiki;

        public Wiki.Tab.Category.Lesson lesson;
        public GameObject entryText;
        public GameObject entryImage;

        private async Task Start() {
            print("1");
            AsyncOperationHandle<TextAsset> h = GameModule.DatabaseController.loadAsset<TextAsset>("sample_lesson.yaml");
            await h.Task;
            print("2");
            this.lesson = Utilities.Helper.parseYAML<Wiki.Tab.Category.Lesson>(h.Result.text);
            Transform parent = this.scrollRect.content.transform;

            print(parent);

            foreach(Wiki.Tab.Category.Lesson.Entry entry in this.lesson.entries) {
                if(entry.text.Length != 0) {
                    TMP_Text t = Instantiate<GameObject>(this.entryText, parent).GetComponent<TMP_Text>();
                    t.SetText(entry.text);

                } else if(entry.images.Count != 0) {
                    Transform i = Instantiate<GameObject>(this.entryImage, parent).transform.Find("Image Template");

                    foreach(string im in entry.images) {
                        Transform ii = Instantiate<Transform>(i, i.parent);
                        ii.gameObject.SetActive(true);
                        Image img = ii.GetComponent<Image>();

                        string npath = $"Images/{im}";
                        AsyncOperationHandle<Sprite> hh = GameModule.DatabaseController.loadAsset<Sprite>(npath);
                        await hh.Task;

                        img.sprite = hh.Result;
                    
                    }

                }

            }

        }

        public void setUpWiki(TextAsset wikiFile) {
            this.setUpWiki(Utilities.Helper.parseYAML<Wiki>(wikiFile.text));

        }

        public void setUpWiki(Wiki wiki) {
            this.wiki = wiki;

        }

    }

}
