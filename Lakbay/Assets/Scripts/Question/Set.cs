using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

namespace Question {
    public class Set : Utilities.ExtendedMonoBehaviour {
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
        
        public bool shuffle = true;
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

            if(this.shuffle) {
                this.shuffleItems();
                
                foreach(Question.Item item in this.items) {
                    item.shuffleChoices();

                }

            }

        }

        public void shuffleItems() {
            System.Random random = new System.Random();
            Question.Item[] aitems = Utilities.Helper.shuffle<Question.Item>(random, this.items.ToArray());
            this.items.Clear();
            this.items.AddRange(new List<Question.Item>(aitems));

        }

        public GameObject[] instantiate(Transform parent) {
            List<GameObject> visuals = new List<GameObject>();

            foreach(Question.Item item in this.items) {
                GameObject visual = Instantiate<GameObject>(this.itemsVisual);
                Controller controller = visual.AddComponent<Question.Controller>();

                controller.item = item;
                controller.display = this.display;
                controller.triggerAgents = this.triggerAgents;
                
                Utilities.Timer timer = visual.AddComponent<Utilities.Timer>();

                float totalCharacters = item.question.Length;
                List<float> choicesCharacters = item.choices.ConvertAll(
                    new Converter<string, float>((string s) => s.Length)
                );

                totalCharacters += Utilities.Helper.getSum(choicesCharacters.ToArray());
                float charactersPerSecond = 25.0f;
                // float minTime = 7.0f, maxTime = 15.0f;
                float pTimeDuration = totalCharacters / charactersPerSecond;
                float additionalTime = 2.25f;
                float timeDuration = pTimeDuration + additionalTime;
                // float timeDuration = Mathf.Max(Mathf.Min(pTimeDuration, maxTime), minTime);

                timer.runOnPlay = false;
                timer.timeDuration = timeDuration;

                controller.timer = timer;

                visuals.Add(visual);
                visual.transform.SetParent(parent);

            }


            return visuals.ToArray();

        }

    }

}
