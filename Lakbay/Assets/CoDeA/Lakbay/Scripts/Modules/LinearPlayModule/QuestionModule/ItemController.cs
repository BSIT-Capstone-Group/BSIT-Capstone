using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using CoDeA.Lakbay.Modules.GameModule;
using TMPro;

namespace CoDeA.Lakbay.Modules.LinearPlayModule.QuestionModule {
    [System.Serializable]
    public class Question {
        public List<string> images = new List<string>();
        public string text = "";

    }

    [System.Serializable]
    public class Choice {
        public string text = "";
        public bool correct = false;

    }

    [System.Serializable]
    public class Item {
        public Question question;
        public List<Choice> choices = new List<Choice>();
        
        public float minCoinGain = 5.0f;
        public float maxCoinGain = 30.0f;
        public float minFuelDeduction = 7.0f;
        public float maxFuelDeduction = 15.0f;

    }

    public class ItemController : MonoBehaviour {
        public static readonly float TIMER_ADDITIONAL_TIME = 4.0f;

        [HideInInspector]
        public Choice answeredChoice = null;
        public bool answeredCorrectly {
            get => this.answeredChoice != null && this.answeredChoice.correct;
        }

        [HideInInspector]
        public bool triggered = false;

        public TextAsset itemFile;
        public SetController setController;
        public Item item;
        
        public UIModule.UIController uiController;
        public Utilities.Timer timer;
        // public List<GameObject> triggerAgents = new List<GameObject>();
        public PlayerModule.PlayerController playerController;

        public UnityEvent<ItemController, Choice> onAnswer = new UnityEvent<ItemController, Choice>();

        public bool checkAnswer(Choice choice) => choice != null ? choice.correct : false;

        private void Start() {
            if(this.itemFile) this.setUpItem(this.itemFile);

        }

        private void OnTriggerEnter(Collider collider) {
            this.trigger(collider);

        }

        public void trigger(Collider collider) {
            if(this.triggered) return;

            if(this.playerController.transform == collider.transform) {
                if(this.setController) this.setController.currentItemController = this;
                this.triggered = true;
                
                this.uiController.setQuestionText(this.item.question);

                GameObject[] cbs = this.uiController.setChoiceButtons(this.item.choices.ToArray());
                int i = 0;
                foreach(GameObject cb in cbs) {
                    Button button = cb.GetComponent<Button>();
                    Func<Choice, UnityAction> listener = (Choice c) => () => this.answer(c);
                    button.onClick.AddListener(listener(this.item.choices[i]));

                    i++;

                }

                this.uiController.maximizedImagePanel.SetActive(false);

                GameObject[] imbs = this.uiController.setImageButtons(this.item.question.images.ToArray());
                foreach(GameObject imb in imbs) {
                    Button btn = imb.GetComponent<Button>();
                    btn.onClick.AddListener(this.maximizeImage(
                        imb.gameObject.GetComponent<Image>().sprite
                    ));

                }

                this.uiController.hintButton.onClick.RemoveAllListeners();
                this.uiController.hintButton.onClick.AddListener(this.useHint);

                if(this.playerController.player.hint <= 0) {
                    this.uiController.hintButton.GetComponent<Button>().interactable = false;

                } else {
                    this.uiController.hintButton.GetComponent<Button>().interactable = true;

                }
                
                this.uiController.questionPanel.SetActive(true);
                // this.playerController.GetComponent<Rigidbody>().isKinematic = true;
                this.playerController.vehicleController.sleep();

                this.setUpTimer();

            }

        }

        public void useHint() {
            Transform[] ch = Utilities.Helper.getChildren(this.uiController.choicesPanel.transform);

            System.Random rand = new System.Random();
            List<int> inds = new List<int>();
            int half = (ch.Length / 2) % 2 == 0 ? ch.Length / 2 : (ch.Length / 2) - 1;
            half = Mathf.Max(half, 1);

            while(inds.Count != half) {
                int ri = rand.Next(0, ch.Length - 1);
                if(this.item.choices[ri].correct) continue;

                inds.Add(ri);

            }

            foreach(int i in inds) {
                ch[i].GetComponent<Button>().interactable = false;;

            }

            this.playerController.setHint(this.playerController.player.hint - 1);

            this.uiController.hintButton.GetComponent<Button>().interactable = false;

        }

        public UnityAction maximizeImage(Sprite sprite) {
            return () => {
                this.uiController.setMaximizedImage(sprite);
            };

        }

        public void answer(Choice choice) {
            if(!this.triggered) return;
            this.answeredChoice = choice;

            this.onAnswer.Invoke(this, choice);
            this.hide();

        }

        public void show() {


        }

        public void hide() {
            this.uiController.questionPanel.SetActive(false);
            this.uiController.questionText.SetText("");
            Utilities.Helper.destroyChildren(this.uiController.choicesPanel.transform);

            // this.playerController.GetComponent<Rigidbody>().isKinematic = false;
            this.playerController.vehicleController.wakeUp();

            this.gameObject.SetActive(false);
            if(this.setController) this.setController.currentItemController = null;

        }

        public void setUpTimer() {
            if(this.item == null) return;

            this.timer = this.gameObject.GetComponent<Utilities.Timer>();
            if(!this.timer) this.timer = this.gameObject.AddComponent<Utilities.Timer>();

            float totalCharacters = this.item.question.text.Length;
            List<float> choicesCharacters = this.item.choices.ConvertAll(
                new Converter<Choice, float>((Choice c) => c.text.Length)
            );

            totalCharacters += choicesCharacters.Sum();
            float pTimeDuration = totalCharacters / Utilities.Helper.READING_CHARACTER_PER_SECOND;
            float timeDuration = pTimeDuration + ItemController.TIMER_ADDITIONAL_TIME;

            this.timer.startOnPlay = false;
            this.timer.timeDuration = timeDuration;
            this.timer.onRun.AddListener(this.onTimerRun);
            this.timer.onStop.AddListener(this.onTimerStop);

            this.timer.start();

        }

        public void onTimerRun(Utilities.Timer timer) {
            this.uiController.timeText.SetText(this.timer.time.ToString("N1") + "s");
            
        }

        public void onTimerStop(Utilities.Timer timer) {
            this.answer(null);
            
        }
        
        public void setUpItem(TextAsset itemFile) {
            this.setUpItem(Utilities.Helper.parseYAML<Item>(itemFile.text));

        }

        public void setUpItem(Item item) {
            this.item = item;

        }

    }

}
