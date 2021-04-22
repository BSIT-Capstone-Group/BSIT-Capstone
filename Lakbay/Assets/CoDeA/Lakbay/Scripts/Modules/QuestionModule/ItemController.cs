using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;
using UnityEngine;

namespace CoDeA.Lakbay.Modules.QuestionModule {
    [System.Serializable]
    public class Item {
        public string question;
        public List<string> choices;
        public int correctChoiceIndex;

    }

    public class ItemController : Utilities.ExtendedMonoBehaviour {
        [HideInInspector]
        public GameObject triggerAgent;
        [HideInInspector]
        public bool answeredCorrectly = false;

        public bool triggered = false;

        public PlayerModule.PlayerController playerController;
        public VehicleModule.VehicleController vehicleController;
        public SetController setController;
        public Item item;
        public UIModule.UIController uiController;
        public Utilities.Timer timer;
        public List<GameObject> triggerAgents = new List<GameObject>();

        private void Awake() {

        }

        private void OnTriggerEnter(Collider collider) {
            this.trigger(collider);
            
        }

        public void trigger(Collider collider) {
            if(this.triggered) return;

            foreach(GameObject ta in this.triggerAgents) {
                if(ta.transform == collider.transform) {
                    this.triggered = true;
                    this.uiController.questionText.SetText(this.item.question);
                    Utilities.Helper.destroyChildren(this.uiController.choicesPanel.transform);

                    foreach(string choice in this.item.choices) {
                        GameObject choiceButton = Instantiate<GameObject>(
                            this.uiController.choiceButton, this.uiController.choicesPanel.transform
                        );

                        Button button = choiceButton.GetComponent<Button>();
                        Func<int, UnityAction> listener = (int i) => () => this.answer(i);
                        button.onClick.AddListener(listener(this.item.choices.IndexOf(choice)));

                        TMP_Text text = button.transform.GetChild(0).GetComponent<TMP_Text>();
                        text.SetText(choice);

                    }

                    float totalCharacters = this.item.question.Length;
                    List<float> choicesCharacters = this.item.choices.ConvertAll(
                        new Converter<string, float>((string s) => s.Length)
                    );

                    totalCharacters += Utilities.Helper.getSum(choicesCharacters.ToArray());
                    float pTimeDuration = totalCharacters / Utilities.Helper.READING_CHARACTER_PER_SECOND;
                    float additionalTime = 2.25f;
                    float timeDuration = pTimeDuration + additionalTime;

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
                    
                    this.uiController.questionModal.SetActive(true);
                    ta.GetComponent<Rigidbody>().isKinematic = true;

                    this.triggerAgent = ta;

                }

            }

        }

        public void shuffleChoices() {
            string correctChoice = this.item.choices[this.item.correctChoiceIndex];

            System.Random random = new System.Random();
            string[] aitems = Utilities.Helper.shuffle<string>(random, this.item.choices.ToArray());
            this.item.choices = new List<string>(aitems);
            this.item.correctChoiceIndex = this.item.choices.IndexOf(correctChoice);

        }

        public bool checkAnswer(int choiceIndex) => this.item.correctChoiceIndex == choiceIndex;

        public void answer(int choiceIndex) {
            if(!this.triggered) return;

            if(this.checkAnswer(choiceIndex)) {
                this.answeredCorrectly = true;
                this.playerController.setPoint(this.setController.point);

                float maxCoinGain = 30.0f, minCoinGain = 5.0f;
                float coinGain = this.playerController.coin + (
                    maxCoinGain * (1.0f - this.timer.progress)
                );
                coinGain = Mathf.Max(coinGain, minCoinGain);
                coinGain = Convert.ToInt32(coinGain);

                this.playerController.setCoin(coinGain);

                string cg = coinGain.ToString("N0");
                // print($"Correct Answer! Coins got increased by {coinGain}.");
                this.uiController.notification.show(
                    $"Correct Answer! Coins got increased by {cg}.",
                    3.5f
                );

            } else {
                this.answeredCorrectly = false;
                float maxFuelDeduction = this.vehicleController.maxFuel * 0.15f;
                float minFuelDeduction = this.vehicleController.maxFuel * 0.05f;
                float fuelDeduction = maxFuelDeduction * this.timer.progress;
                fuelDeduction = Mathf.Max(maxFuelDeduction * this.timer.progress, minFuelDeduction);
                this.vehicleController.fuel -= fuelDeduction;
                this.vehicleController.updateFuel();
                // this.vehicleController.fuelDeduction += 0.25f;
                string fd = fuelDeduction.ToString("N0");
                // print($"Wrong Answer! Fuel got reduced by {fd}.");
                this.uiController.notification.show(
                    $"Wrong Answer! Fuel got reduced by {fd}.",
                    3.5f
                );

            }

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
