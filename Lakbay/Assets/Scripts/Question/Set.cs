using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Question {
    public class Set : MonoBehaviour {
        private int score {
            get {
                int score = 0;
                foreach(Question.Item item in this.items) {
                    score += item.correct ? 1 : 0;

                }

                return score;

            }

        }
        private int total {
            get => this.items.Count;

        }
        
        public Question.Display display;
        public List<Transform> triggerAgents = new List<Transform>();
        public GameObject itemsVisual;
        public TextAsset itemsJSON;
        public List<Question.Item> items = new List<Question.Item>();

        private void Awake() {
            this.setUpItemsJSON();

        }

        public void setUpItemsJSON() {
            if(!this.itemsJSON) return;

            this.items = JsonConvert.DeserializeObject<List<Question.Item>>(this.itemsJSON.ToString());

        }

        public GameObject[] instantiate(Transform parent) {
            List<GameObject> visuals = new List<GameObject>();

            foreach(Question.Item item in this.items) {
                GameObject visual = Instantiate<GameObject>(this.itemsVisual);
                Controller controller = visual.AddComponent<Question.Controller>();

                controller.item = item;
                controller.display = this.display;
                controller.triggerAgents = this.triggerAgents;

                // visual.transform.SetParent(parent);
                visuals.Add(visual);
                visual.transform.SetParent(parent);

            }


            return visuals.ToArray();

        }

    }

}
