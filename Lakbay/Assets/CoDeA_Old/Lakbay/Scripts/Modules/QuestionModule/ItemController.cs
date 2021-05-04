using System.Collections;
using System.Collections.Generic;
//using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;
using System.Linq;
using UnityEngine;

namespace CoDeA_Old.Lakbay.Modules.QuestionModule {
    [System.Serializable]
    public class Question {
        public List<string> images = new List<string>();
        public string text = "";

    }

    [System.Serializable]
    public class Item {
        public Question question;
        public List<string> choices;
        public int correctChoiceIndex;
        public float minCoinGain = 5.0f;
        public float maxCoinGain = 30.0f;
        public float minFuelDeduction = 7.0f;
        public float maxFuelDeduction = 15.0f;

    }

    public class ItemController : Utilities.ExtendedMonoBehaviour {
        public static readonly float TIMER_ADDITIONAL_TIME = 3.0f;

        [HideInInspector]
        public GameObject triggerAgent;
        [HideInInspector]
        public bool answeredCorrectly = false;
    
        public bool triggered = false;
        public bool shuffledItems = false;

        public TextAsset itemFile;

        public PlayerModule.PlayerController playerController;
        public VehicleModule.VehicleController vehicleController;
        public SetController setController;
        public UIModule.UIController uiController;
        public Utilities.Timer timer;
        public List<GameObject> triggerAgents = new List<GameObject>();
        public Item item;

        private void Awake() {
            this.setUpItem();

        }

        private void OnTriggerEnter(Collider collider) {
            this.trigger(collider);
            
        }

        public void setUpItem() {
            if(this.itemFile) {
                this.item = Utilities.Helper.parseYAML<Item>(this.itemFile.ToString());

            }

        }

        public void trigger(Collider collider) {
            if(this.triggered) return;

            foreach(GameObject ta in this.triggerAgents) {
                if(ta.transform == collider.transform) {
                    this.triggered = true;
                    this.uiController.setQuestionText(this.item.question.text);

                    GameObject[] cbs = this.uiController.setChoiceButtons(this.item);
                    foreach(GameObject cb in cbs) {
                        Button button = cb.GetComponent<Button>();
                        Func<int, UnityAction> listener = (int i) => () => this.answer(i);
                        button.onClick.AddListener(listener(new List<GameObject>(cbs).IndexOf(cb)));

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
                    
                    this.uiController.questionModal.SetActive(true);
                    ta.GetComponent<Rigidbody>().isKinematic = true;

                    this.triggerAgent = ta;
                    this.setUpTimer();

                }

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
                if(ri == this.item.correctChoiceIndex) continue;

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

        public void setUpTimer() {
            float totalCharacters = this.item.question.text.Length;
            List<float> choicesCharacters = this.item.choices.ConvertAll(
                new Converter<string, float>((string s) => s.Length)
            );

            totalCharacters += Queryable.Sum(choicesCharacters.AsQueryable());
            float pTimeDuration = totalCharacters / Utilities.Helper.READING_CHARACTER_PER_SECOND;
            float timeDuration = pTimeDuration + ItemController.TIMER_ADDITIONAL_TIME;

            this.timer.startOnPlay = false;
            this.timer.timeDuration = timeDuration;
                    
            Func<ItemController, UnityAction> trlistener = (ItemController ic) => () => {
                ic.uiController.timeText.SetText(ic.timer.timeRemaining.ToString("N1") + "s");
            };
            this.timer.onRun.AddListener(trlistener(this));
            
            Func<ItemController, UnityAction> tslistener = (ItemController ic) => () => {
                ic.answer(-1);
            };
            this.timer.onStop.AddListener(tslistener(this));

            this.timer.start();

        }

        public void shuffleChoices() {
            string correctChoice = this.item.choices[this.item.correctChoiceIndex];

            string[] aitems = Utilities.Helper.shuffle<string>(this.item.choices.ToArray());
            this.item.choices = new List<string>(aitems);
            this.item.correctChoiceIndex = this.item.choices.IndexOf(correctChoice);

        }

        public bool checkAnswer(int choiceIndex) => this.item.correctChoiceIndex == choiceIndex;

        public void answer(int choiceIndex) {
            if(!this.triggered) return;

            string message = "";
            string svalue = "";

            if(choiceIndex == -1) {
                message = "Time's Up! Fuel got reduced by {0}.";

            }
            
            if(this.checkAnswer(choiceIndex)) {
                this.answeredCorrectly = true;
                // this.playerController.setHint(this.setController.point);

                // float maxCoinGain = 30.0f, minCoinGain = 5.0f;
                float maxCoinGain = this.item.maxCoinGain, minCoinGain = this.item.minCoinGain;
                float coinGain = (
                    maxCoinGain * (1.0f - this.timer.progress)
                );
                coinGain = Mathf.Max(coinGain, minCoinGain);
                coinGain = Convert.ToInt32(coinGain);
                float currentCoin = this.playerController.player.coin + coinGain;

                this.playerController.setCoin(currentCoin);

                string cg = coinGain.ToString("N0");
                message = "Correct Answer! Coins increased by {0}.";
                svalue = cg;

            } else {
                this.answeredCorrectly = false;

                // float maxFuelDeduction = this.vehicleController.maxFuel * 0.15f;
                // float minFuelDeduction = this.vehicleController.maxFuel * 0.05f;
                float maxFuelDeduction = this.item.maxFuelDeduction;
                float minFuelDeduction = this.item.minFuelDeduction;
                float fuelDeduction = maxFuelDeduction * this.timer.progress;
                fuelDeduction = Mathf.Max(maxFuelDeduction * this.timer.progress, minFuelDeduction);

                this.vehicleController.setFuel(this.vehicleController.vehicle.fuel - fuelDeduction);

                string fd = fuelDeduction.ToString("N0");
                message = choiceIndex != -1 ? "Wrong Answer! Fuel got reduced by {0}." : message;
                svalue = fd;

            }

            this.uiController.notification.show(
                String.Format(message, svalue),
                3.5f
            );

            this.hide();

        }

        public void show() {


        }

        public void hide() {
            this.uiController.questionModal.SetActive(false);
            this.uiController.questionText.SetText("");
            Utilities.Helper.destroyChildren(this.uiController.choicesPanel.transform);

            foreach(GameObject ta in this.triggerAgents) {
                ta.GetComponent<Rigidbody>().isKinematic = false;

            }

            this.gameObject.SetActive(false);

        }

    }

}
